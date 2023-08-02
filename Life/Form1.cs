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

            for (int i = World.Cells.GetLowerBound(0); i < World.Cells.GetUpperBound(0); i++)
                for (int j = World.Cells.GetLowerBound(1); j < World.Cells.GetUpperBound(1); j++)
                {
                    var cell = World.Cells[i, j];
                    graphics.FillRectangle(cell.State == State.Alive ? aliveBrush : deadBrush, World.Cells[i, j].Rectangle);
                }
        }
    }
}