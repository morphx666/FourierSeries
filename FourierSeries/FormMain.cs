using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourierSeries {
    public partial class FormMain : Form {
        private readonly List<Circle> cs = new List<Circle>();
        private readonly List<float> wave = new List<float>();
        private float angle = 0;
        private int waveMaxPoints;
        private float angleStep;
        private Region clipRegion;
        private SolidBrush crBrush = new SolidBrush(Color.FromArgb(44, 44, 44));
        private int xOffset;

        private int ps = 4;
        private int ps2;

        private AutoResetEvent ev = new AutoResetEvent(false);

        public FormMain() {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint, true);

            foreach(Control ctrl in this.Controls) {
                if(ctrl is TextBox) {
                    ((TextBox)ctrl).KeyDown += (object sender, KeyEventArgs e) => { if(e.KeyCode == Keys.Enter) CreateCircles(); };
                } else if(ctrl is CheckBox) {
                    ((CheckBox)ctrl).Click += (o, e) => CreateCircles();
                }
            }

            this.Paint += (o, e) => {
                try { // Just in case...
                    RenderCircles(o, e);
                } catch { }
            };
            this.Resize += (o, e) => SetupParams();

            CreateCircles();

            Task.Run(() => {
                while(true) {
                    ev.WaitOne(15);
                    this.Invalidate();
                }
            });
        }

        private void CreateCircles() {
            lock(cs) {
                if(uint.TryParse(TextBoxTerms.Text, out uint nt)) {
                    cs.Clear();

                    for(int i = 0; i < nt; i++) {
                        try {
                            cs.Add(new Circle(new PointF(0, 0),
                                                float.Parse(TextBoxDiameter.Text),
                                                TextBoxMultiplier.Text,
                                                TextBoxFactor.Text,
                                                i));
                        } catch(Exception ex) {
                            MessageBox.Show(ex.Message, "Invalid Parameters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if(i > 0) {
                            cs[i].Center = cs[i - 1].Point;
                            cs[i].Step(0);
                        }
                    }
                    SetupParams();
                } else {
                    TextBoxTerms.Text = "7";
                }
            }
        }

        private RectangleF SetCirclesArea() {
            float xmin = float.MaxValue;
            float xmax = float.MinValue;
            float ymin = float.MaxValue;
            float ymax = float.MinValue;
            float PI2 = (float)(2.0 * Math.PI);
            int n = cs.Count;
            float r = cs[n - 1].Diameter / 2;

            cs.ForEach((c) => c.Step(0));
            // FIXME: Try to run determine the period of the series
            for(float a = 0; a < 100 * n * PI2; a += 0.2f) { // This fails for very long periods
                PointF lastPoint = PointF.Empty;

                foreach(Circle c in cs) {
                    c.Center = lastPoint;
                    c.Step(a);
                    lastPoint = c.Point;

                    xmin = Math.Min(xmin, c.Center.X - c.Diameter / 2);
                    xmax = Math.Max(xmax, c.Center.X + c.Diameter / 2);
                    ymin = Math.Min(ymin, c.Center.Y - c.Diameter / 2);
                    ymax = Math.Max(ymax, c.Center.Y + c.Diameter / 2);
                }
            }
            cs.ForEach((c) => c.Step(0));

            return new RectangleF(xmin, ymin, xmax, ymax);
        }

        private void SetupParams() {
            ps2 = 2 * ps;
            int ps22 = ps2 + 2;

            RectangleF r = new RectangleF(ps22,
                                    TextBoxFactor.Bottom + ps22,
                                    this.DisplayRectangle.Width - ps22 * 2,
                                    this.DisplayRectangle.Height - TextBoxFactor.Bottom - ps22 * 2);
            clipRegion = new Region(r);

            RectangleF circlesArea = SetCirclesArea();
            cs[0].Center = new PointF(-r.Width / 2 - circlesArea.X + ps22, 0);

            xOffset = (int)(cs[0].Center.X + circlesArea.Width) + ps22 * 4;
            waveMaxPoints = this.DisplayRectangle.Width;
            if(!float.TryParse(TextBoxAngleStep.Text, out angleStep)) angleStep = 0.1f;
        }

        private void RenderCircles(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.Clip = clipRegion;
            g.FillRectangle(crBrush, clipRegion.GetBounds(g));
            g.TranslateTransform(this.DisplayRectangle.Width / 2,
                                 TextBoxFactor.Bottom + (this.DisplayRectangle.Height - TextBoxFactor.Bottom) / 2);
            g.ScaleTransform(1, -1);

            if(cs.Count == 0) return;

            lock(cs) {
                PointF lastPoint = cs[0].Center;

                foreach(Circle c in cs) {
                    c.Center = lastPoint;

                    c.Step(angle);
                    c.Render(g);

                    lastPoint = c.Point;
                }
                angle += angleStep;

                if(!float.IsNaN(lastPoint.Y)) wave.Insert(0, lastPoint.Y);
                if(wave.Count >= waveMaxPoints) wave.RemoveAt(waveMaxPoints - 1);
                for(int x = wave.Count - 1; x > 0; x--) {
                    g.DrawLine(Pens.White, x + xOffset, wave[x], x - 1 + xOffset, wave[x - 1]);
                }
                g.DrawLine(Pens.DimGray, cs.Last().Point.X, cs.Last().Point.Y, xOffset, lastPoint.Y);
                g.FillEllipse(Brushes.Red, xOffset - ps, lastPoint.Y - ps, ps2, ps2);
            }
        }
    }
}