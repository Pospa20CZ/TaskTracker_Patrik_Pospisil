using System;

namespace TaskTracker_Patrik_Pospisil
{
    internal enum PriorityLevel
    {
        Low,
        Medium,
        High
    }

    internal class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DueDate { get; set; }

        public TaskItem(int id, string title, DateTime? dueDate, string priorityString)
        {
            Id = id;
            Title = title;
            IsCompleted = false;
            DateCreated = DateTime.Now;
            DueDate = dueDate;

            if (!Enum.TryParse(priorityString, true, out PriorityLevel parsedPriority))
            {
                parsedPriority = PriorityLevel.Medium;
            }
            Priority = parsedPriority;
        }

        public override string ToString()
        {
            string status = IsCompleted ? "✓ Completed" : "✗ Pending";
            string due = DueDate.HasValue ? DueDate.Value.ToString("yyyy-MM-dd") : "None";
            return $"[{status}] (ID: {Id}) {Title}\n - Created: {DateCreated:yyyy-MM-dd}\n - Due: {due}\n - Priority: {Priority}";
        }
    }
}
