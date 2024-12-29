using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        private const double ZeroThreshold = 1e-10; // Define ZeroThreshold constant

        public static readonly ComplexNumber Zero = new ComplexNumber();
        public static readonly ComplexNumber One = new ComplexNumber { Real = 1 };

        public ComplexNumber Add(ComplexNumber other) =>
            new ComplexNumber { Real = Real + other.Real, Imaginary = Imaginary + other.Imaginary };

        public ComplexNumber Subtract(ComplexNumber other) =>
            new ComplexNumber { Real = Real - other.Real, Imaginary = Imaginary - other.Imaginary };

        public ComplexNumber Multiply(ComplexNumber other) =>
            new ComplexNumber
            {
                Real = Real * other.Real - Imaginary * other.Imaginary,
                Imaginary = Real * other.Imaginary + Imaginary * other.Real
            };

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;
            return new ComplexNumber
            {
                Real = (Real * other.Real + Imaginary * other.Imaginary) / denominator,
                Imaginary = (Imaginary * other.Real - Real * other.Imaginary) / denominator
            };
        }

        public double GetMagnitudeSquared() => Real * Real + Imaginary * Imaginary;

        public bool IsZero() => Math.Abs(Real) < ZeroThreshold && Math.Abs(Imaginary) < ZeroThreshold;

        public bool IsCloseTo(ComplexNumber other)
        {
            double distanceSquared = Math.Pow(Real - other.Real, 2) + Math.Pow(Imaginary - other.Imaginary, 2);
            return distanceSquared < 0.01;
        }

        public override string ToString() => $"({Real} + {Imaginary}i)";
    }
}
