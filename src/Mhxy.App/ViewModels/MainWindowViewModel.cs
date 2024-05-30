using System;
using System.Threading;
using Mhxy.App.Tasks;
using Mhxy.Core;
using Prism.Commands;
using Prism.Mvvm;

namespace Mhxy.App.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";

        #region ========================    属性

        private bool _regButtonEnabled = true;
        public bool RegButtonEnabled
        {
            get { return _regButtonEnabled; }
            set { SetProperty(ref _regButtonEnabled, value); }
        }

        #endregion

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private Dmsoft _dmsoft;
        private ShiMenTask _task;
        private Thread _thread;

        public MainWindowViewModel()
        {
            Rander.Instance = new Rander();
            FeatureLibrary.Instance = new FeatureLibrary();
            InitCommand();
        }

        #region ========================    命令

        private void InitCommand()
        {
            RegCommand = new(RegExecute);
            RunCommand = new(RunExecute);
            StopCommand = new(StopExecute);
            UnBindCommand = new(UnBindExecute);
        }

        public DelegateCommand RegCommand { get; set; }

        private void RegExecute()
        {
            if (_dmsoft == null)
            {
                if (DmsoftRegister.RegisterDmSoftDllA())
                {
                    RegButtonEnabled = false;
                    _dmsoft = new Dmsoft();
                    _dmsoft.Reg(DmsoftConfig.RegCode, DmsoftConfig.VerInfo);
                    _task = new ShiMenTask(_dmsoft);
                }
            }
        }

        public DelegateCommand RunCommand { get; set; }

        private void RunExecute()
        {
            if (_dmsoft != null)
            {
                _thread = new Thread(() =>
                {
                    try
                    {
                        _dmsoft.BindWindowEx(
                            722972,
                            "dx.graphic.3d.10plus",
                            "dx.mouse.api",
                            "dx.keypad.api",
                            "",
                            0
                        );
                        var path = _dmsoft.GetBasePath();
                        //_dmsoft.SetPath(path);
                        _dmsoft.SetPath(DmsoftConfig.GlobalPath);
                        _dmsoft.SetDict(0, "lvl.txt");
                        _task.Start();
                    }
                    catch (Exception error)
                    {
                        Logger.Instance.WriteError(error.Message);
                        Logger.Instance.WriteError(error.StackTrace);
                    }
                });
                _thread.Start();
            }
        }

        public DelegateCommand StopCommand { get; set; }

        private void StopExecute()
        {
            try
            {
                _task.Stop();
                _thread.Interrupt();
                _thread.Join();
            }
            catch (Exception error)
            {
                Logger.Instance.WriteError(error.Message);
                Logger.Instance.WriteError(error.StackTrace);
            }
        }

        public DelegateCommand UnBindCommand { get; set; }

        private void UnBindExecute()
        {
            _dmsoft?.UnBindWindow();
        }

        #endregion
    }
}
