using System.Collections.Generic;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        /// <summary>
        /// Represents a polynomial with coefficients as complex numbers
        /// Provides functionality for polynomial evaluation, derivation, and string representation
        /// </summary>
        public class Polynomial
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Polynomial()
            {
                Coefficients = new List<ComplexNumber>();
            }

            public void AddCoefficient(ComplexNumber coefficient)
            {
                Coefficients.Add(coefficient);
            }

            /// <summary>
            /// Derives this polynomial and creates new one
            /// </summary>
            /// <returns>Derivated polynomial</returns>
            public Polynomial Derive()
            {
                Polynomial derivedPolynomial = new Polynomial();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    derivedPolynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
                }

                return derivedPolynomial;
            }
            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Evaluate(double x)
            {
                return Evaluate(new ComplexNumber() { Real = x, Imaginary = 0 });
            }

            /// <summary>
            /// Evaluates polynomial at given point
            /// </summary>
            /// <param name="x">point of evaluation</param>
            /// <returns>y</returns>
            public ComplexNumber Evaluate(ComplexNumber x)
            {
                ComplexNumber value = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber baseX = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            baseX = baseX.Multiply(x);

                        coefficient = coefficient.Multiply(baseX);
                    }

                    value = value.Add(coefficient);
                }

                return value;
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                string result = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    result += Coefficients[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            result += "x";
                        }
                    }
                    if (i + 1 < Coefficients.Count)
                    {
                        result += " + ";
                    }
                }
                return result;
            }
        }
    }
}
