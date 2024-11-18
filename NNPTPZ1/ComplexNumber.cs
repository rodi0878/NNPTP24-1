using System;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        /// <summary>
        /// Class represents Complex Number with real and imaginary parts.
        /// Provides methods for basic arithmetic operations
        /// </summary>
        public class ComplexNumber
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }

            public static readonly ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };



            public ComplexNumber Subtract(ComplexNumber subtrahend)
            {
                return new ComplexNumber()
                {
                    Real = this.Real - subtrahend.Real,
                    Imaginary = this.Imaginary - subtrahend.Imaginary
                };
            }
            public ComplexNumber Add(ComplexNumber addend)
            {
                return new ComplexNumber()
                {
                    Real = this.Real + addend.Real,
                    Imaginary = this.Imaginary + addend.Imaginary
                };
            }
            public ComplexNumber Multiply(ComplexNumber multiplier)
            {
                return new ComplexNumber()
                {
                    Real = this.Real * multiplier.Real - this.Imaginary * multiplier.Imaginary,
                    Imaginary = (float)(this.Real * multiplier.Imaginary + this.Imaginary * multiplier.Real)
                };
            }
            public ComplexNumber Divide(ComplexNumber divisor)
            {
                var numerator = Multiply(new ComplexNumber() { Real = divisor.Real, Imaginary = -divisor.Imaginary });
                var denominator = divisor.Real * divisor.Real + divisor.Imaginary * divisor.Imaginary;

                return new ComplexNumber()
                {
                    Real = numerator.Real / denominator,
                    Imaginary = (float)(numerator.Imaginary / denominator)
                };
            }
            public double GetAbsolutValue()
            {
                return Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }


            public double GetAngleInRadians()
            {
                return Math.Atan(Imaginary / Real);
            }


            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }
            public override bool Equals(object obj)
            {
                if (obj is ComplexNumber)
                {
                    ComplexNumber comparand = obj as ComplexNumber;
                    return comparand.Real == Real && comparand.Imaginary == Imaginary;
                }
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
