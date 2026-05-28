using System;

public class Vector
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }

    public static Vector operator +(Vector a, Vector b) => new Vector { X = a.X + b.X, Y = a.Y + b.Y, Z = a.Z + b.Z };
    public static Vector operator -(Vector a, Vector b) => new Vector { X = a.X - b.X, Y = a.Y - b.Y, Z = a.Z - b.Z };
    public static double operator *(Vector a, Vector b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;

    public static bool operator ==(Vector a, Vector b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }
    public static bool operator !=(Vector a, Vector b) => !(a == b);

    public static bool operator >(Vector a, Vector b) => (double)a > (double)b;
    public static bool operator <(Vector a, Vector b) => (double)a < (double)b;

    public static Vector operator ++(Vector a) => new Vector { X = a.X + 1, Y = a.Y + 1, Z = a.Z + 1 };
    public static Vector operator --(Vector a) => new Vector { X = a.X - 1, Y = a.Y - 1, Z = a.Z - 1 };

    public static explicit operator double(Vector v) => Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);

    public override bool Equals(object obj) => obj is Vector v && this == v;
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}