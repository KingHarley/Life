using System.Numerics;

namespace Life
{
    public partial class Life : Form
    {
        World World;
        public Life()
        {
            InitializeComponent();
            World = new World(200, 150, new Vector2(ClientRectangle.Width, ClientRectangle.Height));
        }

        private void Life_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var aliveBrush = new SolidBrush(Color.White);
            var deadBrush = new SolidBrush(Color.Black);
            graphics.FillRectangle(deadBrush, ClientRectangle);


            for (int i = World.Cells.GetLowerBound(0); i < World.Cells.GetUpperBound(0); i++)
                for (int j = World.Cells.GetLowerBound(1); j < World.Cells.GetUpperBound(1); j++)
                {
                    var cell = World.Cells[i, j];
                    if (cell.State == State.Alive)
                        graphics.FillRectangle(aliveBrush, cell.Rectangle);
                }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            World = new World(World.GetNewCells(World.Cells));
            Invalidate();
        }
    }
}