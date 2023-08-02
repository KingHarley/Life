using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Life
{
    enum State
    {
        Alive,
        Dead
    }
    internal class Cell
    {
        public State State;
        public Rectangle Rectangle;

        public Cell(State state, Rectangle rectangle)
        {
            State = state;
            Rectangle = rectangle;
        }

        public State GetNewCellState(State[] neighbours)
        {
            if (neighbours.Count() > 4)
                throw new Exception($"Cell cannot have more than 4 neighbours. Something went wrong.");

            if (neighbours.Count() < 2)
                throw new Exception($"A cell cannot have less than 2 neighbours. Something went wrong.");

            var aliveNeighbours = neighbours.Where(s => s == State.Alive).Count();

#pragma warning disable 8524
            return State switch
            {
                State.Alive => (aliveNeighbours == 2 || aliveNeighbours == 3) ? State.Alive : State.Dead,
                State.Dead => aliveNeighbours == 3 ? State.Alive : State.Dead
            };
#pragma warning restore 8524
        }
    }
}
