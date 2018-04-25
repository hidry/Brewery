using System;
using System.Collections.ObjectModel;
using Brewery.UI.Core.Contracts;
using Brewery.Core.Contracts.ServiceAdapter;
using Brewery.UI.Core.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;
using Telerik.Data.Core;

namespace Brewery.UI.Logic
{
    public class BrewProcessViewModel : ViewModelBase
    {
        public readonly BrewProcessSteps BrewProcessSteps;
        private readonly IMashService _mashService;

        public BrewProcessViewModel(BrewProcessSteps brewProcessSteps, IMashService mashService)
        {
            _mashService = mashService;

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

        private async void StartBrewProcess()
        {
            ButtonStartBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = true;
            await _mashService.StartMashProcess();
        }
        
        public RelayCommand PauseBrewProcessCommand => new RelayCommand(PauseBrewProcess);

        private async void PauseBrewProcess()
        {
            ButtonPauseBrewProcessEnabled = false;
            ButtonStartBrewProcessEnabled = true;
            await _mashService.PauseMashProcess();
        }

        public RelayCommand StopBrewProcessCommand => new RelayCommand(StopBrewProcess);

        private async void StopBrewProcess()
        {
            ButtonStartBrewProcessEnabled = true;
            ButtonStopBrewProcessEnabled = false;
            ButtonPauseBrewProcessEnabled = false;
            await _mashService.StopMashProcess();
        }
    }
    



    public class BrewProcessSteps : ObservableCollection<BrewProcessStep>
    {
        //public BrewProcessSteps()
        //{
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "Aufheizen & Einmaischen",
        //        Temperature = 70,
        //        Rast = 0,
        //        Alert = true,
        //        Mixer = true,
        //        Active = true
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "1. Rast",
        //        Temperature = 66.5,
        //        Rast = 90,
        //        Alert = false,
        //        Mixer = true,
        //        Active = true
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "2. Rast",
        //        Temperature = 66.5,
        //        Rast = 90,
        //        Alert = false,
        //        Mixer = true,
        //        Active = false
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "3. Rast",
        //        Temperature = 66.5,
        //        Rast = 90,
        //        Alert = false,
        //        Mixer = true,
        //        Active = false
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "4. Rast",
        //        Temperature = 66.5,
        //        Rast = 90,
        //        Alert = false,
        //        Mixer = true,
        //        Active = false
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "5. Rast",
        //        Temperature = 66.5,
        //        Rast = 90,
        //        Alert = false,
        //        Mixer = true,
        //        Active = false
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "Aufheizen",
        //        Temperature = 76,
        //        Rast = 0,
        //        Alert = true,
        //        Mixer = true,
        //        Active = true
        //    });
        //    Add(new BrewProcessStep()
        //    {
        //        Step = "Abmaischen",
        //        Temperature = 0,
        //        Rast = 60,
        //        Alert = false,
        //        Mixer = true,
        //        Active = true
        //    });
        //}
    }

    public class StartBrewProcessMessage { }
    public class PauseBrewProcessMessage { }
    public class StopBrewProcessMessage { }
    public class BrewProcessFinishedMessage { }

    //public interface IBrewProcessModule
    //{
    //    void ExecuteBrewProcessStep();
    //}

    //public class BrewProcessModule : IBrewProcessModule
    //{
    //    private enum Status
    //    {
    //        Started,
    //        Paused,
    //        Stopped
    //    }

    //    private readonly ITimer _timer;
    //    private readonly IBoilingPlate1Service _boilingPlate1Module;
    //    private readonly IMixerService _mixerModule;
    //    private readonly IPiezoService _piezoModule;
    //    private readonly BrewProcessSteps _brewProcessSteps;
    //    private Status _status = Status.Stopped;
        
    //    private DateTime _tempReachedAt = default(DateTime);
    //    private int _currentStep = 0;
    //    private bool _messageOpen;
    //    private bool _messageAcknowledged;
    //    private DateTime _startedAt = default(DateTime);

    //    public BrewProcessModule(ITimer timer, IBoilingPlate1Service boilingPlate1Module, IMixerService mixerModule, IPiezoService piezoModule, BrewProcessSteps brewProcessSteps, IDevicesService devicesService)
    //    {
    //        _timer = timer;
    //        _boilingPlate1Module = boilingPlate1Module;
    //        _mixerModule = mixerModule;
    //        _piezoModule = piezoModule;
    //        _brewProcessSteps = brewProcessSteps;
    //        Messenger.Default.Register<StartBrewProcessMessage>(this, StartBrewProcessMessageReceived);
    //        Messenger.Default.Register<PauseBrewProcessMessage>(this, PauseBrewProcessMessageReceived);
    //        Messenger.Default.Register<StopBrewProcessMessage>(this, StopBrewProcessMessageReceived);
            
    //        devicesService.Temperature1ChangedEvent += (sender, args) => Temperature1 = args.Temperature;
    //    }
        
    //    private double Temperature1 { get; set; }

    //    private void StopBrewProcessMessageReceived(StopBrewProcessMessage obj)
    //    {
    //        _status = Status.Stopped;
    //        _boilingPlate1Module.PowerOff();
    //        _tempReachedAt = default(DateTime);
    //        _startedAt = default(DateTime);
    //        _currentStep = 0;
    //        _timer.RemoveEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
    //    }

    //    private void PauseBrewProcessMessageReceived(PauseBrewProcessMessage obj)
    //    {
    //        _status = Status.Paused;
    //        _timer.RemoveEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
    //    }

    //    private void StartBrewProcessMessageReceived(StartBrewProcessMessage obj)
    //    {
    //        if (_status != Status.Paused)
    //        {
    //            foreach (var brewProcessStep in _brewProcessSteps)
    //            {
    //                brewProcessStep.ElapsedTime = null;
    //            }
    //        }
    //        _status = Status.Started;
    //        ExecuteBrewProcessStep();
    //        _timer.AddEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
    //    }

    //    public void ExecuteBrewProcessStep()
    //    {
    //        _piezoModule.Power(false);

    //        if (_status != Status.Started)
    //            return;

    //        var currentStep = _brewProcessSteps[_currentStep];
    //        if (!currentStep.Active)
    //        {
    //            SetNextStep();
    //            return;
    //        }
            
    //        if (_startedAt == default(DateTime))
    //            _startedAt = DateTime.Now;

    //        var elapsed = (DateTime.Now - _startedAt);

    //        currentStep.ElapsedTime = $"{elapsed.Hours.ToString("00")}:{elapsed.Minutes.ToString("00")}:{elapsed.Seconds.ToString("00")}";

    //        _boilingPlate1Module.ManageTemperature(currentStep.Temperature);

    //        _mixerModule.Power(currentStep.Mixer);

    //        //wenn ein nachfolgender Schritt eine niedrigere Temperatur benötigt als der Vorgängerschritt
    //        if (_currentStep > 0 && currentStep.Temperature < _brewProcessSteps[_currentStep - 1].Temperature && _tempReachedAt == default(DateTime))
    //        {
    //            if (Temperature1 <= currentStep.Temperature)
    //            {
    //                _tempReachedAt = DateTime.Now;
    //            }
    //        }
    //        //wenn Solltemperatur erreicht
    //        else if (Temperature1 >= currentStep.Temperature)
    //        {
    //            if (_tempReachedAt == default(DateTime))
    //                _tempReachedAt = DateTime.Now;

    //            //Rast
    //            if (_tempReachedAt.AddMinutes(currentStep.Rast) <= DateTime.Now)
    //            {
    //                //Evtl. Meldung anzeigen und warten bis bestätigt
    //                if (currentStep.Alert && !_messageAcknowledged)
    //                {
    //                    if (!_messageOpen)
    //                    {
    //                        _messageOpen = true;

    //                        DisplayBrewStepMessage(currentStep, () => { _messageOpen = false; _messageAcknowledged = true; });
    //                        try
    //                        {
    //                            SendBrewStepNotification(currentStep); // wenn kein Netzwerk verfügbar Exception!?
    //                        }
    //                        catch (Exception)
    //                        {
    //                            //todo: logging
    //                        }   
    //                    }
    //                    _piezoModule.Power(true);
    //                }
    //                else
    //                {
    //                    SetNextStep();
    //                }
    //            }
    //        }
    //    }

    //    private void SetNextStep()
    //    {
    //        if (_brewProcessSteps.Count - 1 > _currentStep)
    //        {
    //            _currentStep += 1;
    //        }
    //        else
    //        {
    //            Messenger.Default.Send(new BrewProcessFinishedMessage());
    //            _timer.RemoveEvent(nameof(ExecuteBrewProcessStep), (o, e) => ExecuteBrewProcessStep());
    //            _currentStep = 0;
    //        }
    //        _tempReachedAt = default(DateTime);
    //        _startedAt = default(DateTime);
    //        _messageAcknowledged = false;
    //    }

    //    private void DisplayBrewStepMessage(BrewProcessStep currentStep, Action okButtonCommand)
    //    {
    //        Messenger.Default.Send(new ShowMessageDialog()
    //        {
    //            Title = "Rast-Ende",
    //            Message = currentStep.ToString(),
    //            OkButtonCommand = okButtonCommand
    //        });
    //    }

    //    private void SendBrewStepNotification(BrewProcessStep currentStep)
    //    {
    //        var client = new PushbulletClient("o.8eOhOCEf24WUSvlXsQQ05X5XOpWS40EY");
    //        var reqeust = new PushNoteRequest()
    //        {
    //            Email = "hidry@gmx.de",
    //            Title = "Rast-Ende",
    //            Body = currentStep.ToString()
    //        };
    //        var response = client.PushNote(reqeust);
    //    }
    //}

    public class BrewProcessStep : ViewModelBase
    {
        //todo: prüfen ob Annotations auch für RadDataGrid verfügbar https://feedback.telerik.com/Project/167/Feedback/List/Your%20Items
        [Display(Header = "°C")]
        public double Temperature { get; set; }
        [Display(Header = "Rast in Minuten")]
        public int Rast { get; set; }
        [Display(Header = "Rührgerät")]
        public bool Mixer { get; set; }
        [Display(Header = "Alarm")]
        public bool Alert { get; set; }
        private string _elapsedTime;
        [ReadOnly]
        public string ElapsedTime
        {
            get => _elapsedTime;
            set { Set(() => ElapsedTime, ref _elapsedTime, value); }
        }

        [Display(Header = "Schritt")]
        public string Step { get; internal set; }

        [Display(Header = "Aktiv")]
        public bool Active { get; set; }

        public override string ToString()
        {
            return $"Temperatur: {Temperature}, Schritt: {Step}, Rast: {Rast}, Benachrichtigung: {Alert}, Rührgerät: {Mixer}";
        }
    }
}