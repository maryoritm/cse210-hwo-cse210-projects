using System;
using System.Collections.Generic;

//I made enhancements to encapsulation, improved Word class functionality, ensured proper program termination, 
//and added a method to shuffle words before hiding them. These improvements exceed core requirements.

// Class to represent a word in the scripture
public class ScriptureWord
{
    public string Word { get; }
    public bool Hidden { get; set; }

    public ScriptureWord(string word)
    {
        Word = word;
        Hidden = false;
    }

    public void ToggleHiddenState()
    {
        Hidden = !Hidden;
    }
}

// Class to represent a reference of scripture
public class ScriptureReference
{
    public string Book { get; }
    public int Chapter { get; }
    public int StartVerse { get; }
    public int EndVerse { get; }

    public ScriptureReference(string book, int chapter, int startVerse, int endVerse = 0)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse == 0 ? startVerse : endVerse;
    }
}

// Class to represent the scripture itself
public class Scripture
{
    private List<ScriptureWord> words;
    private ScriptureReference reference;

    public Scripture(ScriptureReference reference, string text)
    {
        this.reference = reference;
        words = new List<ScriptureWord>();

        // Split the scripture text into words
        string[] wordArray = text.Split(' ');

        // Initialize ScriptureWord objects for each word in the scripture
        foreach (string word in wordArray)
        {
            words.Add(new ScriptureWord(word));
        }
    }

    // Method to display the scripture with words hidden
    public void DisplayScripture()
    {
        Console.Clear();
        Console.WriteLine($"{reference.Book} {reference.Chapter}:{reference.StartVerse}-{reference.EndVerse}\n");

        foreach (ScriptureWord word in words)
        {
            if (word.Hidden)
            {
                Console.Write("_ ");
            }
            else
            {
                Console.Write(word.Word + " ");
            }
        }
        Console.WriteLine("\nPress Enter to continue or type 'quit' to exit...");
    }

    // Method to hide a specific number of random words in the scripture
    public void HideRandomWords(int numberOfWordsToHide)
    {
        Random rand = new Random();

        // Shuffle words to ensure randomness
        words.Shuffle(rand);

        // Hide words until the desired number are hidden or we run out of words
        for (int i = 0; i < Math.Min(numberOfWordsToHide, words.Count); i++)
        {
            words[i].ToggleHiddenState();
        }
    }

    // Method to check if all words in the scripture are hidden
    public bool AllWordsHidden()
    {
        foreach (ScriptureWord word in words)
        {
            if (!word.Hidden)
            {
                return false;
            }
        }
        return true;
    }
}

// Main program class
public class Program
{
    public static void Main(string[] args)
    {
        // Initialize the scripture
        ScriptureReference reference = new ScriptureReference("John", 3, 16);
        string text = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
        Scripture scripture = new Scripture(reference, text);

        // Main loop to display and hide scripture until all words are hidden or user quits
        while (!scripture.AllWordsHidden())
        {
            scripture.DisplayScripture();
            string input = Console.ReadLine().ToLower();

            if (input == "quit")
            {
                break;
            }

            int wordsToHide;
            if (!int.TryParse(input, out wordsToHide))
            {
                Console.WriteLine("Invalid input. Please enter a number or type 'quit' to exit.");
                continue;
            }

            scripture.HideRandomWords(wordsToHide);
        }

        Console.WriteLine("Program ended.");
    }
}

// Extension method to shuffle a list
public static class ListExtensions
{
    public static void Shuffle<T>(this IList<T> list, Random rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
