namespace NNPTPZ1.Mathematics
{
	public class ComplexNumber
	{
		public double Real { get; set; }
		public double Imaginary { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is ComplexNumber other)
			{
				return other.Real == Real && other.Imaginary == Imaginary;
			}
			return false;
		}

		public static readonly ComplexNumber Zero = new ComplexNumber
		{
			Real = 0,
			Imaginary = 0
		};

		public ComplexNumber Multiply(ComplexNumber other)
		{
			return new ComplexNumber
			{
				Real = Real * other.Real - Imaginary * other.Imaginary,
				Imaginary = Real * other.Imaginary + Imaginary * other.Real
			};
		}

		public ComplexNumber Add(ComplexNumber other)
		{
			return new ComplexNumber
			{
				Real = Real + other.Real,
				Imaginary = Imaginary + other.Imaginary
			};
		}

		public ComplexNumber Subtract(ComplexNumber other)
		{
			return new ComplexNumber
			{
				Real = Real - other.Real,
				Imaginary = Imaginary - other.Imaginary
			};
		}

		public override string ToString()
		{
			return $"({Real} + {Imaginary}i)";
		}

		public ComplexNumber Divide(ComplexNumber other)
		{
			var denominator = other.Real * other.Real + other.Imaginary * other.Imaginary;

			return new ComplexNumber
			{
				Real = (Real * other.Real + Imaginary * other.Imaginary) / denominator,
				Imaginary = (Imaginary * other.Real - Real * other.Imaginary) / denominator
			};
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
