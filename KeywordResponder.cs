using System;
using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class KeywordResponder
    {
        private readonly Dictionary<string, List<string>> _responses = new();
        private readonly Random _random = new Random();

        public KeywordResponder()
        {
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            _responses["vpn"] = new List<string>
            {
                "A VPN (Virtual Private Network) encrypts your internet connection and hides your IP address. It's very useful on public Wi-Fi.",
                "Using a VPN protects your data from hackers on public networks and lets you access geo-blocked content safely."
            };

            _responses["phishing"] = new List<string>
            {
                "Never click links or download attachments from unknown senders. Always check the email address carefully.",
                "Phishing emails often create urgency or fear. If in doubt, contact the company directly through their official website."
            };

            _responses["password"] = new List<string>
            {
                "Use strong unique passwords (12+ characters) and a password manager.",
                "Never reuse the same password across multiple sites."
            };

            _responses["2fa"] = new List<string> { "Two-Factor Authentication adds a second layer of security. Enable it everywhere possible!" };
        }

        public string GetResponse(string input)
        {
            string lower = input.ToLower();
            foreach (var key in _responses.Keys)
            {
                if (lower.Contains(key))
                {
                    var list = _responses[key];
                    return list[_random.Next(list.Count)];
                }
            }
            return "";
        }
    }
}