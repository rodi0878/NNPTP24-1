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
                ComplexNumber derivedCoefficient = Coefficients[i].Multiply(new ComplexNumber { RealPart = i });
                derivedPolynomial.Coefficients.Add(derivedCoefficient);
            }

            return derivedPolynomial;
        }

        /// <summary>
        /// Evaluates polynomial at given point: y = polynome(x)
        /// </summary>
        /// <param name="point">point of evaluation (x)</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(double point)
        {
            return Eval(new ComplexNumber { RealPart = point, ImaginaryPart = 0 });
        }

        /// <summary>
        /// Evaluates polynomial at given point
        /// </summary>
        /// <param name="point">point of evaluation</param>
        /// <returns>y</returns>
        public ComplexNumber Eval(ComplexNumber point)
        {
            ComplexNumber result = ComplexNumber.Zero;

            for (int i = 0; i < Coefficients.Count; i++)
            {
                ComplexNumber term = Coefficients[i];
                ComplexNumber baseValue = point; 
                int exponent = i;

                if (i > 0)
                {
                    for (int j = 0; j < exponent - 1; j++)
                        baseValue = baseValue.Multiply(point);

                    term = term.Multiply(baseValue);
                }

                result = result.Add(term);
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Coefficients.Count; i++)
            {
                sb.Append(Coefficients[i]);

                if (i > 0)
                {
                    for (int j = 0; j < i; j++)
                    {
                        sb.Append("x");
                    }
                }

                if (i + 1 < Coefficients.Count)
                {
                    sb.Append(" + ");
                }
            }

            return sb.ToString();
        }
    }
}