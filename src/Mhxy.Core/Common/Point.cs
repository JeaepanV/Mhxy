namespace Mhxy.Core.Common
{
    public class Point
    {
        public int Index { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Point() { }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(int id, int x, int y)
        {
            Index = id;
            X = x;
            Y = y;
        }
    }
}
