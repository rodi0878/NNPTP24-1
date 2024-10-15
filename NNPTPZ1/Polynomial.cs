using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        /// <summary>
        /// Computes the derivative of the polynomial and returns a new polynomial.
        /// </summary>
        /// <returns>A new polynomial representing the derivative of the original polynomial.</returns>
        public Polynomial Derive()
        {
            Polynomial polynomial = new Polynomial();
            for (int i = 1; i < Coefficients.Count; i++)
            {
                polynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { RealPart = i }));
            }
            return polynomial;
        }

        /// <summary>
        /// Evaluates polynomial at a real point.
        /// </summary>
        /// <param name="x">The real point at which the polynomial is evaluated.</param>
        /// <returns>The result of the polynomial evaluation as a ComplexNumber.</returns>
        public ComplexNumber Eval(double x) => Eval(new ComplexNumber() { RealPart = x, ImaginaryPart = 0 });

        public void Add(ComplexNumber coefficients) => Coefficients.Add(coefficients);

        /// <summary>
        /// Evaluates polynomial at given complex number.
        /// </summary>
        /// <param name="complexNumber">The point at which the polynomial is evaluated.</param>
        /// <returns>The result of the polynomial evaluation at the given point.</returns>
        public ComplexNumber Eval(ComplexNumber complexNumber)
        {
            ComplexNumber resultComplexNumber = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber term = complexNumber;
                int exponent = i;

                if (exponent > 0)
                {
                    for (int j = 0; j < exponent - 1; j++)
                        term = term.Multiply(complexNumber);

                    coefficient = coefficient.Multiply(term);
                }

                resultComplexNumber = resultComplexNumber.Add(coefficient);
            }

            return resultComplexNumber;
        }

        /// <summary>
        /// Returns a string representation of the polynomial.
        /// </summary>
        /// <returns>String representation of the polynomial with coefficients and powers of 'x'.</returns>
        public override string ToString()
        {
            string termStrings = "";
            for (int i = 0; i < Coefficients.Count; i++)
            {
                termStrings += Coefficients[i];

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        termStrings += "x";
                    }
                }

                if (i + 1 < Coefficients.Count)
                {
                    termStrings += " + ";
                }
            }
            return termStrings;
        }
    }
}
