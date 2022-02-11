using System;
using System.Text;
using Code.Framework.Enums;
using UnityEngine;

namespace Code.Framework.Logger
{
  public static class Log
  {
    private static readonly LogMaskThreshold _currentLogMaskThreshold = LogMaskThreshold.Debugging;
    private static readonly StringBuilder StringBuilder = new(500);
    
    private static bool PrepareLog(string header, string msg, LogMask logMask, string[] tags)
    {
      if (Log._currentLogMaskThreshold > (LogMaskThreshold) logMask)
        return false;
      Log.StringBuilder.Clear();
      if (!string.IsNullOrEmpty(header))
      {
        int num = Math.Abs(header.GetHashCode()) % 1000;
        string htmlStringRgb = ColorUtility.ToHtmlStringRGB(Color.HSVToRGB( num / 1000f, 1f, (float) ( (num * 89 % 100) / 200.0 + 0.5)));
        Log.StringBuilder.Append("<b><color=#");
        Log.StringBuilder.Append(htmlStringRgb);
        Log.StringBuilder.Append(">[");
        Log.StringBuilder.Append(header);
        Log.StringBuilder.Append("]</color></b> ");
      }
      Log.StringBuilder.Append(msg);
      if (tags != null && tags.Length != 0)
      {
        Log.StringBuilder.Append("\n<b><color=#c36800ff>[");
        Log.StringBuilder.Append(tags[0]);
        for (int index = 1; index < tags.Length; ++index)
        {
          Log.StringBuilder.Append(", ");
          Log.StringBuilder.Append(tags[index]);
        }
        Log.StringBuilder.Append("]</color></b>");
      }
      return true;
    }

    public static void Message(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog( null, msg, logMask, tags))
        return;
      Debug.Log(Log.StringBuilder.ToString());
    }

    public static void Print(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.Log(Log.StringBuilder.ToString());
    }

    public static void Warning(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(null, msg, logMask, tags))
        return;
      Debug.LogWarning(Log.StringBuilder.ToString());
    }

    public static void Warning(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.LogWarning(Log.StringBuilder.ToString());
    }

    public static void Error(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(null, msg, logMask, tags))
        return;
      Debug.LogError(Log.StringBuilder.ToString());
    }

    public static void Error(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.LogError(Log.StringBuilder.ToString());
    }

    public static void ExceptionString(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(null, msg, logMask, tags))
        return;
      Debug.LogException(new Exception(Log.StringBuilder.ToString()));
    }

    public static void ExceptionString(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.LogException(new Exception(Log.StringBuilder.ToString()));
    }

    public static void Exception(Exception exception, LogMask logMask = LogMask.Debugging)
    {
      if (Log._currentLogMaskThreshold > (LogMaskThreshold) logMask)
        return;
      Debug.LogException(exception);
    }
  }
}
