using System.Collections.Generic;

namespace CyberSecurityBot
{
    public class MemoryStore
    {
        public string? UserName { get; set; }
        public string? FavouriteTopic { get; set; }

        private readonly TaskDatabase _db = new TaskDatabase();

        public void AddTask(string taskDescription) => _db.AddTask(taskDescription);

        public string GetTasksSummary()
        {
            var tasks = _db.GetAllTasks();
            if (tasks.Count == 0)
                return "You have no active tasks yet.";

            string summary = "📋 **Your Active Cybersecurity Tasks:**\n\n";
            foreach (var task in tasks)
                summary += $"• {task}\n";

            summary += "\nType 'complete 3' or 'delete 5' to manage tasks.";
            return summary;
        }

        public bool MarkComplete(int id) => _db.MarkTaskComplete(id);
        public bool DeleteTask(int id) => _db.DeleteTask(id);
    }
}