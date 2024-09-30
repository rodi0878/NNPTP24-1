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
            if (obj is ComplexNumber)
            {
                ComplexNumber other = obj as ComplexNumber;
                return other.RealPart == RealPart && other.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = 1382181547;
            hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
            hashCode = hashCode * -1521134295 + ImaginaryPart.GetHashCode();
            return hashCode;
        }

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart + other.RealPart,
                ImaginaryPart = this.ImaginaryPart + other.ImaginaryPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber()
            {
                RealPart = this.RealPart - other.RealPart,
                ImaginaryPart = this.ImaginaryPart - other.ImaginaryPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealPart = this.RealPart * other.RealPart - this.ImaginaryPart * other.ImaginaryPart,
                ImaginaryPart = this.RealPart * other.ImaginaryPart + this.ImaginaryPart * other.RealPart
            };
        }

        public ComplexNumber Divide(ComplexNumber other)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i

            ComplexNumber conjugate = new ComplexNumber()
            {
                RealPart = other.RealPart,
                ImaginaryPart = -other.ImaginaryPart
            };
            ComplexNumber numerator = this.Multiply(conjugate);
            var denominator = other.RealPart * other.RealPart + other.ImaginaryPart * other.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denominator,
                ImaginaryPart = numerator.ImaginaryPart / denominator
            };
        }

        public double GetAbsValue()
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