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

        public override bool Equals(object obj)
        {
            const double tolerance = 0.00001;
            if (obj is ComplexNumber)
            {
                ComplexNumber anotherNumber = obj as ComplexNumber;
                return Math.Abs(anotherNumber.RealPart - RealPart) < tolerance
                    && Math.Abs(anotherNumber.ImaginaryPart - ImaginaryPart) < tolerance;
            }
            return base.Equals(obj);
        }

        public ComplexNumber Add(ComplexNumber summand)
        {
            ComplexNumber summand2 = this;
            return new ComplexNumber()
            {
                RealPart = summand2.RealPart + summand.RealPart,
                ImaginaryPart = summand2.ImaginaryPart + summand.ImaginaryPart
            };
        }
        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            ComplexNumber minuent = this;
            return new ComplexNumber()
            {
                RealPart = minuent.RealPart - subtrahend.RealPart,
                ImaginaryPart = minuent.ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber factor)
        {
            ComplexNumber factor2 = this;
            return new ComplexNumber()
            {
                RealPart = factor2.RealPart * factor.RealPart - factor2.ImaginaryPart * factor.ImaginaryPart,
                ImaginaryPart = factor2.RealPart * factor.ImaginaryPart + factor2.ImaginaryPart * factor.RealPart
            };
        }
        public ComplexNumber Divide(ComplexNumber divisor)
        {
            ComplexNumber numerator = this.Multiply(new ComplexNumber() { RealPart = divisor.RealPart, ImaginaryPart = -divisor.ImaginaryPart });
            double denumerator = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denumerator,
                ImaginaryPart = numerator.ImaginaryPart / denumerator
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