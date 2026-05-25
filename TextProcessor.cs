using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class TextProcessor
{
    // Реверс рядка
    public string Reverse(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        char[] charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    // Підрахунок кількості слів
    public int CountWords(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return 0;
        return text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    // Підрахунок символів
    public int CountCharacters(string text, bool ignoreWhitespace = true)
    {
        if (string.IsNullOrEmpty(text)) return 0;
        return ignoreWhitespace ? text.Count(c => !char.IsWhiteSpace(c)) : text.Length;
    }

    // Нормалізація (видалення зайвих пробілів)
    public string Normalize(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        text = text.Trim();
        return Regex.Replace(text, @"\s+", " ");
    }

    // Перевірка на паліндром
    public bool IsPalindrome(string text, bool ignoreCase = true, bool ignoreSpaces = true)
    {
        if (string.IsNullOrEmpty(text)) return false;

        string processedText = text;
        if (ignoreCase) processedText = processedText.ToLower();
        if (ignoreSpaces) processedText = processedText.Replace(" ", "");

        string reversedText = Reverse(processedText);
        return processedText == reversedText;
    }

    // Множинна заміна
    public string ReplaceMultiple(string text, Dictionary<string, string> replacements)
    {
        if (string.IsNullOrEmpty(text) || replacements == null) return text;

        StringBuilder sb = new StringBuilder(text);
        foreach (var kvp in replacements)
        {
            sb.Replace(kvp.Key, kvp.Value);
        }
        return sb.ToString();
    }

    // Розбиття на речення
    public string[] SplitIntoSentences(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return new string[0];
        // Розбиваємо за крапкою, знаком питання або оклику
        return text.Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                   .Select(s => s.Trim())
                   .Where(s => s.Length > 0)
                   .ToArray();
    }

    // Порівняння продуктивності string та StringBuilder
    public string ComparePerformance(int iterations)
    {
        Stopwatch sw = new Stopwatch();

        // Тест string
        sw.Start();
        string s = "";
        for (int i = 0; i < iterations; i++) s += "a";
        sw.Stop();
        long stringTime = sw.ElapsedMilliseconds;

        // Тест StringBuilder
        sw.Restart();
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iterations; i++) sb.Append("a");
        sw.Stop();
        long sbTime = sw.ElapsedMilliseconds;

        return $"Ітерацій: {iterations}. Час string: {stringTime} мс. Час StringBuilder: {sbTime} мс.";
    }
}