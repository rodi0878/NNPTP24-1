using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    internal class ComplexNumber
    {

        public double Real { get; set; }
        public float Imaginari { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber x = obj as ComplexNumber;
                return x.Real == Real && x.Imaginari == Imaginari;
            }
            return base.Equals(obj);
        }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Real = 0,
            Imaginari = 0
        };

        public ComplexNumber Multiply(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Real = a.Real * b.Real - a.Imaginari * b.Imaginari,
                Imaginari = (float)(a.Real * b.Imaginari + a.Imaginari * b.Real)
            };
        }
        public double GetAbsoluteValue()
        {
            return Math.Sqrt(Real * Real + Imaginari * Imaginari);
        }

        public ComplexNumber Add(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Real = a.Real + b.Real,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(Imaginari / Real);
        }
        public ComplexNumber Subtract(ComplexNumber b)
        {
            ComplexNumber a = this;
            return new ComplexNumber()
            {
                Real = a.Real - b.Real,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginari}i)";
        }

        internal ComplexNumber Divide(ComplexNumber b)
        {
            var complexConjugate = this.Multiply(new ComplexNumber() { Real = b.Real, Imaginari = -b.Imaginari });
            var modulusSquared = b.Real * b.Real + b.Imaginari * b.Imaginari;

            return new ComplexNumber()
            {
                Real = complexConjugate.Real / modulusSquared,
                Imaginari = (float)(complexConjugate.Imaginari / modulusSquared)
            };
        }
    }

}
