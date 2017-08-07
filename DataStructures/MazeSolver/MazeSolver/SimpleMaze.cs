﻿using System.Collections.Generic;
using ImageSharp;
using ImageSharp.Formats;

namespace MazeSolver
{
    public class SimpleMaze : IMaze
    {
        private readonly List<int> _mazeData;

        public static IMaze CreateSimpleMaze1()
        {
            var maze = new List<int>
            {
                1, 1, 1, 0, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 1, 0, 0, 1, 0, 1,
                1, 0, 1, 0, 1, 0, 1, 1, 0, 1,
                1, 0, 0, 0, 0, 0, 1, 0, 0, 1,
                1, 1, 1, 1, 1, 0, 1, 1, 1, 1,
                1, 0, 1, 0, 1, 0, 1, 1, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 1, 1, 1, 1, 0, 1, 1
            };
            return new SimpleMaze(maze);
        }

        public static IMaze CreateSimpleMaze2()
        {
            var maze = new List<int>
            {
                1, 1, 1, 0, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 1, 0, 0, 1, 0, 1,
                1, 0, 1, 0, 1, 0, 1, 1, 0, 1,
                1, 0, 1, 0, 0, 0, 1, 0, 0, 1,
                1, 1, 1, 1, 1, 0, 1, 1, 1, 1,
                1, 0, 1, 0, 1, 0, 1, 1, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 1, 1, 1, 1, 1, 1, 0, 1, 1
            };
            return new SimpleMaze(maze);
        }

        public static IMaze CreateSimpleMaze3()
        {
            var maze = new List<int>
            {
                1, 1, 1, 0, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 0, 0, 0, 0, 0, 1,
                1, 0, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 0, 0, 0, 1, 0, 0, 1, 0, 1,
                1, 0, 1, 0, 1, 0, 1, 1, 0, 1,
                1, 0, 1, 0, 0, 0, 1, 0, 0, 1,
                1, 1, 1, 1, 1, 0, 1, 1, 1, 1,
                1, 0, 1, 0, 1, 0, 1, 1, 0, 1,
                1, 0, 0, 0, 0, 0, 0, 1, 0, 1,
                1, 1, 1, 1, 1, 1, 1, 0, 1, 1
            };
            return new SimpleMaze(maze);
        }

        public SimpleMaze(List<int> mazeDataData)
        {
            _mazeData = mazeDataData;
        }

        private IMaze This => this;

        bool IMaze.IsWall(int x, int y)
        {
            int index = y * This.Width + x;
            return _mazeData[index] == 1;
        }

        int IMaze.Width => 10;

        int IMaze.Height => 10;

        void IMaze.SavePath(IEnumerable<Point> path, string fileName)
        {
            Image<Rgba32> image = new Image<Rgba32>(This.Width, This.Height);
            for (int i = 0; i < This.Width; i++)
            {
                for (int j = 0; j < This.Height; j++)
                {
                    image[i, j] = This.IsWall(i, j) ? Rgba32.Black : Rgba32.White;
                }
            }
            path.ForEach(point => { image[point.X, point.Y] = Rgba32.Red; });
            image.Save(fileName, new JpegEncoder {Quality = 100});
        }
    }
}