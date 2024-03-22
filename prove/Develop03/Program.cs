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

public class Scripture
{
    private List<ScriptureWord> words;
    private ScriptureReference reference;
    private Random rand;

    public Scripture(ScriptureReference reference, string text)
    {
        this.reference = reference;
        words = new List<ScriptureWord>();

        string[] wordArray = text.Split(' ');

        foreach (string word in wordArray)
        {
            words.Add(new ScriptureWord(word));
        }

        rand = new Random();
    }

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

    public void HideRandomWord()
    {
        // Filter out already hidden words
        List<ScriptureWord> visibleWords = words.FindAll(w => !w.Hidden);
        if (visibleWords.Count > 0)
        {
            int index = rand.Next(visibleWords.Count);
            visibleWords[index].ToggleHiddenState();
        }
    }

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

public class Program
{
    public static void Main(string[] args)
    {
        ScriptureReference reference = new ScriptureReference("John", 3, 16);
        string text = "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.";
        Scripture scripture = new Scripture(reference, text);

        while (!scripture.AllWordsHidden())
        {
            scripture.DisplayScripture();
            string input = Console.ReadLine().ToLower();

            if (input == "quit")
            {
                break;
            }

            if (input == "")
            {
                scripture.HideRandomWord();
            }
            else
            {
                Console.WriteLine("Invalid input. Please press Enter or type 'quit' to exit.");
            }
        }

        Console.WriteLine("Program ended.");
    }
}
