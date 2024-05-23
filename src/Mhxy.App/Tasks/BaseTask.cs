using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Mhxy.App.Common;
using Mhxy.App.Common.Enums;
using Mhxy.App.Helpers;

namespace Mhxy.App.Tasks
{
    public class BaseTask
    {
        protected Dmsoft _dm;
        protected string _taskName;

        private int _movingColorCount = 0;

        private bool _isRunning = false;
        private bool _isPaused = false;
        private bool _isCompleted = false;

        protected Detector _detector;

        /// <summary>
        /// 检测主界面
        /// </summary>
        private Feature _featureMainView = new Feature
        {
            Mode = DetectMode.FindPic,
            DetectRegion = new Region(208, 0, 645, 73),
            PicName = "主界面_活动",
        };

        /// <summary>
        /// 检测战斗中收缩
        /// </summary>
        private Feature _featureBattledHide = new Feature
        {
            Mode = DetectMode.FindPic,
            DetectRegion = new Region(8, 25, 50, 71),
            PicName = "战斗中_收缩"
        };

        /// <summary>
        /// 检测战斗中展开
        /// </summary>
        private Feature _featureBattledShow = new Feature
        {
            Mode = DetectMode.FindPic,
            DetectRegion = new Region(365, 30, 404, 69),
            PicName = "战斗中_展开",
            Clicked = true,
            ClickRegion = new Region { Width = 15, Height = 15 }
        };

        /// <summary>
        /// 检测任务栏
        /// </summary>
        private Feature _featureTaskbar = new Feature
        {
            Name = "界面_任务栏",
            Mode = DetectMode.FindPic,
            PicName = "界面_任务栏",
            DetectRegion = new Region(803, 98, 865, 145)
        };

        /// <summary>
        /// 检测队伍栏
        /// </summary>
        private Feature _featureTeambar = new Feature
        {
            Name = "界面_队伍栏",
            Mode = DetectMode.FindPic,
            PicName = "界面_队伍栏",
            DetectRegion = new Region(918, 99, 1022, 146)
        };

        /// <summary>
        /// 展开任务栏
        /// </summary>
        private Feature _featureExpandTaskbar = new Feature
        {
            Name = "界面_展开任务栏",
            Mode = DetectMode.FindPic,
            PicName = "界面_展开任务栏",
            DetectRegion = new Region(968, 104, 1022, 153),
            Clicked = true,
            WaitTime = Rander.Instance.Next(1000, 2000),
            ClickRegion = new Region { Width = 15, Height = 15 }
        };

        /// <summary>
        /// NPC交互
        /// </summary>
        private Feature _featureNPCInteractive = new Feature
        {
            Name = "界面_NPC交互",
            Mode = DetectMode.FindColorBlock,
            DeltaColor = "EFD1A0-061022",
            DetectRegion = new Region(798, 98, 987, 587),
            Clicked = true,
            WaitTime = Rander.Instance.Next(1000, 2000),
            ClickRegion = new Region { Height = 40, Width = 160 },
            ColorCount = 4800,
            BlockWidth = 170,
            BlockHeight = 40,
        };

        public BaseTask(Dmsoft dm)
        {
            _dm = dm;
            _detector = new Detector(dm);
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
            _isRunning = false;
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Resume()
        {
            _isPaused = false;
        }

        /// <summary>
        /// 检测队伍栏
        /// </summary>
        /// <returns></returns>
        protected bool IsTaskbar()
        {
            var result = _detector.Bulid(_featureTaskbar);
            return result;
        }

        /// <summary>
        /// 切换任务栏
        /// </summary>
        protected void SwitchTaskbar()
        {
            ExpandTaskbar();
            if (!IsTaskbar())
            {
                _detector.ClickRect(_featureTaskbar.DetectRegion);
            }
        }

        /// <summary>
        /// 检测队伍栏
        /// </summary>
        /// <returns></returns>
        protected bool IsTeambar()
        {
            var result = _detector.Bulid(_featureTeambar);
            return result;
        }

        /// <summary>
        /// 切换队伍栏
        /// </summary>
        protected void SwitchTeambar()
        {
            ExpandTaskbar();
            if (!IsTeambar())
            {
                _detector.ClickRect(_featureTeambar.DetectRegion);
            }
        }

        /// <summary>
        /// 展开任务栏
        /// </summary>
        /// <returns></returns>
        protected bool ExpandTaskbar()
        {
            var result = _detector.Bulid(_featureExpandTaskbar);
            return result;
        }

        /// <summary>
        /// NPC交互
        /// </summary>
        /// <returns></returns>
        protected bool NPCInteractive()
        {
            var result = _detector.Bulid(_featureNPCInteractive);
            return result;
        }

        #region ========================    基本方法
        public void Delay(int time)
        {
            while (_isPaused)
            {
                Thread.Sleep(1);
            }
            Thread.Sleep(time);
        }

        public void DelayShort()
        {
            Delay(Rander.Instance.Next(500, 1500));
        }

        public void DelayLong()
        {
            Delay(Rander.Instance.Next(1500, 3000));
        }

        protected void KeyPressEsc()
        {
            _dm.KeyPress(27);
        }

        protected void KeyPressAndAlt(int key)
        {
            _dm.KeyDown(18);
            _dm.KeyPress(key);
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

        #endregion========================    基本方法

        #region ========================    检测界面

        /// <summary>
        /// 检测主界面
        /// </summary>
        /// <returns></returns>
        protected bool IsMainView()
        {
            var result = _detector.Bulid(_featureMainView);
            return result;
        }

        /// <summary>
        /// 检测战斗中
        /// </summary>
        /// <returns></returns>
        protected bool IsBattled()
        {
            var result = _detector.Bulid(_featureBattledHide);
            if (result)
                return true;
            result = _detector.Bulid(_featureBattledShow);
            return result;
        }

        /// <summary>
        /// 检测移动中
        /// </summary>
        /// <returns></returns>
        protected bool IsMoving()
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

        #endregion

        #region ========================    界面入口

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
    }
}
