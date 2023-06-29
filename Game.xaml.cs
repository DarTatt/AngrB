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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AngryBirds
{

    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        //public RoutedPropertyChangedEventArgs<double> Value { get; private set; }
        private double sp;
        private double a;
        int gravity = 5;
        public Window1()
        {
            InitializeComponent();
            //var bird = new AB();
        }
        
        void SliderSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
             sp = speed.Value;
            //textBox1.Text = speed.Value.ToString();
            // return sp;
        }
        void SliderAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            a = angle.Value;
        }
        void Button_Click(object sender, RoutedEventArgs e)
            {
            //Console.Write(SliderSpeed_ValueChanged(speed,));
             
            /*var bird = new AB();
            bird.Notify += bird.message;
            bird.interval = 0.2;
            bird.Vo = Convert.ToDouble(sp);
            bird.angle = (Math.PI * Convert.ToDouble(a)) / 180;
            bird.M = 2;
            bird.wall_x = 15;
            bird.wall_y = 10;
            bird.coefficient = 0.1;
            bird.flight();*/


            NameScope.SetNameScope(this, new NameScope());


            Image aRectangle = new Image();
            aRectangle.Width = 50;
            aRectangle.Height = 50;
            aRectangle.Source = new BitmapImage(new Uri("C:\\Users\\Daria\\source\\repos\\AngryBirds\\птица.png"));


            TranslateTransform animatedTranslateTransform =
                new TranslateTransform();


            this.RegisterName("AnimatedTranslateTransform", animatedTranslateTransform);

            aRectangle.RenderTransform = animatedTranslateTransform;


            Canvas mainPanel = new Canvas();
            mainPanel.Width = 400;
            mainPanel.Height = 400;
            mainPanel.Children.Add(aRectangle);
            this.Content = mainPanel;


            var bird = new AB();
            bird.Notify += bird.message;
            bird.interval = 0.2;
            bird.Vo = Convert.ToDouble(sp);
            bird.angle = (Math.PI * Convert.ToDouble(a)) / 180;
            bird.M = 2;
            bird.wall_x = 15;
            bird.wall_y = 10;
            bird.coefficient = 0.1;
            var points = bird.flight();


            PathGeometry animationPath = new PathGeometry();
            PathFigure pFigure = new PathFigure();

            pFigure.StartPoint = new Point(10, 300);
            PolyBezierSegment pBezierSegment = new PolyBezierSegment();
            foreach (var point in points)
                pBezierSegment.Points.Add(point);

            //pBezierSegment.Points.Add(new Point(35, 0));
            //pBezierSegment.Points.Add(new Point(135, 0));
            //pBezierSegment.Points.Add(new Point(160, 100));
            //pBezierSegment.Points.Add(new Point(180, 190));
            //pBezierSegment.Points.Add(new Point(285, 200));
            //pBezierSegment.Points.Add(new Point(310, 100));
            pFigure.Segments.Add(pBezierSegment);
            animationPath.Figures.Add(pFigure);


            animationPath.Freeze();


            DoubleAnimationUsingPath translateXAnimation =
                new DoubleAnimationUsingPath();
            translateXAnimation.PathGeometry = animationPath;
            translateXAnimation.Duration = TimeSpan.FromSeconds(5);


            translateXAnimation.Source = PathAnimationSource.X;


            Storyboard.SetTargetName(translateXAnimation, "AnimatedTranslateTransform");
            Storyboard.SetTargetProperty(translateXAnimation,
                new PropertyPath(TranslateTransform.XProperty));


            DoubleAnimationUsingPath translateYAnimation =
                new DoubleAnimationUsingPath();
            translateYAnimation.PathGeometry = animationPath;
            translateYAnimation.Duration = TimeSpan.FromSeconds(5);

            translateYAnimation.Source = PathAnimationSource.Y;


            Storyboard.SetTargetName(translateYAnimation, "AnimatedTranslateTransform");
            Storyboard.SetTargetProperty(translateYAnimation,
                new PropertyPath(TranslateTransform.YProperty));

            Storyboard pathAnimationStoryboard = new Storyboard();
            pathAnimationStoryboard.RepeatBehavior = RepeatBehavior.Forever;
            pathAnimationStoryboard.Children.Add(translateXAnimation);
            pathAnimationStoryboard.Children.Add(translateYAnimation);
            pathAnimationStoryboard.Begin(this);

        }

    }


}
internal class AB
{
    public double interval;// шаг
    public double Vo;//начальная скорость
    public double angle;//угол
    public double M;//масса
                    //препятствие
    public double wall_x;//расстояние до препятствия
    public double wall_y;//высота препятствия
                         //public double wall_y2;//нижняя граница препятствия
                         //
    public double g = 9.8;
    public double coefficient;// коэффициент сопротивления воздуха
                              // event
    public delegate void AccountHandler(string message);
    public event AccountHandler Notify;

    public void message(string events)
    {
        Console.WriteLine(events);
    }

    public List<Point> flight()
    {
        double X = 0;//пройденный путь
        double time = 0;//время в определенный период полета
        double Vx = Vo * Math.Cos(angle);
        double Vy = Vo * Math.Sin(angle);
        double Y = 0;
        var result = new List<Point>();
        while (Y >= 0)
        {
            time += interval;
            Vx = Vx - interval * (coefficient * Vx / M);
            Vy = Vy - interval * (g + (coefficient * Vx / M));
            //X = X + interval * Vx;
            Y = Y + interval * Vy;
            if ((Y < wall_y) && (X < wall_x) && (wall_x < (X + interval * Vx)))
            {
                Notify?.Invoke($"Птичка врезалась в стену!");
                break;
            }
            X = X + interval * Vx;
            if (Y >= 0)
            {
                result.Add(new Point(X * 100, Y * 100));
                Console.Write($"X: {Math.Round(X, 4)} м  Y: {Math.Round(Y, 4)} м  t: {Math.Round(time, 4)} с \n");
            }
            else
            {
                Notify?.Invoke($"Птичка упала на землю!");
            }

        }

        return result;
    }
}