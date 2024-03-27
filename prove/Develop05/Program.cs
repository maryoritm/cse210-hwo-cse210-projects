using System;
using System.Collections.Generic;
using System.IO;

//I added functionality to allow users to create, save, load, and manage their own goals.

// Base class for goals
public class Goal
{
    public string Name { get; set; }
    public int Value { get; set; }
    public bool IsCompleted { get; set; }

    public Goal(string name, int value)
    {
        Name = name;
        Value = value;
        IsCompleted = false;
    }

    public virtual void MarkComplete()
    {
        IsCompleted = true;
    }

    public virtual void ShowProgress()
    {
        Console.WriteLine($"Goal: {Name} - Completed: {(IsCompleted ? "Yes" : "No")}");
    }

    public virtual string GetGoalInfo()
    {
        return $"{Name},{Value},{IsCompleted}";
    }
}

// Subclass for simple goals
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int value) : base(name, value)
    {
    }

    public override void MarkComplete()
    {
        base.MarkComplete();
        Console.WriteLine($"Congratulations! You completed the goal: {Name}");
    }
}

// Subclass for eternal goals
public class EternalGoal : Goal
{
    public EternalGoal(string name, int value) : base(name, value)
    {
    }

    public override void ShowProgress()
    {
        Console.WriteLine($"Eternal Goal: {Name} - Completed: {(IsCompleted ? "Yes" : "No")}");
    }
}

// Subclass for checklist goals
public class ChecklistGoal : Goal
{
    private int timesCompleted;
    private int totalTimes;

    public ChecklistGoal(string name, int value, int totalTimes) : base(name, value)
    {
        this.totalTimes = totalTimes;
        timesCompleted = 0;
    }

    public override void MarkComplete()
    {
        timesCompleted++;
        if (timesCompleted >= totalTimes)
        {
            base.MarkComplete();
            Console.WriteLine($"Congratulations! You completed the checklist goal: {Name}");
        }
    }

    public override void ShowProgress()
    {
        Console.WriteLine($"Checklist Goal: {Name} - Completed: {timesCompleted}/{totalTimes}");
    }

    public override string GetGoalInfo()
    {
        return $"{base.GetGoalInfo()},{timesCompleted},{totalTimes}";
    }
}

public class EternalQuestProgram
{
    private List<Goal> goals;
    private int score;

    public EternalQuestProgram()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(string goalName)
    {
        Goal goal = goals.Find(g => g.Name == goalName && !g.IsCompleted);
        if (goal != null)
        {
            goal.MarkComplete();
            score += goal.Value;
        }
        else
        {
            Console.WriteLine("Goal not found or already completed.");
        }
    }

    public void ShowGoals()
    {
        foreach (var goal in goals)
        {
            goal.ShowProgress();
        }
    }

    public void ShowScore()
    {
        Console.WriteLine($"Your current score: {score}");
    }

    public void SaveGoals(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.GetGoalInfo());
            }
        }
    }

    public void LoadGoals(string fileName)
    {
        goals.Clear();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(',');
                string name = parts[0];
                int value = int.Parse(parts[1]);
                bool isCompleted = bool.Parse(parts[2]);
                int timesCompleted = int.Parse(parts[3]);
                int totalTimes = int.Parse(parts[4]);

                Goal goal;
                if (totalTimes > 0)
                {
                    goal = new ChecklistGoal(name, value, totalTimes);
                    ((ChecklistGoal)goal).IsCompleted = isCompleted;
                    ((ChecklistGoal)goal).MarkComplete(); // Mark as completed if needed based on timesCompleted
                }
                else
                {
                    goal = new Goal(name, value);
                    goal.IsCompleted = isCompleted;
                    if (isCompleted)
                    {
                        goal.MarkComplete();
                    }
                }
                goals.Add(goal);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuestProgram program = new EternalQuestProgram();

        // Load existing goals if available
        program.LoadGoals("goals.txt");

        // Menu loop
        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nEternal Quest Program Menu:");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string goalName = Console.ReadLine();
                    Console.Write("Enter goal value: ");
                    int goalValue = int.Parse(Console.ReadLine());
                    Console.Write("Enter goal type (Simple, Eternal, Checklist): ");
                    string goalType = Console.ReadLine();

                    Goal newGoal;
                    switch (goalType.ToLower())
                    {
                        case "simple":
                            newGoal = new SimpleGoal(goalName, goalValue);
                            break;
                        case "eternal":
                            newGoal = new EternalGoal(goalName, goalValue);
                            break;
                        case "checklist":
                            Console.Write("Enter total times for checklist goal: ");
                            int totalTimes = int.Parse(Console.ReadLine());
                            newGoal = new ChecklistGoal(goalName, goalValue, totalTimes);
                            break;
                        default:
                            Console.WriteLine("Invalid goal type.");
                            continue;
                    }

                    program.AddGoal(newGoal);
                    Console.WriteLine("Goal added successfully.");
                    break;
                case "2":
                    Console.Write("Enter goal name to record event: ");
                    string eventGoal = Console.ReadLine();
                    program.RecordEvent(eventGoal);
                    break;
                case "3":
                    program.ShowGoals();
                    break;
                case "4":
                    program.ShowScore();
                    break;
                case "5":
                    program.SaveGoals("goals.txt");
                    Console.WriteLine("Goals saved successfully.");
                    break;
                case "6":
                    program.SaveGoals("goals.txt");
                    Console.WriteLine("Exiting program.");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}
