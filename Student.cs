using System;
using System.Text;

public class Student
{
    private string _fullName;

    // Валідація ПІБ (має містити щонайменше три слова)
    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value) ||
                value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length < 3)
            {
                throw new ArgumentException("FullName має містити щонайменше три слова (прізвище, ім’я, по батькові).");
            }
            _fullName = value;
        }
    }

    public string Notes { get; set; } = "";
    public int Grade { get; set; }

    // Реалізація через StringBuilder (вимога ПР №3)
    public string GetFormattedInfo(bool detailed = false)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Студент: {FullName}");

        if (detailed)
        {
            sb.AppendLine($"Оцінка: {Grade}");
            sb.AppendLine($"Нотатки: {Notes}");
        }

        return sb.ToString();
    }

    // Перевірка на наявність ключового слова у нотатках
    public bool ContainsKeyword(string keyword)
    {
        if (string.IsNullOrEmpty(keyword) || string.IsNullOrEmpty(Notes))
            return false;

        return Notes.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }
}