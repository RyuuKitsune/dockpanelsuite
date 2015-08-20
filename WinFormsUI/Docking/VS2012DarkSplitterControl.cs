using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    class VS2012DarkSplitterControl : DockPane.SplitterControlBase
    {
        private static readonly SolidBrush _horizontalBrush = new SolidBrush(Color.FromArgb(0xFF, 68, 68, 68));
        private static readonly Color[] _verticalSurroundColors = new[] { Color.FromArgb(0x22, 0x22, 0x22) };


        public VS2012DarkSplitterControl(DockPane pane)
            : base(pane)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;

            if (rect.Width <= 0 || rect.Height <= 0)
                return;

            switch (Alignment)
            {
                case DockAlignment.Right:
                case DockAlignment.Left:
                    {
                        using (var path = new GraphicsPath())
                        {
                            path.AddRectangle(rect);
                            using (var brush = new PathGradientBrush(path)
                            {
                                CenterColor = Color.FromArgb(0xFF, 68, 68, 68),
                                SurroundColors = _verticalSurroundColors
                            })
                            {
                                e.Graphics.FillRectangle(brush, rect.X + Measures.SplitterSize / 2 - 1, rect.Y,
                                                         Measures.SplitterSize / 3, rect.Height);
                            }
                        }
                    }
                    break;
                case DockAlignment.Bottom:
                case DockAlignment.Top:
                    {
                        e.Graphics.FillRectangle(_horizontalBrush, rect.X, rect.Y,
                                        rect.Width, Measures.SplitterSize);
                    }
                    break;
            }
        }
    }
}
