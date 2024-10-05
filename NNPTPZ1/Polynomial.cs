using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
	public class Polynomial
	{
		public List<ComplexNumber> Coefficients { get; set; } = new List<ComplexNumber>();

		/// <summary>
		/// Derives this polynomial and creates new one
		/// </summary>
		/// <returns>Derivated polynomial</returns>
		public Polynomial Derive()
		{
			var derivedPolynomial = new Polynomial();
			for (int index = 1; index < Coefficients.Count; index++)
			{
				var derivedCoefficient = Coefficients[index].Multiply(new ComplexNumber { Real = index });
				derivedPolynomial.Coefficients.Add(derivedCoefficient);
			}
			return derivedPolynomial;
		}

		/// <summary>
		/// Evaluates polynomial at given point
		/// </summary>
		/// <param name="x">Point of evaluation</param>
		/// <returns>The resulting complex number after evaluation of the polynomial.</returns>
		public ComplexNumber Evaluate(ComplexNumber x)
		{
			var sum = ComplexNumber.Zero;

			for (int i = 0; i < Coefficients.Count; i++)
			{
				var coefficient = Coefficients[i];

				if (i > 0)
				{
					var baseX = x;
					for (int j = 0; j < i - 1; j++)
					{
						baseX = baseX.Multiply(x);
					}
					coefficient = coefficient.Multiply(baseX);
				}

				sum = sum.Add(coefficient);
			}

			return sum;
		}

		public override string ToString()
		{
			var result = string.Empty;

			for (int i = 0; i < Coefficients.Count; i++)
			{
				result += Coefficients[i];

				if (i > 0)
				{
					result += new string('x', i);
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
