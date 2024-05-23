// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using Mhxy.App;
using Mhxy.App.Helpers;
using Mhxy.App.Tasks;

Dmsoft dm;
int hwnd = 1573236;
if (DmsoftRegister.RegisterDmSoftDllA())
{
    dm = new Dmsoft();
    var regCode = dm.Reg(DmsoftConfig.RegCode, DmsoftConfig.VerInfo);
    Console.WriteLine($"大漠收费注册: {regCode}");

    var result = dm.BindWindowEx(
        hwnd,
        "dx.graphic.3d.10plus",
        "dx.mouse.api",
        "dx.keypad.api",
        "",
        0
    );

    //Logger.Instance.WriteInfo(dm.SetWindowSize(hwnd, 800, 600).ToString());
    //dm.MoveWindow(hwnd, 0, 0);

    dm.SetPath(DmsoftConfig.GlobalPath);

    var task = new ZhuoGuiTask(dm);

    task.Start();
}

void OnProcessExit(object sender, EventArgs e)
{
    Logger.Instance.WriteDebug("OnProcessExit");
    dm.UnBindWindow();
}
