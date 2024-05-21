using System.Runtime.InteropServices;

namespace Mhxy.App;

/// <summary>
/// 免注册调用大漠插件
/// </summary>
public static class DmsoftRegister
{
    // 不注册调用大漠插件，实际上是使用 dmreg.dll 来配合实现
    // 这个文件有 2 个导出接口 SetDllPathW 和 SetDllPathA
    // SetDllPathA 对应 ascii 接口。
    [DllImport(DmsoftConfig.RegDllPath)]
    private static extern int SetDllPathA(string path, int mode);

    // SetDllPathW 对应 unicode
    [DllImport(DmsoftConfig.RegDllPath)]
    private static extern int SetDllPathW(string path, int mode);

    /// <summary>
    /// 免注册调用大漠插件
    /// </summary>
    /// <returns></returns>
    public static bool RegisterDmSoftDllA()
    {
        var setDllPathResult = SetDllPathA(DmsoftConfig.ClassDllPath, 1);
        if (setDllPathResult == 0)
        {
            // 加载 dm.dll 失败
            return false;
        }

        return true;
    }

    /// <summary>
    /// 免注册调用大漠插件
    /// </summary>
    /// <returns></returns>
    public static bool RegisterDmSoftDllW()
    {
        var setDllPathResult = SetDllPathW(DmsoftConfig.ClassDllPath, 1);
        if (setDllPathResult == 0)
        {
            // 加载 dm.dll 失败
            return false;
        }

        return true;
    }
}
