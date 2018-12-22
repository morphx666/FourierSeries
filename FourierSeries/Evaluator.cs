﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCalc;

namespace FourierSeries {
    public class Evaluator {
        public const double Infinity = 10 ^ 6;
        public const double ToRad = Math.PI / 180;

        public delegate void CustomFunctionDel(string name, FunctionArgs args);

        private string mFormula;
        private Dictionary<String, Double> mCustomParameters = new Dictionary<string, double>();
        private CustomFunctionDel mCustomFunction;

        private Expression exp;
        private Random rnd = new Random();

        public CustomFunctionDel CustomFunctionHandler {
            get { return mCustomFunction; }
            set { mCustomFunction = value; }
        }

        public string Formula {
            get { return mFormula; }
            set {
                mFormula = value;
                if(mFormula == "") mFormula = "0";
                exp = new Expression(mFormula);

                exp.EvaluateFunction += (name, args) => {
                    switch(name) {
                        case "IIf":
                            args.Result = (bool)args.Parameters[0].Evaluate() ? args.Parameters[1].Evaluate() : args.Parameters[2].Evaluate();
                            break;
                        case "ToRad":
                            args.Result = (double)args.Parameters[0].Evaluate() * ToRad;
                            break;
                        case "Abs":
                            args.Result = Math.Abs((double)args.Parameters[0].Evaluate());
                            break;
                        case "Rnd":
                            args.Result = rnd.NextDouble() - 0.5;
                            break;
                        case "Mod":
                            args.Result = (double)args.Parameters[0].Evaluate() % (double)args.Parameters[1].Evaluate();
                            break;
                    }
                };

                exp.EvaluateParameter += (name, args) => {
                    switch(name) {
                        case "Pi":
                            args.Result = Math.PI;
                            break;
                        case "e":
                            args.Result = Math.E;
                            break;
                        default:
                            if(mCustomParameters.ContainsKey(name)) args.Result = mCustomParameters[name];
                            break;
                    }
                };
            }
        }

        public Dictionary<string, object> Variables {
            get { return exp?.Parameters; }
        }

        public Dictionary<string, double> CustomParameters {
            get { return mCustomParameters; }
        }

        public double Evaluate() {
            return exp == null ? 0 : (double)exp.Evaluate();
        }

        public double Evaluate(double xValue) {
            if (exp == null) {
                return 0;
            } else {
                mCustomParameters["x"] = xValue;
                try {
                    double result = (double)exp.Evaluate();
                    return double.IsInfinity(result) ? Infinity : result;
                } catch(OverflowException) {
                    return Infinity;
                }
            }
        }
    }
}
