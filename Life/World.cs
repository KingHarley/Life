using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    internal class World
    {
        public Cell[,] Cells;

        public World(int cellsX, int cellsY, Vector2 worldSize)
        {
            Cells = new Cell[cellsX, cellsY];

            var rnd = new Random();
            for(int i = Cells.GetLowerBound(0); i < Cells.GetUpperBound(0) + 1; i++)
                for(int j = Cells.GetLowerBound(1); j < Cells.GetUpperBound(1) + 1; j++)
                {
                    var rect = GetCellRectangle(i, j, GetCellSize(cellsX, cellsY, worldSize));
                    Cells[i, j] = new Cell(rnd.Next(10) == 0 ? State.Alive : State.Dead, rect);
                }
        }

        public World(Cell[,] newCells)
        {
            Cells = newCells;
        }

        Vector2 GetCellSize(int cellsX, int cellsY, Vector2 worldSize)
        {
            var x = worldSize.X / cellsX;
            var y = worldSize.Y / cellsY;
            return new Vector2(x, y);
        }

        Rectangle GetCellRectangle(int cellX, int cellY, Vector2 cellSize)
        {
            var xPos = cellSize.X * (cellX - 0.5);
            var yPos = cellSize.Y * (cellY - 0.5);
            return new Rectangle((int)xPos, (int)yPos, (int)cellSize.X, (int)cellSize.Y);
        }
    }
}
