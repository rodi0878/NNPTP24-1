using System;
namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }
        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };
        private const double Tolerance = 1e-5;

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber
            {
                RealPart = this.RealPart + other.RealPart,
                ImaginaryPart = this.ImaginaryPart + other.ImaginaryPart
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return Math.Abs(other.RealPart - RealPart) < Tolerance &&
                       Math.Abs(other.ImaginaryPart - ImaginaryPart) < Tolerance;
            }
            return false;
        }
        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber
            {
                RealPart = this.RealPart - other.RealPart,
                ImaginaryPart = this.ImaginaryPart - other.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber
            {
                RealPart = this.RealPart * other.RealPart - this.ImaginaryPart * other.ImaginaryPart,
                ImaginaryPart = this.RealPart * other.ImaginaryPart + this.ImaginaryPart * other.RealPart
            };
        }
        public ComplexNumber Divide(ComplexNumber other)
        {
            ComplexNumber nominator = this.Multiply(new ComplexNumber{ RealPart = other.RealPart, ImaginaryPart = -other.ImaginaryPart });
            double denominator = other.RealPart * other.RealPart + other.ImaginaryPart * other.ImaginaryPart;

            return new ComplexNumber
            {
                RealPart = nominator.RealPart / denominator,
                ImaginaryPart = nominator.ImaginaryPart / denominator
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

    }
}