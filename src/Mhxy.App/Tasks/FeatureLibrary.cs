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
                    ScanRegion = new Region(740, 98, 970, 587),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Height = 35, Width = 200 },
                    ColorCount = 6400,
                    BlockWidth = 220,
                    BlockHeight = 40,
                }
            );

            _dictionary.Add(
                "界面_药店",
                new Feature
                {
                    Name = "界面_药店",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_药店",
                    ScanRegion = new(474, 78, 545, 113),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new(757, 562, 856, 605)
                }
            );

            _dictionary.Add(
                "界面_兵器铺",
                new Feature
                {
                    Name = "界面_兵器铺",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_兵器铺",
                    ScanRegion = new(462, 80, 558, 113),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new(757, 561, 859, 605)
                }
            );

            _dictionary.Add(
                "界面_宠物店",
                new Feature
                {
                    Name = "界面_宠物店",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_宠物店",
                    ScanRegion = new(446, 77, 592, 114),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new(806, 615, 920, 661)
                }
            );

            _dictionary.Add(
                "界面_商会",
                new Feature
                {
                    Name = "界面_商会",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_商会",
                    ScanRegion = new(464, 79, 540, 123),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new(794, 623, 905, 664)
                }
            );

            _dictionary.Add(
                "界面_使用",
                new Feature
                {
                    Name = "界面_使用",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_使用",
                    ScanRegion = new(825, 622, 891, 657),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new(825, 622, 891, 657)
                }
            );

            _dictionary.Add(
                "界面_摆摊",
                new Feature
                {
                    Name = "界面_摆摊",
                    Mode = ScanMode.FindPic,
                    PicName = "界面_摆摊",
                    ScanRegion = new(461, 75, 542, 127),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new(794, 623, 905, 664)
                }
            );

            InitTeam();
            InitShiMen();
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

        private void InitShiMen()
        {
            _dictionary.Add(
                "师门_去完成",
                new Feature
                {
                    Name = "师门_去完成",
                    Mode = ScanMode.FindPic,
                    PicName = "师门_去完成",
                    ScanRegion = new(291, 420, 922, 485),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 60, Height = 20 }
                }
            );
            _dictionary.Add(
                "师门_继续任务",
                new Feature
                {
                    Name = "师门_继续任务",
                    Mode = ScanMode.FindPic,
                    PicName = "师门_继续任务",
                    ScanRegion = new(291, 420, 922, 485),
                    Clicked = true,
                    WaitTime = Rander.Instance.Next(1000, 2000),
                    ClickRegion = new Region { Width = 80, Height = 20 }
                }
            );
            _dictionary.Add(
                "任务栏_师门",
                new Feature
                {
                    Name = "任务栏_师门",
                    Mode = ScanMode.FindPic,
                    PicName = "任务栏_师门",
                    ScanRegion = new Region(802, 156, 1021, 515),
                    Clicked = true,
                    ClickRegion = new Region { Width = 30, Height = 30 }
                }
            );
            _dictionary.Add(
                "师门_上交物品",
                new Feature
                {
                    Name = "师门_上交物品",
                    Mode = ScanMode.FindPic,
                    PicName = "师门_上交物品",
                    ScanRegion = new Region(789, 565, 868, 613),
                    Clicked = true,
                    ClickRegion = new Region { Width = 30, Height = 30 }
                }
            );
            _dictionary.Add(
                "师门_完成",
                new Feature
                {
                    Name = "师门_完成",
                    Mode = ScanMode.FindPic,
                    PicName = "师门_完成",
                    ScanRegion = new Region(466, 564, 558, 603),
                    Clicked = true,
                    ClickRegion = new Region(466, 564, 558, 603)
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
