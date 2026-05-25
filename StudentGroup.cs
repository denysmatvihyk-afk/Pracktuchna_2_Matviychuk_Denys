using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StudentGroup
{
    // Припускаю, що у тебе вже є список студентів
    private List<Student> _students = new List<Student>();

    public void AddStudent(Student student) => _students.Add(student);

    [cite_start]// Пошук за фрагментом імені [cite: 17]
    public string SearchByNameFragment(string fragment)
    {
        var found = _students.Where(s => s.FullName.Contains(fragment, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!found.Any()) return "Студентів не знайдено.";

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Знайдені студенти:");
        foreach (var s in found)
        {
            sb.AppendLine($"- {s.FullName}");
        }
        return sb.ToString();
    }

    [cite_start]// Експорт у CSV [cite: 18]
    public string ExportToCsv()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("FullName,Grade,Notes");
        foreach (var s in _students)
        {
            // Екранування ком у нотатках, щоб не зламати CSV формат
            string safeNotes = s.Notes != null && s.Notes.Contains(",") ? $"\"{s.Notes}\"" : s.Notes;
            sb.AppendLine($"{s.FullName},{s.Grade},{safeNotes}");
        }
        return sb.ToString();
    }

    [cite_start]// Імпорт з сирого тексту [cite: 19]
    public void ImportStudentsFromText(string rawText)
    {
        if (string.IsNullOrWhiteSpace(rawText)) return;

        var lines = rawText.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            if (parts.Length >= 3)
            {
                try
                {
                    _students.Add(new Student
                    {
                        FullName = parts[0].Trim(),
                        Grade = int.Parse(parts[1].Trim()),
                        Notes = parts[2].Trim().Trim('"')
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка імпорту рядка '{line}': {ex.Message}");
                }
            }
        }
    }
}