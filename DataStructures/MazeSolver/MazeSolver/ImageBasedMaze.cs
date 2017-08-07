using System.Collections.Generic;
using System.IO;
using Djikstra;
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
        }

        bool IMaze.IsWall(int x, int y)
        {
            return _image[x, y].R == 0;
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
                return new ImageBasedMaze(Image.Load(stream));
            }
        }
    }
}