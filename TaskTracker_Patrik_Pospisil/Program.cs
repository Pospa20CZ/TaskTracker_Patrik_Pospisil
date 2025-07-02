using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskTracker_Patrik_Pospisil
{
    internal class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            ShowOverdueWarning();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Console.WriteLine("==== Task Tracker ====");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Show Tasks");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        ShowTasksMenu();
                        break;
                    case "3":
                        CompleteTask();
                        break;
                    case "4":
                        DeleteTask();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to continue.");
                        Console.ReadLine();
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }

        static void ShowOverdueWarning()
        {
            var overdueTasks = tasks.Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Now.Date).ToList();
            if (overdueTasks.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("⚠️  You have overdue tasks!");
                Console.ResetColor();
                foreach (var task in overdueTasks)
                {
                    Console.WriteLine(task);
                }
                Console.WriteLine("\nPress Enter to continue.");
                Console.ReadLine();
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter due date (yyyy-mm-dd) or leave empty for none: ");
            string dueDateInput = Console.ReadLine();
            DateTime? dueDate = null;
            if (!string.IsNullOrWhiteSpace(dueDateInput))
            {
                if (DateTime.TryParse(dueDateInput, out DateTime parsedDate))
                {
                    dueDate = parsedDate;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Due date will be set to none.");
                }
            }

            Console.Write("Enter priority (Low, Medium, High): ");
            string priority = Console.ReadLine();

            TaskItem task = new TaskItem(nextId++, title, dueDate, priority);
            tasks.Add(task);

            Console.WriteLine("Task added! Press Enter to continue.");
            Console.ReadLine();
        }

        static void ShowTasksMenu()
        {
            Console.Clear();
            Console.WriteLine("==== Show Tasks ====");
            Console.WriteLine("1. All Tasks");
            Console.WriteLine("2. Only Completed Tasks");
            Console.WriteLine("3. Only Incomplete Tasks");
            Console.WriteLine("4. Tasks Due Today");
            Console.WriteLine("5. Overdue Tasks");
            Console.Write("Select a filter option: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ShowTasks("all");
                    break;
                case "2":
                    ShowTasks("completed");
                    break;
                case "3":
                    ShowTasks("incomplete");
                    break;
                case "4":
                    ShowTasks("today");
                    break;
                case "5":
                    ShowTasks("overdue");
                    break;
                default:
                    Console.WriteLine("Invalid option. Showing all tasks.");
                    ShowTasks("all");
                    break;
            }
        }

        static void ShowTasks(string filter)
        {
            Console.Clear();
            Console.WriteLine("==== Your Tasks ====");

            List<TaskItem> filteredTasks = filter switch
            {
                "completed" => tasks.Where(t => t.IsCompleted).ToList(),
                "incomplete" => tasks.Where(t => !t.IsCompleted).ToList(),
                "today" => tasks.Where(t => t.DueDate.HasValue && t.DueDate.Value.Date == DateTime.Now.Date).ToList(),
                "overdue" => tasks.Where(t => !t.IsCompleted && t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Now.Date).ToList(),
                _ => tasks
            };

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                foreach (var task in filteredTasks)
                {
                    Console.WriteLine(task);
                }
            }

            Console.WriteLine("\nPress Enter to continue.");
            Console.ReadLine();
        }

        static void CompleteTask()
        {
            Console.Write("Enter task ID to mark as completed: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.Find(t => t.Id == id);
                if (task != null)
                {
                    task.IsCompleted = true;
                    Console.WriteLine("Task marked as completed.");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void DeleteTask()
        {
            Console.Write("Enter task ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var task = tasks.Find(t => t.Id == id);
                if (task != null)
                {
                    tasks.Remove(task);
                    Console.WriteLine("Task deleted.");
                }
                else
                {
                    Console.WriteLine("Task not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
