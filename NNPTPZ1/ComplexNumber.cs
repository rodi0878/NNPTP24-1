using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double Re { get; set; }
        public double Im { get; set; }

        public static readonly ComplexNumber Zero = new ComplexNumber
        {
            Re = 0,
            Im = 0
        };

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return other.Re == Re && other.Im == Im;
            }
            return false;
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Re = this.Re + other.Re,
                Im = this.Im + other.Im
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Re = this.Re - other.Re,
                Im = this.Im - other.Im
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber
            {
                Re = this.Re * other.Re - this.Im * other.Im,
                Im = this.Re * other.Im + this.Im * other.Re
            };
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            double denominator = other.Re * other.Re + other.Im * other.Im;
            var numerator = this.Multiply(new ComplexNumber { Re = other.Re, Im = -other.Im });

            return new ComplexNumber
            {
                Re = numerator.Re / denominator,
                Im = numerator.Im / denominator
            };
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Re * Re + Im * Im);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(Im / Re);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"({Re} + {Im}i)";
        }
    }
}