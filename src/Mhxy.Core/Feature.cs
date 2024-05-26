using Mhxy.Core.Common;
using Mhxy.Core.Common.Enums;

namespace Mhxy.Core;

public class Feature
{
    /// <summary>
    /// 特征名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 检测模式
    /// </summary>
    public ScanMode Mode { get; set; }

    /// <summary>
    /// 检测区域
    /// </summary>
    public Region ScanRegion { get; set; } = new Region(0, 0, 2000, 2000);

    /// <summary>
    /// 图片名
    /// </summary>
    public string PicName { get; set; }

    /// <summary>
    /// 偏色
    /// </summary>
    public string DeltaColor { get; set; } = "202020";

    /// <summary>
    /// 第一点颜色
    /// </summary>
    public string FirstColor { get; set; }

    /// <summary>
    /// 偏移颜色
    /// </summary>
    public string OffsetColor { get; set; }

    /// <summary>
    /// 相似度
    /// </summary>
    public double Threshold { get; set; } = 0.9;

    /// <summary>
    /// 是否点击
    /// </summary>
    public bool Clicked { get; set; } = false;

    /// <summary>
    /// 点击区域
    /// </summary>
    public Region ClickRegion { get; set; } = null;

    /// <summary>
    /// 等待时间
    /// </summary>
    public int WaitTime { get; set; } = 0;

    /// <summary>
    /// 色块颜色
    /// </summary>
    public int ColorCount { get; set; }

    /// <summary>
    /// 色块宽度
    /// </summary>
    public int BlockWidth { get; set; }

    /// <summary>
    /// 色块高度
    /// </summary>
    public int BlockHeight { get; set; }

    /// <summary>
    /// 找字
    /// </summary>
    public string DetectStr { get; set; }
}
