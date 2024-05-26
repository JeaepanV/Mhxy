using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Mhxy.Core;

namespace Mhxy.App.Tasks
{
    public class BaseTask : Detector
    {
        #region ========================    字段

        private string _taskName;
        private bool _isCompleted;
        private bool _isReceivedTask;
        private bool _isRunning;
        private bool _isPaused;

        private int _movingColorCount;

        #endregion

        #region ========================    属性

        public string TaskName
        {
            get => _taskName;
            set => _taskName = value;
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set => _isCompleted = value;
        }
        public bool IsReceivedTask
        {
            get => _isReceivedTask;
            set => _isReceivedTask = value;
        }
        public bool IsRunning
        {
            get => _isRunning;
            set => _isRunning = value;
        }
        public bool IsPaused
        {
            get => _isPaused;
            set => _isPaused = value;
        }

        #endregion

        public BaseTask(Dmsoft dm)
            : base(dm)
        {
            IsDebug = true;
        }

        public void Start()
        {
            _isRunning = true;
            while (_isRunning)
            {
                if (IsBattled())
                {
                    OnBattled();
                }
                else if (IsMoving())
                {
                    OnMoving();
                }
                else if (IsMainView())
                {
                    OnMainView();
                }
                else
                {
                    OnOther();
                }
                DelayLong();
            }
        }

        public void Stop()
        {
            _isRunning = true;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        #region ========================    基本

        public override Detector Delay(int time)
        {
            while (_isPaused)
            {
                Thread.Sleep(1);
            }
            return base.Delay(time);
        }

        public void DelayShort()
        {
            Delay(Rander.Instance.Next(750, 1500));
        }

        public void DelayLong()
        {
            Delay(Rander.Instance.Next(1500, 3000));
        }

        public void KeyPressEsc()
        {
            _dm.KeyPress(27);
        }

        public void KeyPressAndAlt(int keycode)
        {
            _dm.KeyDown(18);
            _dm.KeyPress(keycode);
            _dm.KeyUp(18);
        }

        protected bool InvokeTimeout(Func<bool> func, int time = 5)
        {
            DateTime endTime = DateTime.Now.AddSeconds(time);
            while (DateTime.Now < endTime)
            {
                if (func())
                {
                    return true;
                }

                DelayShort();
            }
            return false;
        }

        protected void Log(
            string info,
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            Logger.Instance.WriteDebug($"[{_taskName}] -> {info}", callerName, fileName, line);
        }
        #endregion

        #region ========================    检测
        /// <summary>
        /// 检测主界面
        /// </summary>
        public bool IsMainView()
        {
            return Bulid(FeatureLibrary.Instance.Dict["界面_活动"]);
        }

        /// <summary>
        /// 检测战斗中
        /// </summary>
        public bool IsBattled()
        {
            return Bulid(FeatureLibrary.Instance.Dict["战斗_收缩"])
                || Bulid(FeatureLibrary.Instance.Dict["战斗_展开"]);
        }

        /// <summary>
        /// 检测移动中
        /// </summary>
        public bool IsMoving()
        {
            var result = _dm.GetColorNum(114, 52, 193, 71, "C0B4A3-2D2C2C", 0.9);
            if (_movingColorCount == 0)
            {
                _movingColorCount = result;
            }
            var moving = Math.Abs(result - _movingColorCount) > 5;
            _movingColorCount = result;
            return moving;
        }

        /// <summary>
        /// 检测包裹界面
        /// </summary>
        public bool IsPackView()
        {
            return Bulid(FeatureLibrary.Instance.Dict["界面_包裹"]);
        }

        /// <summary>
        /// 检测队伍界面
        /// </summary>
        public bool IsTeamView()
        {
            return Bulid(FeatureLibrary.Instance.Dict["界面_队伍"]);
        }

        protected bool IsTaskbar()
        {
            return Bulid(FeatureLibrary.Instance.Dict["界面_任务栏"]);
        }

        protected bool SwitchTaskbar()
        {
            if (IsMainView())
            {
                ExpandTaskbar();
                DelayLong();
                if (!IsTaskbar())
                {
                    ClickRegion(FeatureLibrary.Instance.Dict["界面_任务栏"].ScanRegion);
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region ========================    入口

        protected virtual void OnMainView(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            Log("主界面", callerName, fileName, line);
        }

        protected virtual void OnBattled(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            Log($"战斗中", callerName, fileName, line);
        }

        protected virtual void OnMoving(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            Log($"移动中", callerName, fileName, line);
            if (!_isReceivedTask)
            {
                ClickRect(400, 230, 609, 439);
            }
        }

        protected virtual void OnOther(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            Log($"未知界面", callerName, fileName, line);
        }
        #endregion

        #region ========================    必备

        public virtual bool ClickTask()
        {
            Log($"点击{TaskName}任务");
            return false;
        }

        public virtual bool ReceiveTask()
        {
            Log($"领取{TaskName}任务");
            return false;
        }

        protected bool ExpandTaskbar()
        {
            return Bulid(FeatureLibrary.Instance.Dict["界面_展开任务栏"]);
        }

        /// <summary>
        /// 打开队伍界面
        /// </summary>
        /// <returns></returns>
        protected bool OpenTeamView()
        {
            while (!IsTeamView())
            {
                KeyPressEsc();
                DelayShort();
                KeyPressAndAlt(84);
                DelayLong();
            }
            return true;
        }

        /// <summary>
        /// 打开包裹界面
        /// </summary>
        /// <returns></returns>
        protected bool OpenPackView()
        {
            while (!IsPackView())
            {
                KeyPressEsc();
                DelayShort();
                KeyPressAndAlt(69);
                DelayLong();
            }
            return true;
        }

        /// <summary>
        /// 打开指引界面
        /// </summary>
        /// <returns></returns>
        protected bool OpenGuideView()
        {
            KeyPressEsc();
            DelayShort();
            KeyPressAndAlt(72);
            return true;
        }

        /// <summary>
        /// 打开秘籍界面
        /// </summary>
        /// <returns></returns>
        protected bool OpenSecretView()
        {
            OpenGuideView();
            DelayLong();
            ClickRect(948, 460, 973, 531);
            return true;
        }

        protected bool OpenDoubleExpView()
        {
            KeyPressAndAlt(71);
            return true;
        }

        /// <summary>
        /// 是否队长
        /// </summary>
        /// <returns></returns>
        protected bool IsTeamLeader()
        {
            return !Bulid(FeatureLibrary.Instance.Dict["队伍_暂离"]);
        }

        /// <summary>
        /// NPC交互
        /// </summary>
        /// <returns></returns>
        protected bool NPCInteractive()
        {
            return Bulid(FeatureLibrary.Instance.Dict["界面_NPC交互"]);
        }
        #endregion
    }
}
