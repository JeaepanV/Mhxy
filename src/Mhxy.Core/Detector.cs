using System.IO;
using Mhxy.Core.Common;
using Mhxy.Core.Common.Enums;

namespace Mhxy.Core;

public class Detector
{
    private bool _isDebug = false;
    protected Dmsoft _dm;
    private Feature _feature;
    private int _scanX;
    private int _scanY;

    public int ScanX
    {
        get => _scanX;
        set => _scanX = value;
    }
    public int ScanY
    {
        get => _scanY;
        set => _scanY = value;
    }

    private List<Point> _points;

    public List<Point> Points
    {
        get { return _points; }
        set { _points = value; }
    }

    public bool IsDebug
    {
        get => _isDebug;
        set => _isDebug = value;
    }

    public Detector(Dmsoft dm)
    {
        _dm = dm;
        Reset();
    }

    public Detector Reset()
    {
        _feature = new Feature();
        _scanX = -1;
        _scanY = -1;
        _points = null;
        return this;
    }

    public Detector Mode(ScanMode mode)
    {
        _feature.Mode = mode;
        return this;
    }

    public Detector ScanRegion(int l, int t, int r, int b)
    {
        _feature.ScanRegion = new Region(l, t, r, b);
        return this;
    }

    public Detector ScanRegion(Region region)
    {
        _feature.ScanRegion = region;
        return this;
    }

    public Detector PicName(string picName)
    {
        _feature.PicName = picName;
        return this;
    }

    public Detector DeltaColor(string deltaColor)
    {
        _feature.DeltaColor = deltaColor;
        return this;
    }

    public Detector FirstColor(string firstColor)
    {
        _feature.FirstColor = firstColor;
        return this;
    }

    public Detector OffsetColor(string offsetColor)
    {
        _feature.OffsetColor = offsetColor;
        return this;
    }

    public Detector Threshold(double threshold)
    {
        _feature.Threshold = threshold;
        return this;
    }

    public Detector Clicked(bool clicked)
    {
        _feature.Clicked = clicked;
        return this;
    }

    public Detector ClickRegion(int l, int t, int r, int b)
    {
        _feature.ClickRegion = new Region(l, t, r, b);
        return this;
    }

    public Detector ClickRegion(Region region)
    {
        _feature.ClickRegion = region;
        return this;
    }

    public Detector WaitTime(int time)
    {
        _feature.WaitTime = time;
        return this;
    }

    public Detector ColorCount(int count)
    {
        _feature.ColorCount = count;
        return this;
    }

    public Detector BlockWidth(int width)
    {
        _feature.BlockWidth = width;
        return this;
    }

    public Detector BlockHeight(int height)
    {
        _feature.BlockHeight = height;
        return this;
    }

    public Detector DetectStr(string str)
    {
        _feature.DetectStr = str;
        return this;
    }

    public bool Bulid()
    {
        if (_feature == null)
            return false;

        var result = false;
        var region = _feature.ScanRegion;
        int l = (int)region.Left;
        int t = (int)region.Top;
        int r = (int)region.Right;
        int b = (int)region.Bottom;

        if (_feature.Mode == ScanMode.FindPic)
        {
            result = FindPic(l, t, r, b, _feature.PicName, _feature.DeltaColor, _feature.Threshold);
        }
        else if (_feature.Mode == ScanMode.FindPicEx)
        {
            result = FindPicEx(
                l,
                t,
                r,
                b,
                _feature.PicName,
                _feature.DeltaColor,
                _feature.Threshold
            );
        }
        else if (_feature.Mode == ScanMode.FindMultiColor)
        {
            result = FindMultiColor(
                l,
                t,
                r,
                b,
                _feature.FirstColor,
                _feature.OffsetColor,
                _feature.Threshold
            );
        }
        else if (_feature.Mode == ScanMode.FindColorBlock)
        {
            result = FindColorBlock(
                l,
                t,
                r,
                b,
                _feature.DeltaColor,
                _feature.ColorCount,
                _feature.BlockWidth,
                _feature.BlockHeight,
                _feature.Threshold
            );
            //Logger.Instance.WriteInfo(
            //    $"{result}, {l}, {t}, {r}, {b}, {_feature.DeltaColor}, {_feature.ColorCount}, {_feature.BlockWidth}, {_feature.BlockHeight}"
            //);
        }
        else if (_feature.Mode == ScanMode.FindStr)
        {
            result = FindStr(
                l,
                t,
                r,
                b,
                _feature.DetectStr,
                _feature.DeltaColor,
                _feature.Threshold
            );
        }

        if (IsDebug)
        {
            Logger.Instance.WriteDebug($"{_feature.Name}: {result}, {_scanX}, {_scanY}");
        }

        if (result)
        {
            if (_feature.Clicked)
            {
                ClickRect(_feature.ClickRegion);
                if (_feature.WaitTime > 0)
                {
                    Delay(_feature.WaitTime);
                }
            }
            return result;
        }

        return false;
    }

    public bool Bulid(Feature feature)
    {
        Reset();
        _feature = feature;
        return Bulid();
    }

    public bool FindPic(
        int l,
        int t,
        int r,
        int b,
        string name,
        string deltaColor,
        double threshold
    )
    {
        if (string.IsNullOrEmpty(Path.GetExtension(name)))
        {
            name += ".bmp";
        }

        var result = _dm.FindPic(
            l,
            t,
            r,
            b,
            name,
            deltaColor,
            threshold,
            0,
            out _scanX,
            out _scanY
        );
        if (result > -1)
        {
            return true;
        }
        return false;
    }

    public bool FindPicEx(
        int l,
        int t,
        int r,
        int b,
        string name,
        string deltaColor,
        double threshold
    )
    {
        if (string.IsNullOrEmpty(Path.GetExtension(name)))
        {
            name += ".bmp";
        }

        var result = _dm.FindPicEx(l, t, r, b, name, deltaColor, threshold, 0);
        if (string.IsNullOrEmpty(result))
        {
            return false;
        }

        var datas = result.Split("|");
        _points = new();
        for (int i = 0; i < datas.Length; i++)
        {
            var item = datas[i].Split(",");
            _points.Add(
                new Point(
                    Convert.ToInt32(item[0]),
                    Convert.ToInt32(item[1]),
                    Convert.ToInt32(item[2])
                )
            );
        }

        return true;
    }

    public bool FindMultiColor(
        int l,
        int t,
        int r,
        int b,
        string firstColor,
        string offsetColor,
        double threshold
    )
    {
        var result = _dm.FindMultiColor(
            l,
            t,
            r,
            b,
            firstColor,
            offsetColor,
            threshold,
            0,
            out _scanX,
            out _scanY
        );
        if (result > 0)
        {
            return true;
        }
        return false;
    }

    public bool FindColorBlock(
        int l,
        int t,
        int r,
        int b,
        string deltaColor,
        int count,
        int width,
        int height,
        double threshold
    )
    {
        var result = _dm.FindColorBlock(
            l,
            t,
            r,
            b,
            deltaColor,
            threshold,
            count,
            width,
            height,
            out _scanX,
            out _scanY
        );
        if (result > 0)
        {
            return true;
        }
        return false;
    }

    public bool FindStr(
        int l,
        int t,
        int r,
        int b,
        string value,
        string deltaColor,
        double threshold
    )
    {
        var result = _dm.FindStr(l, t, r, b, value, deltaColor, threshold, out _scanX, out _scanY);
        if (result > -1)
        {
            return true;
        }
        return false;
    }

    public Detector Click(int x, int y)
    {
        _dm.MoveTo(x, y);
        _dm.LeftClick();
        return this;
    }

    public Detector ClickRect(int l, int t, int r, int b)
    {
        _dm.MoveTo(Rander.Instance.Next(l, r), Rander.Instance.Next(t, b));
        _dm.LeftClick();
        return this;
    }

    public Detector ClickRect(Region region)
    {
        if (region == null)
        {
            Click(_scanX, _scanY);
            return this;
        }

        if (
            region.Left != null
            && region.Top != null
            && region.Right != null
            && region.Bottom != null
        )
        {
            ClickRect((int)region.Left, (int)region.Top, (int)region.Right, (int)region.Bottom);
            return this;
        }

        //if (region.Left != null) _scanX = (int)region.Left;
        //if (region.Top != null) _scanY = (int)region.Top;

        if (region.Width == null && region.Height == null)
        {
            Click(_scanX, _scanY);
            return this;
        }

        ClickRect(_scanX, _scanY, _scanX + (int)region.Width, _scanY + (int)region.Height);
        return this;
    }

    public virtual Detector Delay(int time)
    {
        Thread.Sleep(time);
        return this;
    }
}
