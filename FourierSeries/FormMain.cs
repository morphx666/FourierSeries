using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourierSeries {
    public partial class FormMain : Form {
        private List<Circle> cs = new List<Circle>();
        private List<float> wave = new List<float>();
        private float angle = 0;
        private int waveMaxPoints;
        private float xOffset;

        public FormMain() {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.Paint += (o, e) => {
                try {
                    RenderCircles(o, e);
                } catch { }
            };
            this.Resize += (o, e) => SetupParams();
            ButtonApply.Click += (o, e) => CreateCircles();

            CreateCircles();

            Task.Run(() => {
                while(true) {
                    Thread.Sleep(33);
                    this.Invalidate();
                }
            });
        }

        private void CreateCircles() {
            lock(cs) {
                if(int.TryParse(TextBoxTerms.Text, out int nt)) {
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

        private void SetupParams() {
            cs[0].Center = new PointF(-this.DisplayRectangle.Width / 2 + cs.Sum((c) => c.Diameter / 2) + 10, 0);
            xOffset = cs[0].Center.X + cs.Sum((c) => c.Diameter / 2) + 10;
            waveMaxPoints = this.DisplayRectangle.Width;
        }

        private void RenderCircles(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            g.TranslateTransform(this.DisplayRectangle.Width / 2, this.DisplayRectangle.Height / 2);
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
                angle += 0.1f;

                if(!float.IsNaN(lastPoint.Y)) wave.Insert(0, lastPoint.Y);
                if(wave.Count >= waveMaxPoints) wave.RemoveAt(waveMaxPoints - 1);
                for(int x = wave.Count - 1; x > 0; x--) {
                    g.DrawLine(Pens.White, x + xOffset, wave[x], x - 1 + xOffset, wave[x - 1]);
                }
                g.DrawLine(Pens.DarkGray, cs.Last().Point.X, cs.Last().Point.Y, xOffset, lastPoint.Y);
                g.FillEllipse(Brushes.Red, xOffset - 4, lastPoint.Y - 4, 8, 8);
            }
        }
    }
}
