using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public static readonly ComplexNumber Zero = new ComplexNumber
        {
            RealPart = 0,
            ImaginaryPart = 0
        };
        
        public double RealPart { get; set; }
        public double ImaginaryPart { get; set; }

        public override bool Equals(object obj)
        {
            double tolerance = 0.00001;
            if (obj is ComplexNumber other)
            {
                return Math.Abs(other.RealPart - RealPart) < tolerance && Math.Abs(other.ImaginaryPart - ImaginaryPart) < tolerance;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RealPart.GetHashCode() * 397) ^ ImaginaryPart.GetHashCode();
            }
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber
            {
                RealPart = this.RealPart + other.RealPart,
                ImaginaryPart = this.ImaginaryPart + other.ImaginaryPart
            };
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
            ComplexNumber conjugate = Multiply(new ComplexNumber { RealPart = other.RealPart, ImaginaryPart = -other.ImaginaryPart });
            double denominator = other.RealPart * other.RealPart + other.ImaginaryPart * other.ImaginaryPart;

            return new ComplexNumber
            {
                RealPart = conjugate.RealPart / denominator,
                ImaginaryPart = conjugate.ImaginaryPart / denominator
            };
        }

        public double GetAbs()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public double GetAngle()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }
    }
}
