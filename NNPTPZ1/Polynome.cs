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
                var derivedCoefficient = Coefficients[i].Multiply(new ComplexNumber() { RealPart = i });

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
            return Evaluate(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });
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
                ComplexNumber powerOfX = x;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        powerOfX = powerOfX.Multiply(x);

                    coefficient = coefficient.Multiply(powerOfX);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation of polynomial</returns>
        public override string ToString()
        {
            string polynomialString = string.Empty;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                if (i > 0)
                {
                    polynomialString += " + ";
                }

                polynomialString += Coefficients[i];

                if (i > 0)
                {
                    polynomialString += new string('x', i);
                }
            }

            return polynomialString;
        }
    }
}