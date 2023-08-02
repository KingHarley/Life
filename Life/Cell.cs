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
    }
}
