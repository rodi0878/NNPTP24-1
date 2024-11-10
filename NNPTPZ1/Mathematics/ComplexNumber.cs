using System;

namespace NNPTPZ1.Mathematics
{
    public class ComplexNumber
    {
        public double RealPart { get; set; }
        public double ImaginariPart { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            RealPart = 0,
            ImaginariPart = 0
        };

        public override bool Equals(object other)
        {
            if (other is ComplexNumber complexNumber)
            {
                return complexNumber.RealPart == RealPart && complexNumber.ImaginariPart == ImaginariPart;
            }
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            int hashCode = 1303117702;
            hashCode = hashCode * -1521134295 + RealPart.GetHashCode();
            hashCode = hashCode * -1521134295 + ImaginariPart.GetHashCode();
            return hashCode;
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart + addend.RealPart,
                ImaginariPart = ImaginariPart + addend.ImaginariPart
            };
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart - subtrahend.RealPart,
                ImaginariPart = ImaginariPart - subtrahend.ImaginariPart
            };
        }

        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            return new ComplexNumber()
            {
                RealPart = RealPart * multiplier.RealPart - ImaginariPart * multiplier.ImaginariPart,
                ImaginariPart = RealPart * multiplier.ImaginariPart + ImaginariPart * multiplier.RealPart
            };
        }

        public ComplexNumber Divide(ComplexNumber divisor)
        {
            ComplexNumber conjugate = new ComplexNumber() { RealPart = divisor.RealPart, ImaginariPart = -divisor.ImaginariPart };

            ComplexNumber numerator = Multiply(conjugate);

            double denominator = divisor.RealPart * divisor.RealPart + divisor.ImaginariPart * divisor.ImaginariPart;

            return new ComplexNumber()
            {
                RealPart = numerator.RealPart / denominator,
                ImaginariPart = numerator.ImaginariPart / denominator
            };
        }

        public double GetAngleInRadians()
        {
            return Math.Atan(ImaginariPart / RealPart);
        }

        public double GetAbsoluteValue()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginariPart * ImaginariPart);
        }

        public override string ToString()
        {
            return $"({RealPart} + {ImaginariPart}i)";
        }
    }
}
