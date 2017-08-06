using System.IO;
using ImageSharp;

namespace MazeSolver
{
    public class ImageBasedMaze : IMaze
    {
        private readonly Image<Rgba32> _image;

        public IMaze Create(string fileName)
        {
            using (FileStream stream = File.OpenRead(fileName))
            {
                return new ImageBasedMaze(Image.Load(stream));
            }
        }

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
    }
}