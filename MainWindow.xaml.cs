using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;

namespace CyberSecurityBot
{
    public partial class MainWindow : Window
    {
        private readonly Chatbot _chatbot = new Chatbot();
        private bool _greetingPlayed = false;

        public MainWindow()
        {
            InitializeComponent();
            StartBot();
        }

        private async void StartBot()
        {
            // Display ASCII Art Header
            AppendMessage("🤖 Your Cyber Friend", Colors.Cyan, true);
            AppendAsciiArt();

            // Play Voice Greeting
            if (!_greetingPlayed)
            {
                await Task.Run(() => _chatbot.PlayVoiceGreeting());
                _greetingPlayed = true;
            }

            AppendMessage("Hello! I'm your Cybersecurity Assistant.", Colors.White);
            AppendMessage("I can help with tasks, reminders, a quiz, and more.", Colors.LightGray);
            AppendMessage("What is your name?", Colors.LightBlue);
        }

        private void AppendAsciiArt()
        {
            var art = new Run(@"
  _____   ___   _   _   ____     ____   _   _   _____   ____   _   _   ____  
 / ___/  / _ \ | | | | / __ \   / __ \ | | | | |  _  \ / __ \ | | | | / __ \ 
/ /     | | | | | | | | |  | |  | |  | | | | | | | | | |  | | | | | | |  | |
| |     | |_| | |_| | | |__| |  | |__| | |_| | | |_| | | |__| | |_| | | |__| |
 \____/  \___/  \___/  \____/    \____/  \___/  |_____/  \____/  \___/   \____/ 

               Y O U R   C Y B E R   F R I E N D
")
            {
                Foreground = new SolidColorBrush(Color.FromRgb(103, 232, 249)),
                FontFamily = new FontFamily("Consolas"),
                FontSize = 8.5
            };

            tbChat.Inlines.Add(art);
            tbChat.Inlines.Add(new Run("\n" + "".PadRight(60, '=') + "\n\n") { Foreground = Brushes.Gray });
            chatScroll.ScrollToEnd();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void SendMessage()
        {
            string input = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            // Display user message
            AppendMessage($"You: {input}", Colors.LightBlue);

            // Get response from chatbot
            string response = _chatbot.ProcessInput(input);

            // Display bot response
            AppendMessage($"🤖 Cyber Friend: {response}", Colors.LightGreen);

            txtInput.Clear();
            chatScroll.ScrollToEnd();
        }

        private void AppendMessage(string message, Color color, bool bold = false)
        {
            var run = new Run(message + "\n\n")
            {
                Foreground = new SolidColorBrush(color),
                FontWeight = bold ? FontWeights.Bold : FontWeights.Normal
            };

            tbChat.Inlines.Add(run);
            chatScroll.ScrollToEnd();
        }

        // Exit Button
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using Your Cyber Friend!\nStay safe online! 👋", "Goodbye", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.Shutdown();
        }
    }
}