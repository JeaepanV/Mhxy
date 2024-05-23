using System.Runtime.CompilerServices;
using Mhxy.App.Common;
using Mhxy.App.Common.Enums;

namespace Mhxy.App.Tasks;

public class ZhuoGuiTask : BaseTask
{
    private bool _isReceiveDoubleExp = false;

    private Feature _featureTask = new Feature
    {
        Name = "任务栏_捉鬼",
        Mode = DetectMode.FindPic,
        PicName = "任务栏_捉鬼",
        DetectRegion = new Region(802, 156, 1021, 515),
        Clicked = true,
        WaitTime = Rander.Instance.Next(1000, 2000),
        ClickRegion = new Region { Width = 30, Height = 30 }
    };

    // Alt+G 领双界面
    // Alt+H 指引界面
    // Alt+T 队伍界面
    public ZhuoGuiTask(Dmsoft dm)
        : base(dm)
    {
        _taskName = "捉鬼";
    }

    private bool ClickTask()
    {
        var result = _detector.Bulid(_featureTask);
        return result;
    }

    protected override void OnBattled(
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string fileName = "",
        [CallerLineNumber] int line = 0
    )
    {
        base.OnBattled();

        if (!_isReceiveDoubleExp)
        {
            KeyPressAndAlt(71);
            DelayLong();
            _detector.ClickRect(560, 614, 628, 654);
            DelayLong();
            _detector.ClickRect(816, 619, 890, 653);
            DelayLong();
            _isReceiveDoubleExp = true;
        }
    }

    protected override void OnMainView(
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string fileName = "",
        [CallerLineNumber] int line = 0
    )
    {
        base.OnMainView();

        ExpandTaskbar();
        if (!IsTaskbar())
        {
            SwitchTaskbar();
        }

        if (!ClickTask())
        {
            ReceiveTask();
        }
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
        base.OnOther(callerName, fileName, line);
        KeyPressEsc();
    }

    /// <summary>
    /// 领任务
    /// </summary>
    /// <returns></returns>
    private bool ReceiveTask()
    {
        KeyPressAndAlt(72);
        DelayLong();
        _detector.ClickRect(947, 460, 973, 530);
        DelayLong();
        _detector.ClickRect(787, 393, 879, 429);
        DelayLong();
        InvokeTimeout(NPCInteractive, 30);
        _isReceiveDoubleExp = false;
        return true;
    }
}
