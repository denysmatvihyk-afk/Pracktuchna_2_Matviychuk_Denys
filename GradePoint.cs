using System;

public class GradePoint
{
    private double _value;
    public double Value
    {
        get => _value;
        set => _value = Math.Clamp(value, 0, 10);
    }

    public static GradePoint operator +(GradePoint a, GradePoint b) => new GradePoint { Value = a.Value + b.Value };
    public static GradePoint operator ++(GradePoint a) => new GradePoint { Value = a.Value + 1 };
    public static GradePoint operator --(GradePoint a) => new GradePoint { Value = a.Value - 1 };

    public static bool operator >(GradePoint a, GradePoint b) => a.Value > b.Value;
    public static bool operator <(GradePoint a, GradePoint b) => a.Value < b.Value;
    public static bool operator >=(GradePoint a, GradePoint b) => a.Value >= b.Value;
    public static bool operator <=(GradePoint a, GradePoint b) => a.Value <= b.Value;

    public static bool operator true(GradePoint a) => a.Value >= 8;
    public static bool operator false(GradePoint a) => a.Value < 8;

    public static implicit operator double(GradePoint g) => g.Value;
    public static implicit operator GradePoint(double d) => new GradePoint { Value = d };
}