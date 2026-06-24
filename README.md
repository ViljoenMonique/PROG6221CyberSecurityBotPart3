# Cybersecurity Awareness Bot - Part 3 (POE)

**PROG6221 - Programming 2A**  
**Student:** Monique Viljoen

A comprehensive **WPF** Cybersecurity Awareness Chatbot with advanced features including persistent Task Management using SQLite.

## ✨ Features

- Voice Greeting on startup (`greeting.wav`)
- Custom ASCII Art displayed in the GUI header
- User Name Memory
- **Task Assistant** with SQLite Database (Add, View, Complete, Delete tasks)
- **Cybersecurity Mini Quiz** (15 questions with scoring and feedback)
- **Activity Log** with timestamps for all user actions
- Sentiment Detection
- Keyword Recognition (VPN, Phishing, Passwords, 2FA, etc.)
- Smart command handling (`complete 1`, `delete 2`, etc.)

## Project Structure
CyberSecurityBotPart3/
├── Assets/
│   └── greeting.wav
├── ChatBot.cs
├── MainWindow.xaml
├── MainWindow.xaml.cs
├── TaskDatabase.cs
├── MemoryStore.cs
├── ActivityLog.cs
├── QuizManager.cs
├── KeywordResponder.cs
├── SentimentDetector.cs
├── AudioPlayer.cs
├── README.md
└── CyberSecurityBotPart3.sln
text## How to Run

1. Open `CyberSecurityBotPart3.sln` in Visual Studio 2022
2. Ensure `greeting.wav` is in the `Assets` folder and set to **Copy Always**
3. Build and Run the project (**F5**)

## Technologies Used

- C# .NET 8.0 + WPF
- SQLite Database (`Microsoft.Data.Sqlite`)
- Object-Oriented Programming (Clean class separation)

---
