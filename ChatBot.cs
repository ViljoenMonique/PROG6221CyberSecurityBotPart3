using System;

namespace CyberSecurityBot
{
    public class Chatbot
    {
        private readonly AudioPlayer _audioPlayer = new AudioPlayer();
        private readonly MemoryStore _memory = new MemoryStore();
        private readonly SentimentDetector _sentiment = new SentimentDetector();
        private readonly KeywordResponder _keywords = new KeywordResponder();
        private readonly ActivityLog _activityLog = new ActivityLog();
        private readonly QuizManager _quiz = new QuizManager();

        public void PlayVoiceGreeting() => _audioPlayer.PlayGreeting();

        public string ProcessInput(string input)
        {
            string lower = input.ToLower().Trim();
            _activityLog.AddAction($"User: {input}");

            if (lower == "exit" || lower == "quit" || lower == "close")
                return "Goodbye! Stay safe online! 👋";

            // Name Memory
            if (_memory.UserName == null)
            {
                string name = ExtractName(input);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    _memory.UserName = name;
                    _activityLog.AddAction($"User name saved: {name}");
                    return $"Nice to meet you, {name}! How can I help you today?";
                }
            }

            // Special Greeting - Moved higher
            if (lower.Contains("how are you") || lower.Contains("how r you") || lower == "how are you doing")
            {
                _activityLog.AddAction("Greeting responded");
                return "I'm doing great, thank you for asking! 😊 I'm here to help you stay safe online. What would you like to do?";
            }

            // Task Assistant
            if (lower.Contains("add task") || lower.Contains("remind") || lower.Contains("set reminder"))
            {
                _memory.AddTask(input);
                _activityLog.AddAction($"Task added: {input}");
                return "✅ Task added successfully! Type 'show tasks' to view them.";
            }

            if (lower.Contains("show tasks") || lower.Contains("my tasks"))
            {
                _activityLog.AddAction("Tasks viewed");
                return _memory.GetTasksSummary();
            }

            // Task Management
            if (lower.StartsWith("complete ") && int.TryParse(lower.Replace("complete ", "").Trim(), out int completeId))
            {
                if (_memory.MarkComplete(completeId))
                    return $"✅ Task {completeId} marked as completed!";
            }

            if (lower.StartsWith("delete ") && int.TryParse(lower.Replace("delete ", "").Trim(), out int deleteId))
            {
                if (_memory.DeleteTask(deleteId))
                    return $"🗑️ Task {deleteId} deleted successfully.";
            }

            // Quiz
            if (lower.Contains("quiz") || lower.Contains("game") || lower.Contains("test me"))
            {
                _activityLog.AddAction("Quiz started");
                return _quiz.StartQuiz();
            }

            if (_quiz.IsQuizActive())
                return _quiz.ProcessAnswer(input);

            // Activity Log
            if (lower.Contains("activity log") || lower.Contains("what have you done") || lower.Contains("history"))
            {
                _activityLog.AddAction("Activity log viewed");
                return _activityLog.GetLogSummary();
            }

            string greeting = !string.IsNullOrEmpty(_memory.UserName) ? $"{_memory.UserName}, " : "";

            // Keywords
            string? keywordReply = _keywords.GetResponse(input);
            if (!string.IsNullOrEmpty(keywordReply))
            {
                _activityLog.AddAction("Keyword response");
                return greeting + keywordReply;
            }

            // Sentiment
            string sentimentReply = _sentiment.GetResponse(input);
            if (!string.IsNullOrEmpty(sentimentReply))
            {
                _activityLog.AddAction("Sentiment response");
                return greeting + sentimentReply;
            }

            // Default
            _activityLog.AddAction("Default response");
            return greeting + "Good question! You can say 'add task', 'show tasks', 'start quiz', 'activity log', or ask me about VPN, phishing, or 2FA.";
        }

        private string ExtractName(string input)
        {
            string lower = input.ToLower();
            if (lower.Contains("my name is")) return input.Substring(lower.IndexOf("my name is") + 10).Trim();
            if (lower.Contains("name is")) return input.Substring(lower.IndexOf("name is") + 8).Trim();
            if (lower.Contains("i am")) return input.Substring(lower.IndexOf("i am") + 4).Trim();
            if (input.Split(' ').Length <= 3 && input.Length > 2) return input.Trim();
            return "";
        }
    }
}