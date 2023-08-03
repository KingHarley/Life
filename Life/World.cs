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

        public Cell[,] GetNewCells(Cell[,] oldCells)
        {
            var minX = oldCells.GetLowerBound(0);
            var maxX = oldCells.GetUpperBound(0);
            var minY = oldCells.GetLowerBound(1);
            var maxY = oldCells.GetUpperBound(1);

            var newCells = new Cell[oldCells.GetLength(0), oldCells.GetLength(1)];

            for (int i = minX; i <= maxX; i++)
                for (int j = minY; j <= maxY; j++)
                {
                    var neighbours = GetNeighbours(new Vector2(i, j), oldCells);
                    newCells[i, j] = oldCells[i, j].GetNewCellState(neighbours);
                }
            return newCells;
        }

        Cell[] GetNeighbours(Vector2 index, Cell[,] cells)
        {
            var lowerBounds = new Vector2(cells.GetLowerBound(0), cells.GetLowerBound(1));
            var upperBounds = new Vector2(cells.GetUpperBound(0), cells.GetUpperBound(1));

            var x = (int)index.X;
            var y = (int)index.Y;

            var indices = new int[] { x-1, x, x+1 }
            .SelectMany(i => new Vector2[]
            {
                new Vector2(i, y-1),
                new Vector2(i, y),
                new Vector2(i, y+1)
            })
            .Except(new Vector2[] { index })
            .Where(v => IsValidNeighbourIndices(v, lowerBounds, upperBounds));
            var neighbours = indices.Select(v => cells[(int)v.X, (int)v.Y]).ToArray();
            if (neighbours.Count() < 3)
                throw new Exception($"Cell at x: {x} and y: {y} has less than 3 neighbours");
            if (neighbours.Count() > 8)
                throw new Exception($"Cell at x: {x} and y: {y} has more than 8 neighbours");

            return neighbours;
        }

        bool IsValidNeighbourIndices(Vector2 indices, Vector2 lowerBounds, Vector2 upperBounds)
        {
            if (indices.X < lowerBounds.X || indices.X > upperBounds.X || indices.Y < lowerBounds.Y || indices.Y > upperBounds.Y)
                return false;
            return true;
        }
    }
}
