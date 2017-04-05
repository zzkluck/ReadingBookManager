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
            MovmentCanon Canon = new MovmentCanon
                (AngelAlphaTextBox.Text, AngelGammaTextBox.Text, CononLengthTextBox.Text, InitalizingVelocityTextBox.Text, CononHeightTextBox.Text);
            double t = 0;
            Position initialPosition = Canon.DisplacementEquation(0);
            StringBuilder pathDescriptionSide = new StringBuilder();
            pathDescriptionSide.AppendFormat("M {0},{1} L ", initialPosition.Item1, SideViewCanvas.ActualHeight-initialPosition.Item2);

            while (true)
            {
                Position nowPosition = Canon.DisplacementEquation(t);
                if (getStatus(nowPosition) != Status.Drawing)
                    break;
                t += 0.1;
                pathDescriptionSide.AppendFormat("{0},{1} ", nowPosition.Item1, SideViewCanvas.ActualHeight - nowPosition.Item2);
            }
            Path path = new Path();
            path.Data = Geometry.Parse(pathDescriptionSide.ToString());           
            path.Style = (Style)Resources["flaringPathStyle"];
            SideViewCanvas.Children.Add(path);
        }

        private Status getStatus(Position p)
        {
            if (p.Item2 > SideViewCanvas.ActualHeight || p.Item1 > SideViewCanvas.ActualWidth || p.Item1 < 0 || p.Item2 < 0)
                return Status.CrossTheBorder;
            else
                return Status.Drawing;
        }

        private void ReFreshButton_Click(object sender, RoutedEventArgs e)
        {
            TopViewCanvas.Children.Clear();
            SideViewCanvas.Children.Clear();
        }

        enum Status
        {
            Drawing,
            Collision,
            CrossTheBorder
        }
    }
}
