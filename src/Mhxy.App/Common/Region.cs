namespace Mhxy.App.Common
{
    public class Region
    {
        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Right { get; set; }
        public int? Bottom { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public Region() { }

        public Region(int l, int t, int r, int b)
        {
            Left = l;
            Top = t;
            Right = r;
            Bottom = b;
        }
    }
}
