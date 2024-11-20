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

        public Polynome(List<ComplexNumber> coefficients)
        {
            Coefficients = coefficients ?? new List<ComplexNumber>();
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
                ComplexNumber derivedCoefficient = Coefficients[i].Multiply(new ComplexNumber { RealPart = i });
                derivedPolynome.Coefficients.Add(derivedCoefficient);
            }
            return derivedPolynome;
        }

        /// <summary>
        /// Evaluates polynomial at given point: y = polynome(x)
        /// </summary>
        /// <param name="x">point of evaluation (x)</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(double x)
        {
            return Evaluate(new ComplexNumber { RealPart = x });
        }

        private ComplexNumber Power(ComplexNumber baseNumber, int exponent)
        {
            ComplexNumber result = baseNumber;
            for (int i = 1; i < exponent; i++)
            {
                result = result.Multiply(baseNumber);
            }
            return result;
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="x">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber x)
        {
            ComplexNumber result = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber term = coefficient;

                if (i > 0)
                {
                    term = coefficient.Multiply(Power(x, i));
                }

                result = result.Add(term);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string polynomeString = string.Empty;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (i > 0)
                {
                    polynomeString += " + ";
                }

                polynomeString += Coefficients[i];

                if (i > 0)
                {
                    polynomeString += new string('x', i);
                }
            }

            return polynomeString;
        }
    }
}