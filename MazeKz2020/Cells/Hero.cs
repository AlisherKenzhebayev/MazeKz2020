﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MazeKz.Cells
{
    public class Hero : CellBase
    {
        public int Money { get; set; }

        public Hero() : base(0, 0, null) { }

        public Hero(int x, int y, Maze maze) : base(x, y, maze)
        {
        }

        public override bool TryStep()
        {
            throw new NotImplementedException("герой на может наступить сам на себя");
        }
    }
}
