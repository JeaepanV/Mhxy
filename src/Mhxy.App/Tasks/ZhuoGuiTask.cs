using System;
using System.Runtime.CompilerServices;
using Mhxy.Core;

namespace Mhxy.App.Tasks
{
    public class ZhuoGuiTask : BaseTask
    {
        #region ========================    字段

        private bool _isInitTeamLevle = false;
        private bool _isInitBattled = false;
        private bool _isReceiveDoubleExp = false;

        private int _minLevel = 69;
        private int _maxLevel = 89;
        private int _minPreson = 3;

        #endregion

        public ZhuoGuiTask(Dmsoft dm)
            : base(dm)
        {
            TaskName = "捉鬼";
        }

        #region ========================    入口

        protected override void OnMainView(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnMainView();
            _isInitBattled = false;
            if (SwitchTaskbar())
            {
                DelayLong();
            }
            if (!ClickTask())
            {
                CreateTeam();
                ReceiveTask();
            }
            else
            {
                Click(ScanX, ScanY);
            }
        }

        protected override void OnBattled(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnBattled();
            if (!_isInitBattled)
            {
                OpenTeamView();
                CheckedMinPreson();
                KickAFK();
                KeyPressEsc();
                if (!_isReceiveDoubleExp)
                {
                    ReceiveDoubleExp();
                }
                _isInitBattled = true;
            }
        }

        protected override void OnMoving(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnMoving();
            _isInitBattled = false;
        }

        protected override void OnOther(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnOther();
            KeyPressEsc();
            _isInitBattled = false;
        }
        #endregion

        /// <summary>
        /// 点击任务
        /// </summary>
        /// <returns></returns>
        public override bool ClickTask()
        {
            base.ClickTask();

            var result = Bulid(FeatureLibrary.Instance.Dict["任务栏_捉鬼"]);
            if (result)
            {
                IsReceivedTask = true;
            }
            return result;
        }

        /// <summary>
        /// 领取任务
        /// </summary>
        /// <returns></returns>
        public override bool ReceiveTask()
        {
            base.ReceiveTask();
            IsReceivedTask = true;
            if (IsMainView())
            {
                OpenSecretView();
                DelayLong();
                ClickRect(771, 395, 890, 429);
                DelayLong();
                InvokeTimeout(NPCInteractive, 60);
                ClickTask();
                Click(ScanX, ScanY);
                _isReceiveDoubleExp = false;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 领取双倍点数
        /// </summary>
        private void ReceiveDoubleExp()
        {
            OpenDoubleExpView();
            DelayLong();
            ClickRect(562, 615, 625, 653);
            DelayShort();
            ClickRect(818, 617, 887, 652);
            DelayLong();
            KeyPressEsc();
            _isReceiveDoubleExp = true;
        }

        /// <summary>
        /// 点击队伍匹配
        /// </summary>
        /// <returns></returns>
        private bool ClickMatchPerson()
        {
            return Bulid(FeatureLibrary.Instance.Dict["队伍_匹配"]);
        }

        /// <summary>
        /// 创建队伍
        /// </summary>
        /// <returns></returns>
        private bool CreateTeam()
        {
            OpenTeamView();

            if (!IsTeamLeader())
            {
                // 如果不是队长则退出退伍
                ClickRect(88, 631, 218, 667);
                DelayShort();
            }

            if (Bulid(FeatureLibrary.Instance.Dict["队伍_创建"]))
            {
                DelayShort();
            }

            // 点击调整目标
            ClickRect(685, 150, 714, 179);
            DelayShort();

            if (!_isInitTeamLevle)
            {
                if (!CheckedMinLevel() && !CheckedMaxLevel())
                {
                    Bulid(FeatureLibrary.Instance.Dict["队伍_捉鬼"]);
                    DelayShort();
                }
                if (!CheckedMinLevel())
                {
                    SwipeMinLevel();
                    DelayShort();
                }
                if (!CheckedMaxLevel())
                {
                    SwipeMaxLevel();
                    DelayShort();
                }
            }
            // 点击确定调整
            ClickRect(596, 630, 687, 668);
            DelayShort();
            CheckedMinPreson();
            KeyPressEsc();
            DelayShort();
            return true;
        }

        /// <summary>
        /// 获取队伍人数
        /// </summary>
        private int GetPresonCount()
        {
            Bulid(FeatureLibrary.Instance.Dict["队伍_助战"]);
            int? person = Points?.Count;
            return 5 - (person == null ? 0 : (int)person);
        }

        /// <summary>
        /// 检测最小开始人数
        /// </summary>
        private bool CheckedMinPreson()
        {
            int count = 0;
            while (true)
            {
                ClickMatchPerson();

                if (GetPresonCount() >= _minPreson)
                {
                    return true;
                }

                if (count++ % 5 == 0)
                {
                    SendNotify();
                }

                DelayLong();
            }
            return false;
        }

        /// <summary>
        /// 发送队伍广告
        /// </summary>
        private void SendNotify()
        {
            if (IsTeamView())
            {
                if (Bulid(FeatureLibrary.Instance.Dict["队伍_喊话"]))
                {
                    ClickRect(787, 553, 893, 588);
                }
                if (Bulid(FeatureLibrary.Instance.Dict["队伍_喊话"]))
                {
                    ClickRect(792, 412, 895, 453);
                }
                if (Bulid(FeatureLibrary.Instance.Dict["队伍_喊话"]))
                {
                    ClickRect(786, 341, 891, 383);
                }
            }
        }

        /// <summary>
        /// 获取组队等级下限
        /// </summary>
        private int GetMinLevel()
        {
            var result = _dm.Ocr(547, 270, 590, 298, "674c35-131312", 0.9);
            if (string.IsNullOrEmpty(result))
            {
                return -1;
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 获取组队等级上限
        /// </summary>
        /// <returns></returns>
        private int GetMaxLevel()
        {
            var result = _dm.Ocr(649, 267, 687, 297, "674c35-131312", 0.9);
            if (string.IsNullOrEmpty(result))
            {
                return -1;
            }
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// 检测等级下限
        /// </summary>
        private bool CheckedMinLevel()
        {
            return GetMinLevel() == _minLevel;
        }

        /// <summary>
        /// 检测等级上限
        /// </summary>
        private bool CheckedMaxLevel()
        {
            return GetMaxLevel() == _maxLevel;
        }

        /// <summary>
        /// 调整等级下限
        /// </summary>
        private bool SwipeMinLevel()
        {
            var currLvl = GetMinLevel();
            var lvl = currLvl - _minLevel;
            _dm.MoveTo(560, 285);
            if (lvl < 0)
            {
                lvl = Math.Abs(lvl);
                for (int i = 0; i < lvl; i++)
                {
                    _dm.WheelDown();
                    Delay(Rander.Instance.Next(25, 75));
                }
                return true;
            }
            else
            {
                for (int i = 0; i < lvl; i++)
                {
                    _dm.WheelUp();
                    Delay(Rander.Instance.Next(25, 75));
                }
                return true;
            }

            return false;
        }

        /// <summary>
        /// 调整等级上限
        /// </summary>
        private bool SwipeMaxLevel()
        {
            var currLvl = GetMaxLevel();
            var lvl = currLvl - _maxLevel;
            _dm.MoveTo(670, 285);
            if (lvl < 0)
            {
                lvl = Math.Abs(lvl);
                for (int i = 0; i < lvl; i++)
                {
                    _dm.WheelDown();
                    Delay(Rander.Instance.Next(25, 75));
                }
                return true;
            }
            else
            {
                for (int i = 0; i < lvl; i++)
                {
                    _dm.WheelUp();
                    Delay(Rander.Instance.Next(25, 75));
                }
                return true;
            }
        }

        /// <summary>
        /// 踢出离线队友
        /// </summary>
        private void KickAFK()
        {
            if (IsTeamView())
            {
                if (Bulid(FeatureLibrary.Instance.Dict["队伍_离线"]))
                {
                    DelayLong();
                    Bulid(FeatureLibrary.Instance.Dict["队伍_请离"]);
                    DelayLong();
                    ClickMatchPerson();
                    DelayLong();
                }
            }
        }
    }
}
