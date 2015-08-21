using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace WeifenLuo.WinFormsUI.Docking
{
    [ToolboxItem(false)]
    class VS2012DarkDockWindow : DockWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VS2012DarkDockWindow"/> class.
        /// </summary>
        /// <param name="dockPanel">The dock panel.</param>
        /// <param name="dockState">State of the dock.</param>
        public VS2012DarkDockWindow(DockPanel dockPanel, DockState dockState) : base(dockPanel, dockState)
        {
        }

        public override Rectangle DisplayingRectangle
        {
            get
            {
                Rectangle rect = ClientRectangle;
                if (DockState == DockState.DockLeft)
                    rect.Width -= Measures.SplitterSize;
                else if (DockState == DockState.DockRight)
                {
                    rect.X += Measures.SplitterSize;
                    rect.Width -= Measures.SplitterSize;
                }
                else if (DockState == DockState.DockTop)
                    rect.Height -= Measures.SplitterSize;
                else if (DockState == DockState.DockBottom)
                {
                    rect.Y += Measures.SplitterSize;
                    rect.Height -= Measures.SplitterSize;
                }

                return rect;
            }
        }

        internal class VS2012DarkDockWindowSplitterControl : SplitterBase
        {
            private static readonly SolidBrush _horizontalBrush = new SolidBrush(Color.FromArgb(0xFF, 0x44, 0x44, 0x44));
            private static readonly Color[] _verticalSurroundColors = new[] { Color.FromArgb(0x22,0x22,0x22) };

            protected override int SplitterSize
            {
                get { return Measures.SplitterSize; }
            }

            protected override void StartDrag()
            {
                DockWindow window = Parent as DockWindow;
                if (window == null)
                    return;

                window.DockPanel.BeginDrag(window, window.RectangleToScreen(Bounds));
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                Rectangle rect = ClientRectangle;

                if (rect.Width <= 0 || rect.Height <= 0)
                    return;

                switch (Dock)
                {
                    case DockStyle.Right:
                    case DockStyle.Left:
                        {
                            using (var path = new GraphicsPath())
                            {
                                path.AddRectangle(rect);
                                using (var brush = new PathGradientBrush(path)
                                {
                                    CenterColor = Color.FromArgb(0xFF, 0x22, 0x22, 0x22),
                                    SurroundColors = _verticalSurroundColors
                                })
                                {
                                    e.Graphics.FillRectangle(brush, rect.X + Measures.SplitterSize / 2 - 1, rect.Y,
                                                             Measures.SplitterSize / 3, rect.Height);
                                }
                            }
                        }
                        break;
                    case DockStyle.Bottom:
                    case DockStyle.Top:
                        {
                            e.Graphics.FillRectangle(_horizontalBrush, rect.X, rect.Y,
                                                     rect.Width, Measures.SplitterSize);
                        }
                        break;
                }

            }
        }
    }
}
