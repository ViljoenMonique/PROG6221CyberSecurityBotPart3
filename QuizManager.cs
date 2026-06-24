//Improved QuizManager with 15 cybersecurity questions and scoring system
//Improved QuizManager with 15 cybersecurity questions and scoring system
using System;
using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class QuizManager
    {
        private readonly List<QuizQuestion> _questions = new List<QuizQuestion>();
        private int _currentQuestionIndex = 0;
        private int _score = 0;
        private bool _quizActive = false;

        public QuizManager()
        {
            InitializeQuiz();
        }

        private void InitializeQuiz()
        {
            // 15 Cybersecurity Questions (as required)
            _questions.Add(new QuizQuestion("What does 2FA stand for?", new[] { "Two-Factor Authentication", "Two-Firewall Access", "Two-Factor Account", "Two-Fast Access" }, 0));
            _questions.Add(new QuizQuestion("What is the best protection against phishing?", new[] { "Click all links quickly", "Never click unknown links", "Share your password", "Use same password" }, 1));
            _questions.Add(new QuizQuestion("What should you do if your account is hacked?", new[] { "Ignore it", "Change password + enable 2FA", "Tell everyone", "Do nothing" }, 1));
            _questions.Add(new QuizQuestion("What does HTTPS mean?", new[] { "HyperText Transfer Protocol Secure", "High Transfer Protocol", "Hidden Text", "Hyper Transfer" }, 0));
            _questions.Add(new QuizQuestion("Best characteristic of a strong password?", new[] { "Short and simple", "Mix of letters, numbers, symbols", "Your birthday", "Your name" }, 1));
            _questions.Add(new QuizQuestion("What is social engineering?", new[] { "Direct hacking", "Tricking people", "Building apps", "Strong passwords" }, 1));
            _questions.Add(new QuizQuestion("Why keep software updated?", new[] { "Looks nicer", "Patches security holes", "Uses more memory", "Slows computer" }, 1));
            _questions.Add(new QuizQuestion("Safest on public Wi-Fi?", new[] { "Use freely", "Use VPN", "Share info", "Turn off firewall" }, 1));
            _questions.Add(new QuizQuestion("Before clicking email link?", new[] { "Click immediately", "Hover to check URL", "Enter password", "Forward" }, 1));
            _questions.Add(new QuizQuestion("Purpose of password manager?", new[] { "Weaken passwords", "Store unique strong passwords", "Share passwords", "Slow login" }, 1));
            _questions.Add(new QuizQuestion("Padlock icon in browser?", new[] { "Slow site", "Secure HTTPS", "Old site", "Needs update" }, 1));
            _questions.Add(new QuizQuestion("Suspicious email asking for password?", new[] { "Send it", "Ask more details", "Delete/report", "Click link" }, 2));
            _questions.Add(new QuizQuestion("Risk of same password everywhere?", new[] { "Easier to remember", "One hack = all accounts", "Professional", "Saves time" }, 1));
            _questions.Add(new QuizQuestion("Good online safety habit?", new[] { "Share passwords", "Enable 2FA", "Click all links", "Public Wi-Fi banking" }, 1));
            _questions.Add(new QuizQuestion("Suspicious attachment?", new[] { "Open immediately", "Delete without opening", "Forward", "Save for later" }, 1));
        }

        public string StartQuiz()
        {
            _quizActive = true;
            _currentQuestionIndex = 0;
            _score = 0;
            return "🎮 **Cybersecurity Quiz Started!** Answer with A, B, C, or D.\n\n" + GetCurrentQuestion();
        }

        public string GetCurrentQuestion()
        {
            if (_currentQuestionIndex >= _questions.Count)
                return GetQuizResult();

            var q = _questions[_currentQuestionIndex];
            string text = $"**Question {_currentQuestionIndex + 1}/{_questions.Count}**\n{q.Question}\n\n";
            for (int i = 0; i < q.Options.Length; i++)
                text += $"{(char)('A' + i)}) {q.Options[i]}\n";
            return text;
        }

        public string ProcessAnswer(string answer)
        {
            if (!_quizActive) return "No active quiz. Type 'quiz' to start.";

            var q = _questions[_currentQuestionIndex];
            string userAns = answer.ToUpper().Trim();

            if (userAns.Length == 1 && userAns[0] >= 'A' && userAns[0] <= 'D')
            {
                int selected = userAns[0] - 'A';
                bool correct = selected == q.CorrectAnswer;

                if (correct) _score++;

                _currentQuestionIndex++;

                string feedback = correct ? "✅ Correct!\n\n" : $"❌ Wrong. Correct answer: {(char)('A' + q.CorrectAnswer)}\n\n";

                return feedback + (_currentQuestionIndex < _questions.Count ? GetCurrentQuestion() : GetQuizResult());
            }
            return "Please answer with A, B, C or D.";
        }

        private string GetQuizResult()
        {
            _quizActive = false;
            string result = $"🎉 **Quiz Complete!**\nYou scored {_score} out of {_questions.Count}!\n\n";
            if (_score >= 12) result += "🏆 Excellent! You're a cybersecurity pro!";
            else if (_score >= 8) result += "👍 Good job! Keep learning!";
            else result += "📚 Keep practicing safe online habits!";
            return result;
        }

        public bool IsQuizActive() => _quizActive;
    }

    public class QuizQuestion
    {
        public string Question { get; }
        public string[] Options { get; }
        public int CorrectAnswer { get; }

        public QuizQuestion(string question, string[] options, int correct)
        {
            Question = question;
            Options = options;
            CorrectAnswer = correct;
        }
    }
}
