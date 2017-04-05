using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using ReadingBookManager.Physics;

namespace ReadingBookManager
{
    /// <summary>
    /// PhysicsSimulation.xaml 的交互逻辑
    /// </summary>
    public partial class PhysicsSimulation : Page
    {
        public PhysicsSimulation()
        {
            InitializeComponent();
        }
        private void FireButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO: 学习线程相关的知识，制作绘制线的动画
            try
            {
                MovmentCanon Canon = new MovmentCanon
                (AngelAlphaTextBox.Text, AngelGammaTextBox.Text, CononLengthTextBox.Text, InitalizingVelocityTextBox.Text, CononHeightTextBox.Text);
                DrawConon(Canon);
                double t = 0;
                Position initialPosition = Canon.DisplacementEquation(0);

                StringBuilder pathDescriptionTop = new StringBuilder();
                pathDescriptionTop.AppendFormat("M {0},{1} L ", initialPosition.Item1, CanvasYConvert(initialPosition.Item3 + SideViewCanvas.ActualHeight / 2, SideViewCanvas));
                StringBuilder pathDescriptionSide = new StringBuilder();
                pathDescriptionSide.AppendFormat("M {0},{1} L ", initialPosition.Item1, CanvasYConvert(initialPosition.Item2, SideViewCanvas));
                
                Status TopDrawStatus = Status.Drawing, SideDrawStatus = Status.Drawing;
                while (TopDrawStatus == Status.Drawing || SideDrawStatus == Status.Drawing)
                {
                    t += 0.1;
                    Position nowPosition = Canon.DisplacementEquation(t);
                    if ((TopDrawStatus = getStatus(nowPosition, TopOrSide.Top)) == Status.Drawing)
                        pathDescriptionTop.AppendFormat("{0},{1} ", nowPosition.Item1, CanvasYConvert(nowPosition.Item3 + SideViewCanvas.ActualHeight / 2, TopViewCanvas));
                    if ((SideDrawStatus = getStatus(nowPosition, TopOrSide.Side)) == Status.Drawing)
                        pathDescriptionSide.AppendFormat("{0},{1} ", nowPosition.Item1, CanvasYConvert(nowPosition.Item2,SideViewCanvas));
                }
                Path path = new Path();
                path.Data = Geometry.Parse(pathDescriptionTop.ToString());
                path.Style = (Style)Resources["flaringPathStyle"];
                TopViewCanvas.Children.Add(path);
                Path path2 = new Path();
                path2.Data = Geometry.Parse(pathDescriptionSide.ToString());
                path2.Style = (Style)Resources["flaringPathStyle"];
                SideViewCanvas.Children.Add(path2);
            }
            catch (ArgumentOutOfRangeException excp)
            {
                MessageBox.Show(excp.Message);
            }

        }

        private void ReFreshButton_Click(object sender, RoutedEventArgs e)
        {
            TopViewCanvas.Children.Clear();
            SideViewCanvas.Children.Clear();
        }
        private void DrawConon(MovmentCanon c)
        {
            Path cononTop = new Path();
            cononTop.Stroke = Brushes.Brown;
            cononTop.StrokeThickness = 5;
            cononTop.Data = Geometry.Parse
                (string.Format("M {0},{1} L{2},{3}", 0, TopViewCanvas.ActualHeight / 2, c.CononLength * c.cosX, CanvasYConvert(c.CononLength * c.cosZ + TopViewCanvas.ActualHeight / 2, TopViewCanvas)));
            Path cononSide = new Path();
            cononSide.Stroke = Brushes.Brown;
            cononSide.StrokeThickness = 5;
            cononSide.Data = Geometry.Parse
                (string.Format("M {0},{1} L{2},{3}", 0, CanvasYConvert(c.InitalizingHeight, SideViewCanvas), c.CononLength * c.cosX, CanvasYConvert(c.CononLength * c.cosY + c.InitalizingHeight, TopViewCanvas)));
            TopViewCanvas.Children.Add(cononTop);
            SideViewCanvas.Children.Add(cononSide);
        }
        private double CanvasYConvert(double YValue, Canvas c)
        {
            return c.ActualHeight - YValue;
        }
        private Status getStatus(Position p, TopOrSide tos)
        {
            switch (tos)
            {
                case TopOrSide.Top:
                    if (p.Item3 > TopViewCanvas.ActualHeight/2 || p.Item1 > TopViewCanvas.ActualWidth || p.Item1 < 0 || p.Item3 < -TopViewCanvas.ActualHeight / 2)
                        return Status.CrossTheBorder;
                    else
                        return Status.Drawing;
                case TopOrSide.Side:
                    if (p.Item2 > SideViewCanvas.ActualHeight || p.Item1 > SideViewCanvas.ActualWidth || p.Item1 < 0 || p.Item2 < 0)
                        return Status.CrossTheBorder;
                    else
                        return Status.Drawing;
                default:
                    throw new ArgumentException();
            }

        }
        enum Status
        {
            Drawing,
            Collision,
            CrossTheBorder
        }
        enum TopOrSide
        {
            Top, Side
        }
    }
}
