using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class AdvancedLogger
{
    private StringBuilder _logStorage = new StringBuilder();
    private List<string> _logLines = new List<string>();

    public void Log(string level, string message)
    {
        string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level.ToUpper()}] {message}";
        _logStorage.AppendLine(logEntry);
        _logLines.Add(logEntry);
    }

    public void SaveToFile(string path)
    {
        File.WriteAllText(path, _logStorage.ToString());
    }

    public string GetLogsByLevel(string level)
    {
        var filteredLogs = _logLines.Where(log => log.Contains($"[{level.ToUpper()}]", StringComparison.OrdinalIgnoreCase));
        return string.Join(Environment.NewLine, filteredLogs);
    }

    public void Clear()
    {
        _logStorage.Clear();
        _logLines.Clear();
    }

    public string GetLast(int count)
    {
        var lastLogs = _logLines.Skip(Math.Max(0, _logLines.Count - count));
        return string.Join(Environment.NewLine, lastLogs);
    }
}