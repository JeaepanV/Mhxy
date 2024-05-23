namespace Mhxy.App;

/// <summary>
/// 大漠插件配置
/// </summary>
public class DmsoftConfig
{
    /// <summary>
    /// 大漠插件免注册 DmReg.dll 路径
    /// </summary>
    public const string RegDllPath = @".\Resources\reg.dll";

    /// <summary>
    /// 大漠插件 dm.dll 路径
    /// </summary>
    public const string ClassDllPath = @".\Resources\dm.dll";

    /// <summary>
    /// 大漠插件注册码
    /// </summary>
    public static readonly string RegCode = "Jeaepana24a911d5b5da1e9f35314f3bbd7e40e";

    /// <summary>
    /// 大漠插件版本附加信息
    /// </summary>
    public static readonly string VerInfo = "Sham1ko2222";

    /// <summary>
    /// 大漠插件全局路径,设置了此路径后,所有接口调用中,相关的文件都相对于此路径. 比如图片,字库等.
    /// </summary>
    public const string GlobalPath = @"C:\Users\Sham1koMPC\Desktop\梦幻时空";
}
