// 1. Індексатор для пошуку за номером залікової книжки
// Дозволяє звертатися до групи як до масиву: group["12345678"]
public Student this[string recordBookNumber]
{
    get => _students.FirstOrDefault(s => s.RecordBookNumber == recordBookNumber);
}
// 2. Оператор + для об'єднання двох груп в одну нову
// Реалізує логіку злиття списків студентів двох об'єктів
public static StudentGroup operator +(StudentGroup g1, StudentGroup g2)
{
    var mergedGroup = new StudentGroup
    {
        GroupName = $"{g1.GroupName}+{g2.GroupName}",
        Specialization = g1.Specialization,
        Course = g1.Course
    };
    mergedGroup._students.AddRange(g1._students);


    mergedGroup._students.AddRange(g2._students);
    mergedGroup.LogAction($"Об'єднано групи {g1.GroupName} та {g2.GroupName}");
    return mergedGroup;
}
// 3. Метод для виклику оператора + (вимога ПР щодо альтернативного виклику)
public StudentGroup MergeGroups(StudentGroup other) => this + other;

// 4. Пошук найкращого студента за допомогою перевантаженого оператора >
// Цей метод демонструє практичне застосування перевантаження операторів у класі Student
public Student BestStudent()
{
    if (!_students.Any()) return null;
    Student best = _students[0];
    foreach (var student in _students)
    {
        // Використовується перевантажений оператор > з класу Student
        if (student > best)
        {
            best = student;
        }
    }
    return best;
}
