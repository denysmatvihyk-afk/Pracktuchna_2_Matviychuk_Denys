using Praktychna1;
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.IO;
class Program
{
    static StudentGroup myGroup = new StudentGroup
    {
        GroupName = "К-321",
        Specialization = "Software Engineering",
        Course = 3
    };
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;
        while (true)
        {
            StringBuilder menuBuilder = new StringBuilder();
            menuBuilder.AppendLine("\n--- СИСТЕМА УПРАВЛІННЯ ГРУПОЮ (ПР №4) ---");
            menuBuilder.AppendLine("1.  Додати студента");
            menuBuilder.AppendLine("2.  Видалити студента");
            menuBuilder.AppendLine("3.  Вивести всіх студентів");
            menuBuilder.AppendLine("4.  Пошук за ключовим словом");
            menuBuilder.AppendLine("7.  Статистика групи");
            menuBuilder.AppendLine("8.  Зберегти дані (JSON)");
            menuBuilder.AppendLine("9.  Завантажити дані (JSON)");
            menuBuilder.AppendLine("10. Пошук за фрагментом ПІБ");

            // --- Відновлені пункти (ПР №3) ---
            menuBuilder.AppendLine("11. Згенерувати повний звіт (Statistics + All Students)");
            menuBuilder.AppendLine("12. Нормалізувати нотатки (видалити зайві пробіли)");
            menuBuilder.AppendLine("13. Перевірити паліндроми в нотатках");
            menuBuilder.AppendLine("14. Експорт групи у CSV");
            menuBuilder.AppendLine("15. Імпорт студентів з тексту");
            menuBuilder.AppendLine("16. Переглянути логи системи");
            menuBuilder.AppendLine("17. Порівняти продуктивність string vs StringBuilder");
            menuBuilder.AppendLine("18. Реверс тексту та підрахунок слів");
            // --- Нові пункти (ПР №4)  ---
            menuBuilder.AppendLine("19. Порівняти двох студентів (>, <, ==)");
            menuBuilder.AppendLine("20. Об’єднати дві групи (+)");
            menuBuilder.AppendLine("21. Продемонструвати роботу з класом Vector");
            menuBuilder.AppendLine("22. Продемонструвати роботу з GradePoint");
            menuBuilder.AppendLine("23. Знайти найкращого студента (BestStudent)");
            menuBuilder.AppendLine("0.  Вийти");
            menuBuilder.Append("Виберіть дію: ");
            Console.Write(menuBuilder.ToString());
            string choice = Console.ReadLine();
            if (choice == "0") break;
            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": RemoveStudent(); break;
                case "3": ShowAllStudents(); break;
                case "4": SearchStudent(); break;
                case "7": Console.WriteLine(myGroup.GetGroupStatistics()); break;

                case "8": myGroup.SaveToFile("students.json"); Console.WriteLine("Збережено."); break;
                case "9": myGroup.LoadFromFile("students.json"); Console.WriteLine("Завантажено."); break;
                case "10":
                    Console.Write("Введіть фрагмент: ");
                    myGroup.SearchByNameFragment(Console.ReadLine());
                    break;
                case "11":
                    Console.WriteLine(myGroup.GetGroupStatistics());
                    ShowAllStudents();
                    break;
                case "12":
                    foreach (var s in myGroup.GetAllStudents()) s.Notes = s.Notes?.Trim();
                    Console.WriteLine("Нотатки нормалізовано.");
                    break;
                case "13":
                    foreach (var s in myGroup.GetAllStudents())
                    {
                        string clean = new string(s.Notes?.Where(char.IsLetterOrDigit).ToArray()).ToLower();
                        if (!string.IsNullOrEmpty(clean) && clean == new string(clean.Reverse().ToArray()))
                            Console.WriteLine($"Паліндром у {s.FullName}: {s.Notes}");
                    }
                    break;
                case "14":
                    myGroup.ExportToCsv("export.csv");
                    Console.WriteLine("Експортовано в export.csv");
                    break;
                case "15":
                    Console.WriteLine("Введіть імена (через кому):");
                    myGroup.ImportStudentsFromText(Console.ReadLine());
                    break;
                case "16": Console.WriteLine(myGroup.GetSystemLogs()); break;

                case "17":
                    var sw = System.Diagnostics.Stopwatch.StartNew();
                    string st = ""; for (int i = 0; i < 5000; i++) st += i;
                    sw.Stop(); long t1 = sw.ElapsedTicks;
                    sw.Restart();
                    StringBuilder sbb = new StringBuilder(); for (int i = 0; i < 5000; i++) sbb.Append(i);
                    sw.Stop(); long t2 = sw.ElapsedTicks;
                    Console.WriteLine($"String: {t1} | StringBuilder: {t2}");
                    break;
                case "18":
                    Console.Write("Текст: "); string t = Console.ReadLine() ?? "";
                    Console.WriteLine($"Реверс: {new string(t.Reverse().ToArray())}, Слів: {t.Split(' ').Length}");
                    break;
                case "19": CompareTwoStudents(); break;
                case "20": MergeWithAnotherGroup(); break;
                case "21": TestVector(); break;
                case "22": TestGradePoint(); break;
                case "23":
                    var best = myGroup.BestStudent();
                    Console.WriteLine(best != null ? $"Найкращий: {best.GetFormattedInfo()}" : "Порожньо.");
                    break;
                default: Console.WriteLine("Невірно."); break;
            }
        }
    }
    static void AddStudent()
    {
        try
        {
            Console.Write("ПІБ: "); string name = Console.ReadLine();

            Console.Write("№ заліковки (8 цифр): "); string id = Console.ReadLine();
            Console.Write("Прогрес (0-100): "); int progress = int.Parse(Console.ReadLine());

            var s = new Student { FullName = name, RecordBookNumber = id, CourseProgress = progress, DateOfBirth = DateTime.Now.AddYears(-18) };
            // Додамо кілька випадкових оцінок для тесту [cite: 29]
            s.Grades.Add(new GradePoint(8.5));
            s.Grades.Add(new GradePoint(9.0));
            myGroup.AddStudent(s);
            Console.WriteLine("Студента додано.");
        }
        catch (Exception e) { Console.WriteLine($"Помилка: {e.Message}"); }
    }
    static void RemoveStudent()
    {
        Console.Write("№ заліковки: "); string id = Console.ReadLine();
        myGroup.RemoveStudent(id);
        Console.WriteLine("Видалено.");
    }
    static void ShowAllStudents()
    {
        foreach (var s in myGroup.GetAllStudents()) Console.WriteLine(s.GetFormattedInfo());
    }

    static void SearchStudent()
    {
        Console.Write("Ключове слово: "); string k = Console.ReadLine();
        foreach (var s in myGroup.GetAllStudents().Where(x => x.ContainsKeyword(k)))
            Console.WriteLine(s.GetFormattedInfo());


    }
    static void CompareTwoStudents()
    {
        var students = myGroup.GetAllStudents();
        if (students.Count < 2) { Console.WriteLine("Треба мінімум 2 студенти."); return; }
        Student s1 = students[0];
        Student s2 = students[1];
        Console.WriteLine($"Порівняння {s1.FullName} та {s2.FullName}:");
        Console.WriteLine($"s1 > s2: {s1 > s2}"); // Використання оператора 
        Console.WriteLine($"s1 == s2: {s1 == s2}");
        Console.WriteLine(s1 + s2); // Командний профіль [cite: 14]
    }
    static void TestVector()
    {
        Vector v1 = new Vector(1, 2, 3);
        Vector v2 = new Vector(4, 5, 6);
        Console.WriteLine($"v1: {v1}, v2: {v2}");
        Console.WriteLine($"Сума v1 + v2: {v1 + v2}");
        Console.WriteLine($"Довжина v1: {(double)v1:F2}");
    }
    static void TestGradePoint()
    {
        GradePoint g1 = 7.5; // Неявне приведення
        GradePoint g2 = 9.2;
        Console.WriteLine($"Оцінка 1: {g1}, Оцінка 2: {g2}");
        if (g2) Console.WriteLine("Оцінка 2 — відмінна (>=8)");
    }
    static void MergeWithAnotherGroup()
    {
        StudentGroup other = new StudentGroup { GroupName = "K-321" };
        other.AddStudent(new Student { FullName = "Лущан Владислав", RecordBookNumber = "99999999" });



        var merged = myGroup + other; // Оператор + [cite: 16]
        Console.WriteLine($"Нова група: {merged.GroupName}, Студентів: {merged.GroupSize}");
    }
}
