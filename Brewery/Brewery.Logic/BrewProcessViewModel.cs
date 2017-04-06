using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Brewery.Core.Contracts;
using Brewery.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using PushbulletSharp.Models.Responses;
using Telerik.Data.Core;

namespace Brewery.Logic
{
    public class BrewProcessViewModel : ViewModelBase
    {
        private readonly IMixerModule _mixerModule;
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        private readonly ITimer _timer;

        public BrewProcessViewModel(ITimer timer, IMixerModule mixerModule, ITemperature1Module temperature1Module, ITemperatureControl1Module temperatureControl1Module)
        {
            _timer = timer;
            _mixerModule = mixerModule;
            _temperature1Module = temperature1Module;
            _temperatureControl1Module = temperatureControl1Module;

            //_brewProcessTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 1) };
            //_brewProcessTimer.Tick += (sender, o) => { ExecuteBrewProcessStep(); };

            ButtonStartBrewProcessEnabled = true;
            ButtonPauseBrewProcessEnabled = false;
            ButtonStopBrewProcessEnabled = false;
            ButtonRemoveBrewProcessStepEnabled = false;

            BrewProcessSteps.Add(new BrewProcessStep()
            {
                Temperatur = 70,
                Rast = 1,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
            BrewProcessSteps.Add(new BrewProcessStep()
            {
                Temperatur = 66.5,
                Rast = 90,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
            BrewProcessSteps.Add(new BrewProcessStep()
            {
                Temperatur = 76,
                Rast = 0,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
        }
        public ObservableCollection<BrewProcessStep> BrewProcessSteps = new ObservableCollection<BrewProcessStep>();

        public RelayCommand AddBrewProcessStepCommand => new RelayCommand(AddBrewProcessStep);

        private void AddBrewProcessStep()
        {
            BrewProcessSteps.Add(new BrewProcessStep());
        }

        private BrewProcessStep _selectedBrewProcessStep;
        public BrewProcessStep SelectedBrewProcessStep
        {
            get { return _selectedBrewProcessStep; }
            set {
                Set(() => SelectedBrewProcessStep, ref _selectedBrewProcessStep, value);
                ButtonRemoveBrewProcessStepEnabled = value != null;
            }
        }

        private bool _buttonStartBrewProcessEnabled;
        public bool ButtonStartBrewProcessEnabled
        {
            get { return _buttonStartBrewProcessEnabled; }
            set { Set(() => ButtonStartBrewProcessEnabled, ref _buttonStartBrewProcessEnabled, value); }
        }

        private bool _buttonPauseBrewProcessEnabled;
        public bool ButtonPauseBrewProcessEnabled
        {
            get { return _buttonPauseBrewProcessEnabled; }
            set { Set(() => ButtonPauseBrewProcessEnabled, ref _buttonPauseBrewProcessEnabled, value); }
        }

        private bool _buttonStopBrewProcessEnabled;
        public bool ButtonStopBrewProcessEnabled
        {
            get { return _buttonStopBrewProcessEnabled; }
            set { Set(() => ButtonStopBrewProcessEnabled, ref _buttonStopBrewProcessEnabled, value); }
        }

        private bool _buttonRemoveBrewProcessStepEnabled;
        public bool ButtonRemoveBrewProcessStepEnabled
        {
            get { return _buttonRemoveBrewProcessStepEnabled; }
            set { Set(() => ButtonRemoveBrewProcessStepEnabled, ref _buttonRemoveBrewProcessStepEnabled, value ); }
        }

        public RelayCommand RemoveBrewProcessStepCommand => new RelayCommand(RemoveBrewProcessStep);

        private void RemoveBrewProcessStep()
        {
            BrewProcessSteps.Remove(SelectedBrewProcessStep);
            SelectedBrewProcessStep = null;
        }

        public RelayCommand StartBrewProcessCommand => new RelayCommand(StartBrewProcess);

        private void StartBrewProcess()
        {
            ButtonStartBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = true;
            foreach (var brewProcessStep in BrewProcessSteps)
            {
                brewProcessStep.ElapsedTime = null;
            }
            ExecuteBrewProcessStep();
            _timer.AddEvent((o, e) => ExecuteBrewProcessStep());
        }

        //private readonly DispatcherTimer _brewProcessTimer;
        private DateTime _tempReachedAt = default(DateTime);
        private int _currentStep = 0;
        private bool _messageOpen;
        private bool _messageAcknowledged;
        private DateTime _startedAt = default(DateTime);

        private void ExecuteBrewProcessStep()
        {
            if (ButtonStartBrewProcessEnabled == true)
                return;

            var temperatureCurrent = _temperature1Module.GetCurrenTemperature().Temperature;
            var currentStep = BrewProcessSteps[_currentStep];

            if (_startedAt == default(DateTime))
                _startedAt = DateTime.Now;
            
            currentStep.ElapsedTime = new DateTime((DateTime.Now - _startedAt).Ticks).ToString("mm:ss");

            _temperatureControl1Module.ManageTemperature(currentStep.Temperatur, temperatureCurrent);
            
            _mixerModule.Power(currentStep.Ruehrgeraet);

            //wenn ein nachfolgender Schritt eine niedrigere Temperatur benötigt als der Vorgängerschritt
            if (_currentStep > 0 && currentStep.Temperatur < BrewProcessSteps[_currentStep - 1].Temperatur && _tempReachedAt == default(DateTime))
            {
                if (temperatureCurrent <= currentStep.Temperatur)
                {
                    _tempReachedAt = DateTime.Now;
                }
            }
            //wenn Solltemperatur erreicht
            else if (temperatureCurrent >= currentStep.Temperatur)
            {
                if (_tempReachedAt == default(DateTime))
                    _tempReachedAt = DateTime.Now;

                //Rast
                if (_tempReachedAt.AddMinutes(currentStep.Rast) <= DateTime.Now)
                {
                    //Evtl. Meldung anzeigen und warten bis bestätigt
                    if (currentStep.Benachrichtigung && !_messageAcknowledged)
                    {
                        if (!_messageOpen)
                        {
                            _messageOpen = true;

                            DisplayBrewStepMessage(currentStep);
                            SendBrewStepNotification(currentStep);
                        }
                    }
                    else
                    {
                        if (BrewProcessSteps.Count - 1 > _currentStep)
                        {
                            _currentStep += 1;
                        }
                        else
                        {
                            ButtonStartBrewProcessEnabled = true;
                            ButtonPauseBrewProcessEnabled = false;
                            ButtonStopBrewProcessEnabled = false;
                            _timer.RemoveEvent((o, e) => ExecuteBrewProcessStep());
                            _currentStep = 0;
                        }
                        _tempReachedAt = default(DateTime);
                        _startedAt = default(DateTime);
                        _messageAcknowledged = false;
                    }
                }
            }
        }

        private void SendBrewStepNotification(BrewProcessStep currentStep)
        {
            var client = new PushbulletClient("o.8eOhOCEf24WUSvlXsQQ05X5XOpWS40EY");
            var reqeust = new PushNoteRequest()
            {
                Email = "hidry@gmx.de",
                Title = "Rast-Ende",
                Body = currentStep.ToString()
            };
            var response = client.PushNote(reqeust);
        }

        private void DisplayBrewStepMessage(BrewProcessStep currentStep)
        {
            Messenger.Default.Send(new ShowMessageDialog()
            {
                Title = "Rast-Ende",
                Message = currentStep.ToString(),
                OkButtonCommand = () =>
                {
                    _messageOpen = false;
                    _messageAcknowledged = true;
                }
            });
        }

        public RelayCommand PauseBrewProcessCommand => new RelayCommand(PauseBrewProcess);

        private void PauseBrewProcess()
        {
            ButtonPauseBrewProcessEnabled = false;
            ButtonStartBrewProcessEnabled = true;
            _timer.RemoveEvent((o, e) => ExecuteBrewProcessStep());
        }

        public RelayCommand StopBrewProcessCommand => new RelayCommand(StopBrewProcess);

        private void StopBrewProcess()
        {
            _temperatureControl1Module.BoilingPlateOff();
            ButtonStartBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = false;
            _timer.RemoveEvent((o, e) => ExecuteBrewProcessStep());
            _tempReachedAt = default(DateTime);
            _startedAt = default(DateTime);
            _currentStep = 0;
        }
    }

    public class BrewProcessStep : ViewModelBase
    {
        //todo: prüfen ob Annotations auch für RadDataGrid verfügbar https://feedback.telerik.com/Project/167/Feedback/List/Your%20Items
        [Display(Header = "Temperatur in °C")]
        public double Temperatur { get; set; }
        [Display(Header = "Rast in Minuten")]
        public int Rast { get; set; }
        [Display(Header = "Rührgerät")]
        public bool Ruehrgeraet { get; set; }
        [Display(Header = "Benachrichtigung wenn Schritt abgesschlossen")]
        public bool Benachrichtigung { get; set; }
        private string _elapsedTime;
        [ReadOnly]
        public string ElapsedTime
        {
            get { return _elapsedTime; }
            set { Set(() => ElapsedTime, ref _elapsedTime, value); }
        }

        public override string ToString()
        {
            return $"{nameof(Temperatur)}: {Temperatur}, {nameof(Rast)}: {Rast}, {nameof(Benachrichtigung)}: {Benachrichtigung}, {nameof(Ruehrgeraet)}: {Ruehrgeraet}";
        }
    }
}