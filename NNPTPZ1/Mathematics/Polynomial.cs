using System.Collections.Generic;
using System.Text;

namespace NNPTPZ1.Mathematics
{
    public class Polynomial
    {
        public List<ComplexNumber> Coefficients { get; set; }

        public Polynomial() 
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
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">point of evaluation</param>
        /// <returns>Representing the value of the polynomial at the given point as ComplexNumber</returns>
        public ComplexNumber Evaluate(double point)
        {
            return Evaluate(new ComplexNumber() { RealPart = point, ImaginariPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Evaluate(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;
            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber coefficient = Coefficients[i];
                ComplexNumber powerOfX = point;
                int power = i;

                if (i > 0)
                {
                    for (int j = 0; j < power - 1; j++)
                    {
                        powerOfX = powerOfX.Multiply(point);
                    }

                    coefficient = coefficient.Multiply(powerOfX);
                }

                result = result.Add(coefficient);
            }
            return result;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>String representation  of polynomial</returns>
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
