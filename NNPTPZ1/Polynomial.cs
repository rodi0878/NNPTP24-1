using System.Collections.Generic;
using System.Linq;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynomial
        {
            public List<ComplexNumber> Coefficients { get; set; }


            public Polynomial()
            {
                Coefficients = new List<ComplexNumber>();
            }

            public Polynomial(params ComplexNumber[] coefficients)
            {
                Coefficients = coefficients.ToList();
            }


            public void Add(ComplexNumber coefficient)
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
                    derivedPolynomial.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Re = i }));
                }

                return derivedPolynomial;
            }

            /// <summary>
            /// Evaluates the polynomial at a given real point.
            /// </summary>
            /// <param name="x">The point of evaluation.</param>
            /// <returns>The value of the polynomial at the given point.</returns>
            public ComplexNumber Eval(double x)
            {
                return Eval(new ComplexNumber() { Re = x, Im = 0 });
            }

            /// <summary>
            /// Evaluates the polynomial at a given complex point.
            /// </summary>
            /// <param name="x">The point of evaluation.</param>
            /// <returns>The value of the polynomial at the given point.</returns>
            public ComplexNumber Eval(ComplexNumber x)
            {
                ComplexNumber value = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber bx = x;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            bx = bx.Multiply(x);

                        coefficient = coefficient.Multiply(bx);
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
                    if (i+1<Coefficients.Count)
                    {
                        result += " + ";
                    }
                }
                return result;
            }
        }
    }
}
