using System;
using System.Text;
using Code.Framework.Enums;
using UnityEngine;

namespace Code.Framework.Logger
{
  public static class Log
  {
    private static LogMaskThreshold CurrentLogMaskThreshold = LogMaskThreshold.Debugging;
    private static readonly StringBuilder m_StringBuilder = new StringBuilder(500);

    private static bool PrepareLog(string header, string msg, LogMask logMask, string[] tags)
    {
      if (Log.CurrentLogMaskThreshold > (LogMaskThreshold) logMask)
        return false;
      Log.m_StringBuilder.Clear();
      if (!string.IsNullOrEmpty(header))
      {
        int num = Math.Abs(header.GetHashCode()) % 1000;
        string htmlStringRgb = ColorUtility.ToHtmlStringRGB(Color.HSVToRGB( num / 1000f, 1f, (float) ( (num * 89 % 100) / 200.0 + 0.5)));
        Log.m_StringBuilder.Append("<b><color=#");
        Log.m_StringBuilder.Append(htmlStringRgb);
        Log.m_StringBuilder.Append(">[");
        Log.m_StringBuilder.Append(header);
        Log.m_StringBuilder.Append("]</color></b> ");
      }
      Log.m_StringBuilder.Append(msg);
      if (tags != null && tags.Length != 0)
      {
        Log.m_StringBuilder.Append("\n<b><color=#c36800ff>[");
        Log.m_StringBuilder.Append(tags[0]);
        for (int index = 1; index < tags.Length; ++index)
        {
          Log.m_StringBuilder.Append(", ");
          Log.m_StringBuilder.Append(tags[index]);
        }
        Log.m_StringBuilder.Append("]</color></b>");
      }
      return true;
    }

    public static void Message(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog( null, msg, logMask, tags))
        return;
      Debug.Log(Log.m_StringBuilder.ToString());
    }

    public static void Message(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.Log(Log.m_StringBuilder.ToString());
    }

    public static void Warning(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(null, msg, logMask, tags))
        return;
      Debug.LogWarning(Log.m_StringBuilder.ToString());
    }

    public static void Warning(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.LogWarning(Log.m_StringBuilder.ToString());
    }

    public static void Error(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(null, msg, logMask, tags))
        return;
      Debug.LogError(Log.m_StringBuilder.ToString());
    }

    public static void Error(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.LogError(Log.m_StringBuilder.ToString());
    }

    public static void ExceptionString(string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(null, msg, logMask, tags))
        return;
      Debug.LogException(new Exception(Log.m_StringBuilder.ToString()));
    }

    public static void ExceptionString(string header, string msg, LogMask logMask = LogMask.Debugging, string[] tags = null)
    {
      if (!Log.PrepareLog(header, msg, logMask, tags))
        return;
      Debug.LogException(new Exception(Log.m_StringBuilder.ToString()));
    }

    public static void Exception(Exception exception, LogMask logMask = LogMask.Debugging)
    {
      if (Log.CurrentLogMaskThreshold > (LogMaskThreshold) logMask)
        return;
      Debug.LogException(exception);
    }
  }
}
