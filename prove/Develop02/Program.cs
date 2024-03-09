using System;
using System.Collections.Generic;
using System.IO;


// I added: Randomly select a prompt from a list of predefined prompts to add variability
// and depth to the journaling experience. Encourages users to reflect on different
// aspects of their day, enhancing the overall user engagement and usability.

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public JournalEntry(string prompt, string response, string date)
    {
        Prompt = prompt;
        Response = response;
        Date = date;
    }
}

class Journal
{
    private List<JournalEntry> entries;

    public Journal()
    {
        entries = new List<JournalEntry>();
    }

    public void WriteEntry(string prompt)
    {
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToShortDateString();
        entries.Add(new JournalEntry(prompt, response, date));
    }

    public void DisplayEntries()
    {
        Console.WriteLine("Journal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }

    public void SaveJournalToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                outputFile.WriteLine($"{entry.Date},{entry.Prompt},{entry.Response}");
            }
        }
        Console.WriteLine("Journal saved successfully.");
    }

    public void LoadJournalFromFile(string filename)
    {
        entries.Clear();
        string[] lines = File.ReadAllLines(filename);
        foreach (var line in lines)
        {
            string[] parts = line.Split(",");
            entries.Add(new JournalEntry(parts[1], parts[2], parts[0]));
        }
        Console.WriteLine("Journal loaded successfully.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal entries");
            Console.WriteLine("3. Save journal to a file");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine("\nWriting a new entry...");
                    Console.WriteLine("Choose a prompt randomly...");
                    
                    Random rand = new Random();
                    List<string> prompts = new List<string>
                    {
                        "Who was the most interesting person I interacted with today?",
                        "What was the best part of my day?",
                        "How did I see the hand of the Lord in my life today?",
                        "What was the strongest emotion I felt today?",
                        "If I had one thing I could do over today, what would it be?"
                    };
                    int index = rand.Next(prompts.Count);
                    string prompt = prompts[index];
                    journal.WriteEntry(prompt);
                    break;
                case 2:
                    Console.WriteLine("\nDisplaying journal entries...");
                    journal.DisplayEntries();
                    break;
                case 3:
                    Console.Write("\nEnter filename to save the journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveJournalToFile(saveFilename);
                    break;
                case 4:
                    Console.Write("\nEnter filename to load the journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadJournalFromFile(loadFilename);
                    break;
                case 5:
                    isRunning = false;
                    Console.WriteLine("\nExiting the program. Goodbye!");
                    break;
                default:
                    Console.WriteLine("\nInvalid choice. Please choose again.");
                    break;
            }
        }
    }
}
