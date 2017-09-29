using System;
using System.Collections.ObjectModel;
using Brewery.Core.Contracts;
using Brewery.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using Telerik.Data.Core;

namespace Brewery.Logic
{
    public class BrewProcessViewModel : ViewModelBase
    {
        public readonly BrewProcessSteps BrewProcessSteps;
        private readonly IBrewProcessModule _brewProcessModule;

        public BrewProcessViewModel(IBrewProcessModule brewProcessModule, BrewProcessSteps brewProcessSteps)
        {
            _brewProcessModule = brewProcessModule; // aktuell nötgi, da sonst keine Instanz von BrewProcessModule erstellt wird

            ButtonStartBrewProcessEnabled = true;
            ButtonPauseBrewProcessEnabled = false;
            ButtonStopBrewProcessEnabled = false;
            ButtonRemoveBrewProcessStepEnabled = false;

            BrewProcessSteps = brewProcessSteps;

            Messenger.Default.Register<BrewProcessFinishedMessage>(this, BrewProcessFinishedMessageReceived);
        }

        private void BrewProcessFinishedMessageReceived(BrewProcessFinishedMessage obj)
        {
            ButtonStartBrewProcessEnabled = true;
            ButtonPauseBrewProcessEnabled = false;
            ButtonStopBrewProcessEnabled = false;
        }


        public RelayCommand AddBrewProcessStepCommand => new RelayCommand(AddBrewProcessStep);

        private void AddBrewProcessStep()
        {
            BrewProcessSteps.Add(new BrewProcessStep());
        }

        private BrewProcessStep _selectedBrewProcessStep;
        public BrewProcessStep SelectedBrewProcessStep
        {
            get => _selectedBrewProcessStep;
            set {
                Set(() => SelectedBrewProcessStep, ref _selectedBrewProcessStep, value);
                ButtonRemoveBrewProcessStepEnabled = value != null;
            }
        }

        private bool _buttonStartBrewProcessEnabled;
        public bool ButtonStartBrewProcessEnabled
        {
            get => _buttonStartBrewProcessEnabled;
            set { Set(() => ButtonStartBrewProcessEnabled, ref _buttonStartBrewProcessEnabled, value); }
        }

        private bool _buttonPauseBrewProcessEnabled;
        public bool ButtonPauseBrewProcessEnabled
        {
            get => _buttonPauseBrewProcessEnabled;
            set { Set(() => ButtonPauseBrewProcessEnabled, ref _buttonPauseBrewProcessEnabled, value); }
        }

        private bool _buttonStopBrewProcessEnabled;
        public bool ButtonStopBrewProcessEnabled
        {
            get => _buttonStopBrewProcessEnabled;
            set { Set(() => ButtonStopBrewProcessEnabled, ref _buttonStopBrewProcessEnabled, value); }
        }

        private bool _buttonRemoveBrewProcessStepEnabled;
        public bool ButtonRemoveBrewProcessStepEnabled
        {
            get => _buttonRemoveBrewProcessStepEnabled;
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
            Messenger.Default.Send(new StartBrewProcessMessage());
        }
        
        public RelayCommand PauseBrewProcessCommand => new RelayCommand(PauseBrewProcess);

        private void PauseBrewProcess()
        {
            ButtonPauseBrewProcessEnabled = false;
            ButtonStartBrewProcessEnabled = true;
            Messenger.Default.Send(new PauseBrewProcessMessage());
        }

        public RelayCommand StopBrewProcessCommand => new RelayCommand(StopBrewProcess);

        private void StopBrewProcess()
        {
            ButtonStartBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = false;
            Messenger.Default.Send(new StopBrewProcessMessage());
        }
    }
    



    public class BrewProcessSteps : ObservableCollection<BrewProcessStep>
    {
        public BrewProcessSteps()
        {
            Add(new BrewProcessStep()
            {
                Temperatur = 70,
                Rast = 1,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
            Add(new BrewProcessStep()
            {
                Temperatur = 66.5,
                Rast = 90,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
            Add(new BrewProcessStep()
            {
                Temperatur = 76,
                Rast = 0,
                Benachrichtigung = true,
                Ruehrgeraet = true
            });
        }
    }

    public class StartBrewProcessMessage { }
    public class PauseBrewProcessMessage { }
    public class StopBrewProcessMessage { }
    public class BrewProcessFinishedMessage { }

    public interface IBrewProcessModule
    {
        void ExecuteBrewProcessStep();
    }

    public class BrewProcessModule : IBrewProcessModule
    {
        private enum Status
        {
            Started,
            Paused,
            Stopped
        }

        private readonly ITimer _timer;
        private readonly ITemperature1Module _temperature1Module;
        private readonly ITemperatureControl1Module _temperatureControl1Module;
        private readonly IMixerModule _mixerModule;
        private readonly IPiezoModule _piezoModule;
        private readonly BrewProcessSteps _brewProcessSteps;
        private Status _status = Status.Stopped;
        
        private DateTime _tempReachedAt = default(DateTime);
        private int _currentStep = 0;
        private bool _messageOpen;
        private bool _messageAcknowledged;
        private DateTime _startedAt = default(DateTime);

        public BrewProcessModule(ITimer timer, ITemperature1Module temperature1Module, ITemperatureControl1Module temperatureControl1Module, IMixerModule mixerModule, IPiezoModule piezoModule, BrewProcessSteps brewProcessSteps)
        {
            _timer = timer;
            _temperature1Module = temperature1Module;
            _temperatureControl1Module = temperatureControl1Module;
            _mixerModule = mixerModule;
            _piezoModule = piezoModule;
            _brewProcessSteps = brewProcessSteps;
            Messenger.Default.Register<StartBrewProcessMessage>(this, StartBrewProcessMessageReceived);
            Messenger.Default.Register<PauseBrewProcessMessage>(this, PauseBrewProcessMessageReceived);
            Messenger.Default.Register<StopBrewProcessMessage>(this, StopBrewProcessMessageReceived);
        }

        private void StopBrewProcessMessageReceived(StopBrewProcessMessage obj)
        {
            _status = Status.Stopped;
            _temperatureControl1Module.BoilingPlateOff();
            _tempReachedAt = default(DateTime);
            _startedAt = default(DateTime);
            _currentStep = 0;
            _timer.RemoveEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
        }

        private void PauseBrewProcessMessageReceived(PauseBrewProcessMessage obj)
        {
            _status = Status.Paused;
            _timer.RemoveEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
        }

        private void StartBrewProcessMessageReceived(StartBrewProcessMessage obj)
        {
            if (_status != Status.Paused)
            {
                foreach (var brewProcessStep in _brewProcessSteps)
                {
                    brewProcessStep.ElapsedTime = null;
                }
            }
            _status = Status.Started;
            ExecuteBrewProcessStep();
            _timer.AddEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
        }

        public void ExecuteBrewProcessStep()
        {
            _piezoModule.Power(false);

            if (_status != Status.Started)
                return;

            var temperatureCurrent = _temperature1Module.GetCurrenTemperature().Temperature;
            var currentStep = _brewProcessSteps[_currentStep];

            if (_startedAt == default(DateTime))
                _startedAt = DateTime.Now;

            currentStep.ElapsedTime = new DateTime((DateTime.Now - _startedAt).Ticks).ToString("mm:ss");

            _temperatureControl1Module.ManageTemperature(currentStep.Temperatur, temperatureCurrent);

            _mixerModule.Power(currentStep.Ruehrgeraet);

            //wenn ein nachfolgender Schritt eine niedrigere Temperatur benötigt als der Vorgängerschritt
            if (_currentStep > 0 && currentStep.Temperatur < _brewProcessSteps[_currentStep - 1].Temperatur && _tempReachedAt == default(DateTime))
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

                            DisplayBrewStepMessage(currentStep, () => { _messageOpen = false; _messageAcknowledged = true; });
                            SendBrewStepNotification(currentStep);
                        }
                        _piezoModule.Power(true);
                    }
                    else
                    {
                        if (_brewProcessSteps.Count - 1 > _currentStep)
                        {
                            _currentStep += 1;
                        }
                        else
                        {
                            Messenger.Default.Send(new BrewProcessFinishedMessage());
                            _timer.RemoveEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
                            _currentStep = 0;
                        }
                        _tempReachedAt = default(DateTime);
                        _startedAt = default(DateTime);
                        _messageAcknowledged = false;
                    }
                }
            }
        }

        private void DisplayBrewStepMessage(BrewProcessStep currentStep, Action okButtonCommand)
        {
            Messenger.Default.Send(new ShowMessageDialog()
            {
                Title = "Rast-Ende",
                Message = currentStep.ToString(),
                OkButtonCommand = okButtonCommand
            });
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
            get => _elapsedTime;
            set { Set(() => ElapsedTime, ref _elapsedTime, value); }
        }

        public override string ToString()
        {
            return $"{nameof(Temperatur)}: {Temperatur}, {nameof(Rast)}: {Rast}, {nameof(Benachrichtigung)}: {Benachrichtigung}, {nameof(Ruehrgeraet)}: {Ruehrgeraet}";
        }
    }
}