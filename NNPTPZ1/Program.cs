namespace NNPTPZ1
{
    /// <summary>
    /// This program produces Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int _intArgsCount = 2;
        private const int _doubleArgsCount = 4;

        static void Main(string[] args)
        {
            // Parse values from string[] args.
            Arguments parsedArguments = ParseArguments(args);

            // Assign parsed arguments to variables.
            int bitmapWidth = parsedArguments.IntArgs[0];
            int bitmapHeight = parsedArguments.IntArgs[1];

            double minX = parsedArguments.DoubleArgs[0];
            double maxX = parsedArguments.DoubleArgs[1];
            double minY = parsedArguments.DoubleArgs[2];
            double maxY = parsedArguments.DoubleArgs[3];

            string fileOutputPath = parsedArguments.FileOutputPathArg;

            // Generate Newton fractal.
            NewtonFractal newtonFractal = new NewtonFractal(
                bitmapWidth, bitmapHeight, minX, maxX, minY, maxY);

            newtonFractal.GenerateNewtonFractal(fileOutputPath);
        }

        static Arguments ParseArguments(string[] args)
        {
            // Parse values from string[] args to Arguments.
            int[] intArgs = new int[_intArgsCount];
            double[] doubleArgs = new double[_doubleArgsCount];
            string fileOutputPathArg = args[_intArgsCount + _doubleArgsCount];

            for (int i = 0; i < intArgs.Length; i++)
                intArgs[i] = int.Parse(args[i]);

            for (int i = 0; i < doubleArgs.Length; i++)
                doubleArgs[i] = double.Parse(args[i + intArgs.Length]);

            return new Arguments
            {
                IntArgs = intArgs,
                DoubleArgs = doubleArgs,
                FileOutputPathArg = fileOutputPathArg
            };
        }

        class Arguments
        {
            public int[] IntArgs { get; set; }
            public double[] DoubleArgs { get; set; }
            public string FileOutputPathArg { get; set; }
        }
    }
}
