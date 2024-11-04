using System;

namespace NNPTPZ1
{

    namespace Mathematics
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

            public ComplexNumber Add(ComplexNumber operandB)
            {
                ComplexNumber operandA = this;

                return new ComplexNumber()
                {
                    RealPart = operandA.RealPart + operandB.RealPart,
                    ImaginaryPart = operandA.ImaginaryPart + operandB.ImaginaryPart
                };
            }

            public ComplexNumber Subtract(ComplexNumber operandB)
            {
                ComplexNumber operandA = this;

                return new ComplexNumber()
                {
                    RealPart = operandA.RealPart - operandB.RealPart,
                    ImaginaryPart = operandA.ImaginaryPart - operandB.ImaginaryPart
                };
            }

            public ComplexNumber Multiply(ComplexNumber multiplicator)
            {
                // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
                ComplexNumber multiplicand = this;
                
                return new ComplexNumber()
                {
                    RealPart = multiplicand.RealPart * multiplicator.RealPart - multiplicand.ImaginaryPart * multiplicator.ImaginaryPart,
                    ImaginaryPart = (float)(multiplicand.RealPart * multiplicator.ImaginaryPart + multiplicand.ImaginaryPart * multiplicator.RealPart)
                };
            }

            public ComplexNumber Divide(ComplexNumber divisor)
            {
                // (aRe + aIm*i) / (bRe + bIm*i)
                // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
                //  bRe*bRe - bIm*bIm*i*i
                ComplexNumber numerator = Multiply(divisor.GetConjugate());
                double denominator = divisor.GetMagnitudeSquared();

                return new ComplexNumber()
                {
                    RealPart = numerator.RealPart / denominator,
                    ImaginaryPart = (float)(numerator.ImaginaryPart / denominator)
                };
            }

            public ComplexNumber GetConjugate()
            {
                return new ComplexNumber() { RealPart = RealPart, ImaginaryPart = -ImaginaryPart };
            }

            public double GetMagnitudeSquared()
            {
                return RealPart * RealPart + ImaginaryPart * ImaginaryPart;
            }

            public double GetAbsoluteValue()
            {
                return Math.Sqrt(GetMagnitudeSquared());
            }

            public double GetAngleInRadians()
            {
                return Math.Atan(ImaginaryPart / RealPart);
            }

            public double GetSquaredDistanceTo(ComplexNumber other)
            {
                double deltaReal = RealPart - other.RealPart;
                double deltaImaginary = ImaginaryPart - other.ImaginaryPart;

                return deltaReal * deltaReal + deltaImaginary * deltaImaginary;
            }

            public bool IsWithinDistance(ComplexNumber other, double threshold)
            {
                return GetSquaredDistanceTo(other) <= threshold;
            }

            public override string ToString()
            {
                return $"({RealPart} + {ImaginaryPart}i)";
            }

            public override bool Equals(object obj)
            {
                return obj is ComplexNumber number &&
                       RealPart == number.RealPart &&
                       ImaginaryPart == number.ImaginaryPart;
            }

            public override int GetHashCode()
            {
                int hashCode = 352033288;
                hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
                hashCode = hashCode * -1521134295 + ImaginaryPart.GetHashCode();
                return hashCode;
            }

        }
    }
}
