using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public float ImaginaryPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginaryPart = 0
        };

        public ComplexNumber(double realPart, float imaginaryPart)
        {
            RealPart = realPart;
            ImaginaryPart = imaginaryPart;
        }

        public ComplexNumber() { }

        public ComplexNumber Multiply(ComplexNumber complexNumber)
        {
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                RealPart = RealPart * complexNumber.RealPart - ImaginaryPart * complexNumber.ImaginaryPart,
                ImaginaryPart = (float)(RealPart * complexNumber.ImaginaryPart + ImaginaryPart * complexNumber.RealPart)
            };
        }

        public ComplexNumber Add(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + complexNumber.RealPart,
                ImaginaryPart = ImaginaryPart + complexNumber.ImaginaryPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber complexNumber)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - complexNumber.RealPart,
                ImaginaryPart = ImaginaryPart - complexNumber.ImaginaryPart
            };
        }

        public ComplexNumber Divide(ComplexNumber complexNumber)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            // bRe*bRe - bIm*bIm*i*i
            var dividend = this.Multiply(new ComplexNumber() { RealPart = complexNumber.RealPart, ImaginaryPart = -complexNumber.ImaginaryPart });
            var devider = complexNumber.RealPart * complexNumber.RealPart + complexNumber.ImaginaryPart * complexNumber.ImaginaryPart;

            return new ComplexNumber()
            {
                RealPart = dividend.RealPart / devider,
                ImaginaryPart = (float)(dividend.ImaginaryPart / devider)
            };
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber complexNumber = obj as ComplexNumber;
                return complexNumber.RealPart == RealPart && complexNumber.ImaginaryPart == ImaginaryPart;
            }
            return base.Equals(obj);
        }

        public double GetAngleInRadians() => Math.Atan(ImaginaryPart / RealPart);
        public double GetAbsoluteValue() => Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        public override string ToString() => $"({RealPart} + {ImaginaryPart}i)";
    }
}
