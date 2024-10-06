using System;

namespace NNPTPZ1.Mathematics
{

    public class ComplexNumber
    {
        public double Re { get; set; }
        public double Im { get; set; }

        public readonly static ComplexNumber Zero = new ComplexNumber()
        {
            Re = 0,
            Im = 0
        };

        public ComplexNumber Multiply(ComplexNumber multiplier)
        {
            ComplexNumber multiplicand = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new ComplexNumber()
            {
                Re = multiplicand.Re * multiplier.Re - multiplicand.Im * multiplier.Im,
                Im = multiplicand.Re * multiplier.Im + multiplicand.Im * multiplier.Re
            };
        }

        public ComplexNumber Add(ComplexNumber addend)
        {
            ComplexNumber augend = this;
            return new ComplexNumber()
            {
                Re = augend.Re + addend.Re,
                Im = augend.Im + addend.Im
            };
        }

        public ComplexNumber Subtract(ComplexNumber subtrahend)
        {
            ComplexNumber minuend = this;
            return new ComplexNumber()
            {
                Re = minuend.Re - subtrahend.Re,
                Im = minuend.Im - subtrahend.Im
            };
        }

        public ComplexNumber Divide(ComplexNumber divisor)
        {
            // (aRe + aIm*i) / (bRe + bIm*i)
            // ((aRe + aIm*i) * (bRe - bIm*i)) / ((bRe + bIm*i) * (bRe - bIm*i))
            //  bRe*bRe - bIm*bIm*i*i
            ComplexNumber numerator = Multiply(new ComplexNumber() { 
                                                Re = divisor.Re, 
                                                Im = -divisor.Im 
                                            });

            double denominator = divisor.Re * divisor.Re + divisor.Im * divisor.Im;

            return new ComplexNumber()
                    {
                        Re = numerator.Re / denominator,
                        Im = numerator.Im / denominator
                    };
        }

        public double GetAbs()
        {
            return Math.Sqrt(Re * Re + Im * Im);
        }


        public double GetAngleInRadiants()
        {
            return Math.Atan(Im / Re);
        }

        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber)
            {
                ComplexNumber comparand = obj as ComplexNumber;
                return comparand.Re == Re && comparand.Im == Im;
            }
            return base.Equals(obj);
        }


        public override string ToString()
        {
            return $"({Re} + {Im}i)";
        }


    }
}
