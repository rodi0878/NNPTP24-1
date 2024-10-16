using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double RealNumber { get; set; }
        public double ImaginaryNumber { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealNumber = 0,
            ImaginaryNumber = 0
        };

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber
            {
                RealNumber = this.RealNumber + other.RealNumber,
                ImaginaryNumber = this.ImaginaryNumber + other.ImaginaryNumber
            };
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber
            {
                RealNumber = this.RealNumber - other.RealNumber,
                ImaginaryNumber = this.ImaginaryNumber - other.ImaginaryNumber
            };
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealNumber = this.RealNumber * other.RealNumber - this.ImaginaryNumber * other.ImaginaryNumber,
                ImaginaryNumber = this.RealNumber * other.ImaginaryNumber + this.ImaginaryNumber * other.RealNumber
            };
        }

        internal ComplexNumber Divide(ComplexNumber other)
        {
            
            ComplexNumber conjugate = this.Multiply(new ComplexNumber() { RealNumber = other.RealNumber, ImaginaryNumber = -other.ImaginaryNumber });
            double denominator = other.RealNumber * other.RealNumber + other.ImaginaryNumber * other.ImaginaryNumber;

            return new ComplexNumber()
            {
                RealNumber = conjugate.RealNumber / denominator,
                ImaginaryNumber = conjugate.ImaginaryNumber / denominator
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealNumber * RealNumber + ImaginaryNumber * ImaginaryNumber);
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginaryNumber / RealNumber);
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber comparand = obj as ComplexNumber;
                return comparand.RealNumber == RealNumber && comparand.ImaginaryNumber == ImaginaryNumber;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({RealNumber} + {ImaginaryNumber}i)";
        }
    }
}
