using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
    public class Polynome
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynome()
        {
            Coefficients = new List<ComplexNumber>();
        }

        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        /// <summary>
        /// Derives this polynomial and creates new one
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynome Derive()
        {
            Polynome derivedPolynome = new Polynome();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                var coefficient = Coefficients[i].Multiply(new ComplexNumber() { RealPart = i });
                derivedPolynome.Coefficients.Add(coefficient);
            }

            return derivedPolynome;
        }

        /// <summary>
        /// Evaluates polynomial at given point: y = p(x)
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double x)
        {
            var y = Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
            return y;
        }

        /// <summary>
        /// Evaluates polynomial at given point: y = p(x)
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber x)
        {
            ComplexNumber value = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber powerOfX = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        powerOfX = powerOfX.Multiply(x);
                    }

                    coefficient = coefficient.Multiply(powerOfX);
                }

                value = value.Add(coefficient);
            }

            return value;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string value = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                value += Coefficients[i];
                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        value += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                {
                    value += " + ";
                }
            }
            return value;
        }
    }
}
