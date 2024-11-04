using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1
{

    namespace Mathematics
    {
        public class Polynome
        {
            public List<ComplexNumber> Coefficients { get; set; }

            public Polynome() => Coefficients = new List<ComplexNumber>();

            public void Add(ComplexNumber coefficient) => Coefficients.Add(coefficient);

            /// <summary>
            /// Creates a derivation of polynome.
            /// </summary>
            /// <returns>Returns the derivated polynomial.</returns>
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
            /// Evaluates the polynomial at given point.
            /// </summary>
            /// <param name="evaluationPoint">Point to be evaluated.</param>
            /// <returns>Returns evaluated point.</returns>
            public ComplexNumber Evaluate(ComplexNumber evaluationPoint)
            {
                ComplexNumber result = ComplexNumber.Zero;

                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber baseToPower = evaluationPoint;

                    if (i > 0)
                    {
                        for (int j = 0; j < i - 1; j++)
                        {
                            baseToPower = baseToPower.Multiply(evaluationPoint);
                        }

                        coefficient = coefficient.Multiply(baseToPower);
                    }

                    result = result.Add(coefficient);
                }

                return result;
            }

            /// <summary>
            /// Evaluates the polynomial at given point.
            /// </summary>
            /// <param name="evaluationPoint">Point to be evaluated.</param>
            /// <returns>Returns evaluated point.</returns>
            public ComplexNumber Evaluate(double evaluationPoint)
            {
                return Evaluate(new ComplexNumber() { RealPart = evaluationPoint, ImaginaryPart = 0 });
            }

            /// <summary>
            /// ToString
            /// </summary>
            /// <returns>String repr of polynomial</returns>
            public override string ToString()
            {
                StringBuilder strBuilder = new StringBuilder();

                for (int i = 0; i < Coefficients.Count; i++)
                {
                    strBuilder.Append(Coefficients[i]);

                    if (i > 0)
                    {
                        strBuilder.Append("x");
                        strBuilder.Append("^" + i);
                    }

                    if (i + 1 < Coefficients.Count)
                    {
                        strBuilder.Append(" + ");
                    }
                }

                return strBuilder.ToString();
            }
        }
    }
}
