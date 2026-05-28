using System.Collections.Generic;
using System.Linq;

public class StudentGroup
{
    public List<Student> Students { get; set; } = new List<Student>();

    public static StudentGroup operator +(StudentGroup a, StudentGroup b)
    {
        var newGroup = new StudentGroup();
        newGroup.Students.AddRange(a.Students);
        newGroup.Students.AddRange(b.Students);
        return newGroup;
    }

    public Student this[int index]
    {
        get => Students.FirstOrDefault(s => s.RecordBookNumber == index);
    }

    public Student BestStudent()
    {
        if (Students.Count == 0) return null;
        Student best = Students[0];
        foreach (var s in Students)
        {
            if (s > best) best = s;
        }
        return best;
    }

    public StudentGroup MergeGroups(StudentGroup other)
    {
        return this + other;
    }
}