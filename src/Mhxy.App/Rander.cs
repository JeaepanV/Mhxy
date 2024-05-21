using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mhxy.App;

public class Rander
{
    private Random _random;

    public Rander()
    {
        _random = new Random((int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
    }

    public int Next(int max)
    {
         return _random.Next(max);
    }

    public int Next(int min, int max)
    {
        return _random.Next(min, max);
    }


    private static Rander _instance;


    public static Rander Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Rander();
            }
            return _instance;
        }
        set { Instance = value; }
    }


}
