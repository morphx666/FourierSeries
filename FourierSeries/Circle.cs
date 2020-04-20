using System;
using System.Drawing;

namespace FourierSeries {
    public class Circle {
        private readonly float r;
        private PointF point;
        private readonly float pointDiameter;

        public string Factor { get; }
        public float Multiplier { get; }
        public float Diameter { get; }
        public PointF Center { get; set; }

        public PointF Point {
            get { return point; }
        }

        public Circle(PointF center, float diameter, string multiplier, string factor, int index) {
            Center = center;
            Diameter = diameter;
            Factor = factor;

            pointDiameter = 8;

            try {
                Evaluator evMultiplier = new Evaluator { Formula = multiplier };
                evMultiplier.CustomParameters.Add("i", index);
                Multiplier = (float)evMultiplier.Evaluate();
            } catch(Exception ex) {
                throw new ArgumentException(ex.Message, nameof(multiplier));
            }

            try {
                Evaluator evFactor = new Evaluator { Formula = factor };
                evFactor.CustomParameters.Add("Diameter", Diameter);
                evFactor.CustomParameters.Add("Multiplier", Multiplier);
                evFactor.CustomParameters.Add("i", index);

                r = (float)Math.Abs(evFactor.Evaluate());
            } catch(Exception ex) {
                throw new ArgumentException(ex.Message, nameof(factor));
            }

            //r = 4 * (Diameter / 2) / (Multiplier * (float)Math.PI);
            Diameter = 2 * r;
            Step(0);
        }

        public void Step(float angle) {
            point.X = Center.X + r * (float)Math.Cos(Multiplier * angle);
            point.Y = Center.Y + r * (float)Math.Sin(Multiplier * angle);
        }

        public void Render(Graphics g) {
            g.DrawEllipse(Pens.White, Center.X - r, Center.Y - r, Diameter, Diameter);
            g.FillEllipse(Brushes.Red, point.X - pointDiameter / 2, point.Y - pointDiameter / 2, pointDiameter, pointDiameter);
            g.DrawLine(Pens.DimGray, Center, point);
        }
    }
}