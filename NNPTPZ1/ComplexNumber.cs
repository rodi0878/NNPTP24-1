using System;

namespace NNPTPZ1.Mathematics
{
	public class ComplexNumber
	{
		public double RealNumber { get; set; }
		public double ImaginaryNumber { get; set; }

		public readonly static ComplexNumber Zero = new ComplexNumber()
		{
			RealNumber = 0,
			ImaginaryNumber = 0
		};

		public override bool Equals(object obj)
		{
			if (obj is ComplexNumber complexNumber)
			{
				return complexNumber.RealNumber == RealNumber && complexNumber.ImaginaryNumber == ImaginaryNumber;
			}
			return base.Equals(obj);
		}

		public ComplexNumber Multiply(ComplexNumber other)
		{
			ComplexNumber multiplicand = this;
			// aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
			return new ComplexNumber()
			{
				RealNumber = multiplicand.RealNumber * other.RealNumber - multiplicand.ImaginaryNumber * other.ImaginaryNumber,
				ImaginaryNumber = (multiplicand.RealNumber * other.ImaginaryNumber + multiplicand.ImaginaryNumber * other.RealNumber)
			};
		}

		public ComplexNumber Add(ComplexNumber other)
		{
			ComplexNumber addend = this;
			return new ComplexNumber()
			{
				RealNumber = addend.RealNumber + other.RealNumber,
				ImaginaryNumber = addend.ImaginaryNumber + other.ImaginaryNumber
			};
		}

		internal ComplexNumber Divide(ComplexNumber divisor)
		{
			ComplexNumber dividend = this;
			var conjugate = new ComplexNumber() { RealNumber = divisor.RealNumber, ImaginaryNumber = -divisor.ImaginaryNumber };
			var tmp = dividend.Multiply(conjugate);
			var denominator = divisor.RealNumber * divisor.RealNumber + divisor.ImaginaryNumber * divisor.ImaginaryNumber;

			return new ComplexNumber()
			{
				RealNumber = tmp.RealNumber / denominator,
				ImaginaryNumber = (float)(tmp.ImaginaryNumber / denominator)
			};
		}

		public ComplexNumber Subtract(ComplexNumber other)
		{
			ComplexNumber minuend = this;
			return new ComplexNumber()
			{
				RealNumber = minuend.RealNumber - other.RealNumber,
				ImaginaryNumber = minuend.ImaginaryNumber - other.ImaginaryNumber
			};
		}

		public double GetAbsoluteValue()
		{
			return Math.Sqrt(RealNumber * RealNumber + ImaginaryNumber * ImaginaryNumber);
		}

		public double GetAngleInRadians()
		{
			return Math.Atan(ImaginaryNumber / RealNumber);
		}

		public override string ToString()
		{
			return $"({RealNumber} + {ImaginaryNumber}i)";
		}

	}
}

