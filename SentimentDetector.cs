using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class SentimentDetector
    {
        private readonly Dictionary<string, string> _responses = new();

        public SentimentDetector()
        {
            _responses["worried"] = "I'm really sorry you're feeling worried — that's completely understandable. Let's protect you: Never click suspicious links, always check the sender's email address, and enable 2FA wherever possible. I'm here to help you step by step.";

            _responses["curious"] = "Great question! I'm happy to explain in more detail.";
            _responses["scared"] = "I understand this can feel scary. Take a deep breath — we're going to handle this together and keep you safe.";
            _responses["frustrated"] = "I understand it's frustrating. Let me explain it more clearly and simply.";
        }

        public string GetResponse(string input)
        {
            string lower = input.ToLower();
            foreach (var word in _responses.Keys)
            {
                if (lower.Contains(word))
                    return _responses[word];
            }
            return "";
        }
    }
}