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

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryPart / RealPart);
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + addend.RealPart,
                ImaginaryPart = ImaginaryPart + addend.ImaginaryPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - subtrahend.RealPart,
                ImaginaryPart = ImaginaryPart - subtrahend.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplicand)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * multiplicand.RealPart - ImaginaryPart * multiplicand.ImaginaryPart,
                ImaginaryPart = (RealPart * multiplicand.ImaginaryPart + ImaginaryPart * multiplicand.RealPart)
            };
        }

        public ComplexNumber Divide(ComplexNumber divisor)
        {
            var numerator = Multiply(new ComplexNumber() { RealPart = divisor.RealPart, ImaginaryPart = -divisor.ImaginaryPart });
            var denominator = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denominator,
                ImaginaryPart = (numerator.ImaginaryPart / denominator)
            };
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginaryPart}i)";
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber x)
            {
                return x.RealPart == RealPart && x.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }
    }
}
