using System;
using System.Collections.Generic;
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
        private float xOffset;
        private float angleStep;

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
                try {
                    RenderCircles(o, e);
                } catch { }
            };
            this.Resize += (o, e) => SetupParams();

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

        private void SetupParams() {
            if(!CheckBoxAutoAlign.Checked) {
                cs[0].Center = new PointF(-this.DisplayRectangle.Width / 2 + cs[0].Diameter / 2 + 10, 0);
            } else {
                cs[0].Center = new PointF(-this.DisplayRectangle.Width / 2 + cs.Sum((c) => c.Diameter / 2) + 10, 0);
            }
            xOffset = cs[0].Center.X + cs.Sum((c) => c.Diameter / 2) + 10;
            waveMaxPoints = this.DisplayRectangle.Width;
            if(!float.TryParse(TextBoxAngleStep.Text, out angleStep)) angleStep = 0.1f;
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
                angle += angleStep;

                if(!float.IsNaN(lastPoint.Y)) wave.Insert(0, lastPoint.Y);
                if(wave.Count >= waveMaxPoints) wave.RemoveAt(waveMaxPoints - 1);
                for(int x = wave.Count - 1; x > 0; x--) {
                    g.DrawLine(Pens.White, x + xOffset, wave[x], x - 1 + xOffset, wave[x - 1]);
                }
                g.DrawLine(Pens.DimGray, cs.Last().Point.X, cs.Last().Point.Y, xOffset, lastPoint.Y);
                g.FillEllipse(Brushes.Red, xOffset - 4, lastPoint.Y - 4, 8, 8);
            }
        }
    }
}
