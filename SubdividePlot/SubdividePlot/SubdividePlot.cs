using System;

namespace SubdividePlot
{
    public static class SubdividePlot
    {
        public static int Subdivide(int width, int height)
        {
            if (width < height)
            {
                // convention: width >= height
                return Subdivide(height, width);
            }
            int n = width / height;
            int remainder = width - n * height;
            if (remainder == 0)
            {
                // base case for recursive scheme
                // length of square
                return height;
            }
            if (remainder < 0)
            {
                throw new ArgumentException("width and height do not have a common divisor");
            }
            // Divide & Conquer: Simplify problem...
            return Subdivide(height, remainder);
        }

    }
}