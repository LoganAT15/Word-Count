// This program will count the frequency of a word in a file and output a sorted list of the words.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class WordCount
{
    public Dictionary<string, int> pairs = new Dictionary<string, int>();

    /// <summary>
    /// This wil read an input file and fill a dictionary with occurences of
    /// unique words and the amount of times they appear.
    /// </summary>
    /// <param name="fileIn"></param>
    public WordCount(string fileIn)
    {
        try // Check that file exists and can be read.
        {
            using (StreamReader sr = File.OpenText(fileIn))
            {
                string line, wordFixed = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries); // Splits string by whitespace and removes whitespace entries.
                    foreach (string word in words)
                    {
                        wordFixed = FormatString(word); // Removes punctuation from words and sets them to lowercase.
                        if (pairs.ContainsKey(wordFixed)) // Check for word and increment value if it exists.
                            pairs[wordFixed]++;
                        else
                            pairs.Add(wordFixed, 1);      // Add words and set its value to one if it doesn't exist.
                    }
                }
            }
        }
        catch (Exception ex) // Throw error if file can't be read.
        {
            Console.WriteLine("The file could not be read.");
            Console.WriteLine(ex.Message);
        }
    }

    /// <summary>
    /// Removes punctuation from input string and sets it to lowercase.
    /// </summary>
    /// <param name="s"></param>
    /// <returns>s</returns>
    public string FormatString(string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsPunctuation(c))
                    sb.Append(c);
        }
        s = sb.ToString();
        s = s.ToLower();
        return s;
    }
}

public class Driver
{
    /// <summary>
    /// Creates a wordcount object and passes it a file. Then formats an output of the stored words and word counts.
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        WordCount c = new WordCount("C:\\Users\\Logan\\Downloads\\1-0.txt");
        var sortedDict = from entry in c.pairs orderby entry.Value descending select entry; // Sorts dictionary by value in descending order and stores it in a list.
        Dictionary<string, int> sorted = sortedDict.ToDictionary(kvp => kvp.Key, kvp => kvp.Value); // Converts the sorted values back into a dictionary.
        foreach (KeyValuePair<string, int> kvp in sorted)
        {
            Console.WriteLine("{0} {1}", kvp.Key, kvp.Value);
        }
    }
}