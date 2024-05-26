using System.Collections.Generic;
using Mhxy.Core;
using Mhxy.Core.Common;
using Mhxy.Core.Common.Enums;

namespace Mhxy.App.Tasks
{
    public class FeatureLibrary
    {
        private Dictionary<string, Feature> _dictionary;

        public FeatureLibrary()
        {
            _dictionary = new Dictionary<string, Feature>();
            _dictionary.Add(
                "界面_活动",
                new Feature
                {
                    Name = "界面_活动",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_活动",
                    ScanRegion = new(210, 0, 639, 69)
                }
            );
            _dictionary.Add(
                "界面_指引",
                new Feature
                {
                    Name = "界面_指引",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_指引",
                    ScanRegion = new(210, 0, 639, 69)
                }
            );
            _dictionary.Add(
                "界面_包裹",
                new Feature
                {
                    Name = "界面_包裹",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_包裹",
                    ScanRegion = new(450, 75, 549, 114)
                }
            );
            _dictionary.Add(
                "界面_队伍",
                new Feature
                {
                    Name = "界面_队伍",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_队伍",
                    ScanRegion = new(450, 75, 549, 114),
                }
            );
            _dictionary.Add(
                "界面_任务栏",
                new Feature
                {
                    Name = "界面_任务栏",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_任务栏",
                    ScanRegion = new(805, 100, 904, 144)
                }
            );
            _dictionary.Add(
                "界面_队伍栏",
                new Feature
                {
                    Name = "界面_队伍栏",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_队伍栏",
                    ScanRegion = new(920, 100, 1019, 144)
                }
            );
            _dictionary.Add(
                "界面_展开任务栏",
                new Feature
                {
                    Name = "界面_展开任务栏",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_展开任务栏",
                    ScanRegion = new(920, 100, 1019, 144),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(750, 1500),
                    ClickRegion = new(982, 114, 1011, 143)
                }
            );

            _dictionary.Add(
                "任务栏_捉鬼",
                new Feature
                {
                    Name = "任务栏_捉鬼",
                    Mode = ScanMode.FindPic,
                    PicName = "任务栏_捉鬼",
                    ScanRegion = new Region(802, 156, 1021, 515),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 30, Height = 30 }
                }
            );

            _dictionary.Add(
                "战斗_收缩",
                new Feature
                {
                    Name = "战斗_收缩",
                    Mode = ScanMode.FindPic,
                    PicName = "战斗_收缩",
                    ScanRegion = new(13, 31, 47, 65)
                }
            );

            _dictionary.Add(
                "战斗_展开",
                new Feature
                {
                    Name = "战斗_展开",
                    Mode = ScanMode.FindPic,
                    PicName = "战斗_展开",
                    ScanRegion = new(365, 30, 399, 64),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(750, 1500),
                    ClickRegion = new() { Width = 25, Height = 25 }
                }
            );

            _dictionary.Add(
                "界面_NPC交互",
                new Feature
                {
                    Name = "界面_NPC交互",
                    Mode = ScanMode.FindColorBlock,
                    DeltaColor = "EFD1A0-061022",
                    ScanRegion = new Region(798, 98, 987, 587),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Height = 40, Width = 160 },
                    ColorCount = 4800,
                    BlockWidth = 170,
                    BlockHeight = 40,
                }
            );

            InitTeam();
        }

        private void InitTeam()
        {
            _dictionary.Add(
                "队伍_创建",
                new Feature
                {
                    Name = "队伍_创建",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_创建",
                    ScanRegion = new Region(86, 627, 223, 669),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region(86, 627, 223, 669)
                }
            );
            _dictionary.Add(
                "队伍_捉鬼",
                new Feature
                {
                    Name = "队伍_捉鬼",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_捉鬼",
                    ScanRegion = new Region(263, 178, 435, 602),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 120, Height = 30 }
                }
            );
            _dictionary.Add(
                "队伍_喊话",
                new Feature
                {
                    Name = "队伍_喊话",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_喊话",
                    ScanRegion = new Region(767, 629, 915, 668),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region(767, 629, 915, 668)
                }
            );
            _dictionary.Add(
                "队伍_暂离",
                new Feature
                {
                    Name = "队伍_暂离",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_暂离",
                    ScanRegion = new(764, 627, 918, 669)
                }
            );
            _dictionary.Add(
                "队伍_离线",
                new Feature
                {
                    Name = "队伍_离线",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_离线",
                    ScanRegion = new(73, 451, 929, 484),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 70, Height = 30 }
                }
            );
            _dictionary.Add(
                "队伍_请离",
                new Feature
                {
                    Name = "队伍_请离",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_请离",
                    ScanRegion = new(58, 335, 939, 411),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 60, Height = 30 }
                }
            );
            _dictionary.Add(
                "队伍_匹配",
                new Feature
                {
                    Name = "队伍_匹配",
                    Mode = ScanMode.FindPic,
                    PicName = "队伍_匹配",
                    ScanRegion = new(806, 142, 918, 186),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 60, Height = 30 }
                }
            );
            _dictionary.Add(
                "队伍_助战",
                new Feature
                {
                    Name = "队伍_助战",
                    Mode = ScanMode.FindPicEx,
                    PicName = "队伍_助战",
                    ScanRegion = new Region(241, 201, 927, 242)
                }
            );
        }

        private static FeatureLibrary _instance;

        public static FeatureLibrary Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FeatureLibrary();
                }
                return _instance;
            }
            set
            {
                if (_instance == null)
                {
                    _instance = value;
                }
            }
        }

        public Dictionary<string, Feature> Dict
        {
            get => _dictionary;
            set => _dictionary = value;
        }
    }
}
