using System;
using System.Threading;

// Additional creativity point: Adding a log file to save user activities
class Logger
{
    public static void LogActivity(string activityName, int duration)
    {
        string logEntry = $"{DateTime.Now}: Completed {activityName} for {duration} seconds.";
        // You can save the logEntry to a file or display it in the console
        Console.WriteLine(logEntry);
    }
}

// Base class for all activities
class Activity
{
    private string activityName;
    private string description;
    protected int duration;

    public Activity(string name, string desc)
    {
        activityName = name;
        description = desc;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {activityName}...");
        Console.WriteLine(description);
        SetDuration();
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000); // Pause for 3 seconds before starting
    }

    protected virtual void SetDuration()
    {
        Console.Write("Enter duration in seconds: ");
        duration = int.Parse(Console.ReadLine());
    }

    public void EndActivity()
    {
        Console.WriteLine($"You've completed {activityName} for {duration} seconds.");
        Logger.LogActivity(activityName, duration); // Log activity completion
        Console.WriteLine("Good job!");
        Thread.Sleep(3000); // Pause for 3 seconds before ending
    }

    protected void ShowCountdown()
    {
        for (int i = duration; i > 0; i--)
        {
            Console.Write($"\rBreathing {(i % 2 == 0 ? "In" : "Out")}... {i} ");
            Thread.Sleep(1000); // Pause for 1 second for countdown
        }
        Console.WriteLine();
    }

    protected void WriteItems()
    {
        Console.WriteLine("Write as many items as you can within the specified time:");
        DateTime endTime = DateTime.Now.AddSeconds(duration);
        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
            {
                itemCount++;
            }
        }

        Console.WriteLine($"You wrote {itemCount} items.");
    }
}

// Breathing activity class
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
        PerformBreathing();
    }

    private void PerformBreathing()
    {
        Console.WriteLine("Clear your mind and focus on your breathing...");
        ShowCountdown(); // Display countdown with breathing in/out message
    }
}

// Reflection activity class
class ReflectionActivity : Activity
{
    private string[] prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private string[] questions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity() : base("Reflection Activity", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
        ReflectOnExperience();
    }

    private void ReflectOnExperience()
    {
        Console.WriteLine("Think deeply about the following prompt:");
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);

        foreach (string question in questions)
        {
            Console.WriteLine(question);
            Thread.Sleep(3000); // Pause for 3 seconds after each question
        }
    }
}

// Listing activity class
class ListingActivity : Activity
{
    private string[] prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by listing as many things as you can.")
    {
    }

    protected override void SetDuration()
    {
        base.SetDuration();
        WriteItems();
    }

    private void WriteItems()
    {
        Console.WriteLine("Think about the following prompt:");
        string prompt = prompts[new Random().Next(prompts.Length)];
        Console.WriteLine(prompt);

        Console.WriteLine("You have a limited time to write. Start now!");

        base.WriteItems(); // Use the WriteItems method from the base class

        Console.WriteLine("Time's up!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to the Mindfulness Program!");

        // Create instances of activities
        Activity breathing = new BreathingActivity();
        Activity reflection = new ReflectionActivity();
        Activity listing = new ListingActivity();

        bool exitProgram = false;

        while (!exitProgram)
        {
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit Program");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    breathing.StartActivity();
                    Thread.Sleep(1000); // Pause for 1 second before ending
                    breathing.EndActivity();
                    break;
                case 2:
                    reflection.StartActivity();
                    Thread.Sleep(1000); // Pause for 1 second before ending
                    reflection.EndActivity();
                    break;
                case 3:
                    listing.StartActivity();
                    Thread.Sleep(1000); // Pause for 1 second before ending
                    listing.EndActivity();
                    break;
                case 4:
                    exitProgram = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number from 1 to 4.");
                    break;
            }
        }

        Console.WriteLine("Thank you for using the Mindfulness Program!");
    }
}
