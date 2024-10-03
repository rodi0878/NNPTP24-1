using System.Collections.Generic;

namespace NNPTPZ1.Mathematics
{
	public class Polynome
	{
		/// <summary>
		/// Coefficients
		/// </summary>
		public List<ComplexNumber> Coefficients { get; set; }

		/// <summary>
		/// Constructor
		/// </summary>
		public Polynome(params ComplexNumber[] coefficients)
		{
			Coefficients = new List<ComplexNumber>(coefficients);
		}

		public void Add(ComplexNumber coefficients)
		{
			Coefficients.Add(coefficients);
		}

		/// <summary>
		/// Derives this polynomial and creates new one
		/// </summary>
		/// <returns>Derivated polynomial</returns>
		public Polynome Derive()
		{
			Polynome polynome = new Polynome();
			for (int i = 1; i < Coefficients.Count; i++)
			{
				ComplexNumber multipliedCoefficient = Coefficients[i].Multiply(new ComplexNumber() { RealNumber = i });
				polynome.Coefficients.Add(multipliedCoefficient);
			}

			return polynome;
		}

		/// <summary>
		/// Evaluates polynomial at given point
		/// </summary>
		/// <param name="point">point of evaluation</param>
		/// <returns>result of evaluation</returns>
		public ComplexNumber Evaluate(double point)
		{
			return Evaluate(new ComplexNumber() { RealNumber = point, ImaginaryNumber = 0 });
		}

		/// <summary>
		/// Evaluates polynomial at given point
		/// </summary>
		/// <param name="point">point of evaluation</param>
		/// <returns>result of evaluation</returns>
		public ComplexNumber Evaluate(ComplexNumber point)
		{
			ComplexNumber sum = ComplexNumber.Zero;

			for (int degree = 0; degree < Coefficients.Count; degree++)
			{
				ComplexNumber coefficient = Coefficients[degree];
				ComplexNumber term = coefficient;

				if (degree > 0)
				{
					ComplexNumber baseTerm = point;
					for (int exponent = 1; exponent < degree; exponent++)
					{
						baseTerm = baseTerm.Multiply(point);
					}
					term = coefficient.Multiply(baseTerm);
				}

				sum = sum.Add(term);
			}

			return sum;
		}

		/// <summary>
		/// ToString
		/// </summary>
		/// <returns>String representation of polynomial</returns>
		public override string ToString()
		{
			var result = "";

			for (int i = 0; i < Coefficients.Count; i++)
			{
				result += Coefficients[i].ToString();

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