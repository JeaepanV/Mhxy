using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mhxy.Core;

public class Logger
{
    private string FormatMessage(
        string loggerType,
        string message,
        string callerName,
        string fileName,
        int line
    )
    {
        StringBuilder msg = new StringBuilder();
        msg.Append(DateTime.Now.ToString() + " ");
        msg.Append($"[{loggerType}] -> ");
        msg.Append(
            $"{Path.GetFileName(fileName)} > {callerName}() > in line[{line.ToString().PadLeft(2, ' ')}]: "
        );
        msg.Append(message);
        return msg.ToString();
    }

    private void Write(
        string loggerType,
        string message,
        string callerName,
        string fileName,
        int line
    )
    {
        message = FormatMessage(loggerType, message, callerName, fileName, line);
        Debug.WriteLine(message);
        Console.WriteLine(message);
    }

    public void WriteDebug(
        string message,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string fileName = "",
        [CallerLineNumber] int line = 0
    )
    {
        Write("DEBUG", message, callerName, fileName, line);
    }

    public void WriteInfo(
        string message,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string fileName = "",
        [CallerLineNumber] int line = 0
    )
    {
        Write("INFO", message, callerName, fileName, line);
    }

    public void WriteError(
        string message,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string fileName = "",
        [CallerLineNumber] int line = 0
    )
    {
        Write("ERROR", message, callerName, fileName, line);
    }

    public void WriteWarn(
        string message,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string fileName = "",
        [CallerLineNumber] int line = 0
    )
    {
        Write("WARN", message, callerName, fileName, line);
    }

    private static Logger _instance;
    public static Logger Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Logger();
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
}
