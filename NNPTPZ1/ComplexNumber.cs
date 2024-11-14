using System;

namespace NNPTPZ1
{
    public class ComplexNumber
    {

        public double RealPart { get; set; }
        public double Imaginari { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            Imaginari = 0
        };

        public ComplexNumber Add(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart + b.RealPart,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }
        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart - b.RealPart,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                RealPart = a.RealPart * b.RealPart - a.Imaginari * b.Imaginari,
                Imaginari = (float)(a.RealPart * b.Imaginari + a.Imaginari * b.RealPart)
            };
        }
        internal ComplexNumber Divide(ComplexNumber b)
        {
            ComplexNumber dividend = this.Multiply(new ComplexNumber() { RealPart = b.RealPart, Imaginari = -b.Imaginari });
            double divisor = b.RealPart * b.RealPart + b.Imaginari * b.Imaginari;

            return new ComplexNumber()
            {
                RealPart = dividend.RealPart / divisor,
                Imaginari = dividend.Imaginari / divisor
            };
        }
        public double GetAbS()
        {
            return Math.Sqrt(RealPart * RealPart + Imaginari * Imaginari);
        }

        public double GetAngleInRadian()
        {
            return Math.Atan(Imaginari / RealPart);
        }
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber compared = obj as ComplexNumber;
                return compared.RealPart == RealPart && compared.Imaginari == Imaginari;
            }
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return $"({RealPart} + {Imaginari}i)";
        }

    }
}
