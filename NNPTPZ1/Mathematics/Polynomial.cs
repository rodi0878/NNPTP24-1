using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNPTPZ1.Mathematics
{
    internal class Polynomial
    {

        /// <summary>
        /// Coe
        /// </summary>
        public List<ComplexNumber> Coefficients { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Polynomial() => Coefficients = new List<ComplexNumber>();

        public void AddCoefficient(ComplexNumber coefficient) =>
            Coefficients.Add(coefficient);

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
        /// <param name="pointOfEvaluation">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double pointOfEvaluation)
        {
            return Eval(new ComplexNumber() { Real = pointOfEvaluation, Imaginary = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="pointOfEvaluation">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber pointOfEvaluation)
        {
            ComplexNumber sum = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber currentPower = pointOfEvaluation;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                        currentPower = currentPower.Multiply(pointOfEvaluation);

                    coefficient = coefficient.Multiply(currentPower);
                }

                sum = sum.Add(coefficient);
            }

            return sum;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String repr of polynomial</returns>
        public override string ToString()
        {
            string result = "";
            for (int i = 0 ; i < Coefficients.Count; i++)
            {
                result += Coefficients[i];
                if (i > 0)
                {
                    int j = 0;
                    for (; j < i; j++)
                    {
                        result += "x";
                    }
                }
                if (i + 1 < Coefficients.Count)
                    result += " + ";
            }
            return result;
        }

    }

}
