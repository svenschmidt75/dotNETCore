using System;
using System.Collections.Generic;
using System.IO;
using ImageSharp;
using ImageSharp.Formats;

namespace MazeSolver
{
    public class ImageBasedMaze : IMaze
    {
        private readonly Image<Rgba32> _image;

        private ImageBasedMaze(Image<Rgba32> image)
        {
            _image = image;
//            PrintMaze(image);
        }

        private void PrintMaze(Image<Rgba32> image)
        {
            for (int j = 0; j < image.Height; j++)
            {
//                int j = 0;
                for (int i = 0; i < image.Width; i++)
                {
//                    Console.WriteLine($"({i}, {j}): R: {_image[i, j].R:D3} G: {_image[i, j].G:D3} B: {_image[i, j].B:D3}");
                    Console.Write(_image[i, j].R < 100 ? "1" : "0");
                }
                Console.WriteLine();
            }
        }

        bool IMaze.IsWall(int x, int y)
        {
            return _image[x, y].R < 100;
        }

        int IMaze.Width => _image.Width;

        int IMaze.Height => _image.Height;

        void IMaze.SavePath(IEnumerable<Point> path, string fileName)
        {
            path.ForEach(point => { _image[point.X, point.Y] = Rgba32.Red; });
            _image.Save(fileName, new JpegEncoder {Quality = 100});
        }

        public static IMaze Create(string fileName)
        {
            using (var stream = File.OpenRead(fileName))
            {
                return new ImageBasedMaze(Image.Load(stream, new JpegDecoder()));
            }
        }
    }
}