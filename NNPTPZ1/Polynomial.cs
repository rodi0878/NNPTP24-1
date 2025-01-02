using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        /// <summary>
        /// Coefficients of the polynomial
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Initializes a new list of coefficients
        /// </summary>
        public Polynomial()
        {
            Coefficients = new List<ComplexNumber>();
        }
        /// <summary>
        /// Adds a coefficient to the polynomial.
        /// </summary>
        /// <param name="coefficient">The coefficient to add</param>
        public void Add(ComplexNumber coefficient)
        {
            Coefficients.Add(coefficient);
        }

        /// <summary>
        /// Derives the polynomial and returns a new derived polynomial.
        /// </summary>
        /// <returns>Derivated polynomial</returns>
        public Polynomial Derive()
        {
            Polynomial derivedPolynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                derivedPolynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }

            return derivedPolynomial;
        }

        /// <summary>
        /// Evaluates the polynomial at a given real number.
        /// </summary>
        /// <param name="point">The point of evaluation</param>
        /// <returns>The value of the polynomial at the given point.</returns>
        public ComplexNumber Evaluate(double point)
        {
            return Evaluate(new ComplexNumber() { RealPart = point, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates the polynomial at a given complex number.
        /// </summary>
        /// <param name="point">The point of evaluation</param>
        /// <returns>The value of the polynomial at the given point.</returns>
        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber term = point;

                if (i > 0)
                {
                    for (int j = 0; j < i - 1; j++)
                    {
                        term = term.Multiply(point);
                    }

                    coefficient = coefficient.Multiply(term);
                }

                result = result.Add(coefficient);
            }

            return result;
        }

        /// <summary>
        /// Returns a string representation of the polynomial.
        /// </summary>
        /// <returns>A string representation of the polynomial.</returns>
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                result.Append(Coefficients[i]);

                if (i > 0)
                {
                    result.Append(new string('x', i));
                }
                if (i + 1 < Coefficients.Count)
                {
                    result.Append(" + ");
                }
            }
            return result.ToString();
        }
    }
}