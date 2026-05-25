using System;
using System.Text;

public class StudentDocumentGenerator
{
    public string GenerateCharacteristic(Student student)
    {
        if (student == null) return "Помилка: Студента не знайдено.";

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("==================================================");
        sb.AppendLine("               ХАРАКТЕРИСТИКА");
        sb.AppendLine("==================================================");
        sb.AppendLine($"Видана студенту: {student.FullName}");
        sb.AppendLine($"Поточний бал успішності: {student.Grade}");
        sb.AppendLine("--------------------------------------------------");

        sb.Append("Академічна успішність: ");
        if (student.Grade >= 10)
            sb.AppendLine("Студент має відмінні успіхи у навчанні, виявляє високу активність та може бути рекомендований до підвищеної стипендії.");
        else if (student.Grade >= 6)
            sb.AppendLine("Студент демонструє стабільні результати, добре засвоює матеріал, але має простір для покращення.");
        else
            sb.AppendLine("Студенту рекомендується приділити більше уваги навчанню, уникати пропусків та звертатися за консультаціями до викладачів.");

        if (!string.IsNullOrWhiteSpace(student.Notes))
        {
            sb.AppendLine("\nДодаткові відомості (нотатки):");
            sb.AppendLine(student.Notes);
        }

        sb.AppendLine("--------------------------------------------------");
        sb.AppendLine($"Дата генерації: {DateTime.Now:dd.MM.yyyy}");
        sb.AppendLine("==================================================");

        return sb.ToString();
    }
}