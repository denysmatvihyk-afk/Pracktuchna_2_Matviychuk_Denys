public string BuildGroupReport(StudentGroup group)
{
    if (group == null || group.Students.Count == 0) return "Група порожня.";

    StringBuilder sb = new StringBuilder();
    sb.AppendLine("=== ПОВНИЙ ЗВІТ ГРУПИ ===");
    sb.AppendLine($"Загальна кількість студентів: {group.Students.Count}");

    sb.AppendLine("Список студентів:");
    foreach (var student in group.Students)
    {
        // Використовуємо метод, який ми написали раніше
        sb.AppendLine($"- {student.GetFormattedInfo(false).Trim()}");
    }
    sb.AppendLine("=========================");

    return sb.ToString();
}