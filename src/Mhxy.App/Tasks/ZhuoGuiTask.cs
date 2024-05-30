using System;
using System.Runtime.CompilerServices;
using Mhxy.Core;

namespace Mhxy.App.Tasks
{
    public class ZhuoGuiTask : BaseTask
    {
        #region ========================    字段
        // 是否已经初始化队伍等级
        private bool _isInitTeamLevle = false;

        // 是否初次进入战斗
        private bool _isInitBattled = false;

        // 是否已经领双
        private bool _isReceiveDoubleExp = false;

        // 队伍等级下限
        private int _minLevel = 69;

        // 队伍等级上限
        private int _maxLevel = 89;

        // 任务最低开始人数
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
            // 切换任务栏
            if (SwitchTaskbar())
            {
                DelayLong();
            }
            // 点击任务栏捉鬼任务
            if (!ClickTask())
            {
                // 创建队伍
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
                // 打开队伍界面
                OpenTeamView();
                // 检测最低人员
                CheckedMinPreson();
                // 请离离线人员
                KickAFK();
                // 退出队伍界面
                KeyPressEsc();
                // 是否已经领双
                if (!_isReceiveDoubleExp)
                {
                    // 领双
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
        }
        #endregion

        /// <summary>
        /// 点击任务
        /// </summary>
        /// <returns></returns>
        protected override bool ClickTask()
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
        protected override bool ReceiveTask()
        {
            base.ReceiveTask();
            IsReceivedTask = true;
            if (IsMainView())
            {
                // 打开秘籍界面
                OpenSecretView();
                DelayLong();
                // 点击捉鬼任务
                ClickRect(771, 395, 890, 429);
                DelayLong();
                // 等待NPC交互领取捉鬼任务
                InvokeTimeout(NPCInteractive, 60);
                // 点击任务栏捉鬼任务
                ClickTask();
                // 二次点击任务栏捉鬼任务
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
            // 打开领双界面
            OpenDoubleExpView();
            DelayLong();
            // 点击冻结双
            ClickRect(562, 615, 625, 653);
            DelayShort();
            // 点击领取双
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
            // 打开队伍界面
            OpenTeamView();

            // 检测是否是队长
            if (!IsTeamLeader())
            {
                // 如果不是队长则退出退伍
                ClickRect(88, 631, 218, 667);
                DelayShort();
            }

            // 点击队伍创建
            if (Bulid(FeatureLibrary.Instance.Dict["队伍_创建"]))
            {
                DelayLong();
            }

            // 点击调整目标
            ClickRect(685, 150, 714, 179);
            DelayShort();

            // 是否已经初始化等级
            if (!_isInitTeamLevle)
            {
                // 检测等级下限以及等级上限是否已经设置好
                if (!CheckedMinLevel() && !CheckedMaxLevel())
                {
                    // 设置任务目标位捉鬼
                    Bulid(FeatureLibrary.Instance.Dict["队伍_捉鬼"]);
                    DelayShort();
                }
                // 检测等级下限
                if (!CheckedMinLevel())
                {
                    // 设置等级下限
                    SwipeMinLevel();
                    DelayShort();
                }
                // 检查等级上限
                if (!CheckedMaxLevel())
                {
                    // 设置等级上限
                    SwipeMaxLevel();
                    DelayShort();
                }
            }
            if (CheckedMinLevel() && CheckedMaxLevel())
            {
                _isInitTeamLevle = true;
            }
            // 点击确定调整
            ClickRect(596, 630, 687, 668);
            DelayShort();
            // 检测最低开始任务人数
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
                // 点击自动匹配队友
                ClickMatchPerson();
                /// 获取当前队伍人数并比较最低人数
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
            // 检测队伍界面
            if (IsTeamView())
            {
                // 检测离线人员并点击
                if (Bulid(FeatureLibrary.Instance.Dict["队伍_离线"]))
                {
                    DelayLong();
                    // 点击请离队伍
                    Bulid(FeatureLibrary.Instance.Dict["队伍_请离"]);
                    DelayLong();
                    // 点击自动匹配队友
                    ClickMatchPerson();
                    DelayLong();
                }
            }
        }
    }
}
