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

        private Line buildDefaultLine(double x1,double y1,double x2,double y2)
        {
            Line newLine = new Line();
            newLine.X1 = x1;
            newLine.X2 = x2;
            newLine.Y1 = y1;
            newLine.Y2 = y2;
            newLine.Stroke = Brushes.Black;
            newLine.StrokeThickness = 2;
            return newLine;
        }
        private void addLineToCanvas(Line l,Canvas c)
        {
            if(l.Y2>c.Height||l.X2>c.Width||l.X2<0||l.Y2<0)
            {
                throw new NotImplementedException();
            }
            c.Children.Add(l);
        }
        private void FireButton_Click(object sender, RoutedEventArgs e)
        {
            MovmentCanon Canon = new MovmentCanon
                (AngelAlphaTextBox.Text, AngelGammaTextBox.Text, CononLengthTextBox.Text, InitalizingVelocityTextBox.Text, CononHeightTextBox.Text);
            int i = 1000;
            double t = 0;
            Position previous = Canon.DisplacementEquation(0);
            while(i-->0)
            {
                Position now = Canon.DisplacementEquation(t);
                t += 0.01;
                
                addLineToCanvas(buildDefaultLine(previous.Item1, previous.Item3+200, now.Item1, now.Item3+200), TopViewCanvas);
                addLineToCanvas(buildDefaultLine(previous.Item1, previous.Item2, now.Item1, now.Item2), SideViewCanvas);
                previous = now;
            }
        }
    }
}
