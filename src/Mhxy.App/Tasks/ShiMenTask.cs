using System.Runtime.CompilerServices;
using Mhxy.Core;

namespace Mhxy.App.Tasks
{
    public class ShiMenTask : BaseTask
    {
        public ShiMenTask(Dmsoft dm)
            : base(dm)
        {
            TaskName = "师门";
        }

        #region ========================    入口

        protected override void OnMainView(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnMainView();
            if (SwitchTaskbar())
            {
                DelayLong();
            }

            if (!IsReceivedTask)
            {
                ReceiveTask();
            }
            else
            {
                if (NPCInteractive()) { }
                else if (Use()) { }
                else
                {
                    ClickTask();
                }
            }
        }

        protected override void OnBattled(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnBattled();
        }

        protected override void OnMoving(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnMoving();
        }

        protected override void OnOther(
            [CallerMemberName] string callerName = "",
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0
        )
        {
            base.OnOther();
            if (Bulid(FeatureLibrary.Instance.Dict["师门_完成"]))
            {
                Click(ScanX, ScanY);
                IsCompleted = true;
                IsRunning = false;
            }
            else if (Buy()) { }
            else if (Bulid(FeatureLibrary.Instance.Dict["师门_上交物品"])) { }
            else if (Skip()) { }
            KeyPressEsc();
        }
        #endregion

        protected override bool ClickTask()
        {
            base.ClickTask();
            var result = Bulid(FeatureLibrary.Instance.Dict["任务栏_师门"]);
            if (result)
            {
                Click(ScanX, ScanY);
                // IsReceivedTask = true;
            }
            return result;
        }

        protected override bool ReceiveTask()
        {
            base.ReceiveTask();
            // 检测主界面
            if (IsMainView())
            {
                // 打开秘籍界面
                OpenSecretView();
                DelayLong();
                // 点击师门任务
                ClickRect(781, 293, 883, 330);
                DelayLong();
                if (InvokeTimeout(ReceiveTaskFunc, 15))
                {
                    IsReceivedTask = true;
                    return true;
                }
            }
            return false;
        }

        private bool ReceiveTaskFunc()
        {
            if (NPCInteractive())
            {
                DelayLong();
            }
            return Bulid(FeatureLibrary.Instance.Dict["师门_继续任务"])
                || Bulid(FeatureLibrary.Instance.Dict["师门_去完成"]);
        }
    }
}
