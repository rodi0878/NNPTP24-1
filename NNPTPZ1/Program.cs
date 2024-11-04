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

            // Generate Newton fractal.
            NewtonFractal newtonFractal = new NewtonFractal(
                parsedArguments.BitmapWidth, parsedArguments.BitmapHeight,
                parsedArguments.MinX, parsedArguments.MaxX,
                parsedArguments.MinY, parsedArguments.MaxY);

            newtonFractal.GenerateNewtonFractal(parsedArguments.FileOutputPath);
        }

        static Arguments ParseArguments(string[] args)
        {
            // Parse values from string[] args.
            int[] intArgs = new int[_intArgsCount];
            double[] doubleArgs = new double[_doubleArgsCount];
            string fileOutputPathArg = args[_intArgsCount + _doubleArgsCount];

            for (int i = 0; i < intArgs.Length; i++)
                intArgs[i] = int.Parse(args[i]);

            for (int i = 0; i < doubleArgs.Length; i++)
                doubleArgs[i] = double.Parse(args[i + intArgs.Length]);

            // Put parsed values into Arguments object and return it.
            Arguments parsedArguments = new Arguments
            {
                BitmapWidth = intArgs[0],
                BitmapHeight = intArgs[1],

                MinX = doubleArgs[0],
                MaxX = doubleArgs[1],
                MinY = doubleArgs[2],
                MaxY = doubleArgs[3],

                FileOutputPath = fileOutputPathArg
            };

            return parsedArguments;
        }

        class Arguments
        {
            public int BitmapWidth { get; set; }
            public int BitmapHeight { get; set; }

            public double MinX { get; set; }
            public double MaxX { get; set; }
            public double MinY { get; set; }
            public double MaxY { get; set; }

            public string FileOutputPath { get; set; }
        }
    }
}
