using System;
using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class ActivityLog
    {
        private readonly List<string> _log = new List<string>();

        public void AddAction(string action)
        {
            string entry = $"[{DateTime.Now:HH:mm:ss}] {action}";
            _log.Add(entry);
            if (_log.Count > 30) _log.RemoveAt(0);
        }

        public string GetLogSummary()
        {
            if (_log.Count == 0)
                return "No activities recorded yet.";

            string summary = "📜 **Recent Activity Log**\n\n";
            summary += "══════════════════════════════════════\n\n";

            for (int i = _log.Count - 1; i >= 0; i--)
            {
                summary += _log[i] + "\n";
            }

            summary += "\n══════════════════════════════════════\n";
            summary += $"Total actions logged: {_log.Count}";
            return summary;
        }
    }
}
