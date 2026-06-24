//Enhanced ActivityLog to properly track all user actions including tasks

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
            if (_log.Count > 25) _log.RemoveAt(0);
        }

        public string GetLogSummary()
        {
            if (_log.Count == 0)
                return "No activities recorded yet.";

            string summary = "📜 **Recent Activity Log**\n\n";
            foreach (var entry in _log)
            {
                summary += entry + "\n";
            }
            return summary;
        }
    }
}
