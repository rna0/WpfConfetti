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
using System.Windows.Threading;

namespace ConfettiWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random random = new Random();
        private DispatcherTimer timer = new DispatcherTimer();
        private const int NumConfettiPieces = 50;
        public MainWindow()
        {
            InitializeComponent();

            // Make the window background transparent
            WindowStyle = WindowStyle.None;
            AllowsTransparency = true;
            Background = Brushes.Transparent;
            WindowState = WindowState.Maximized;

            // Initialize the timer to continuously add confetti
            timer.Tick += AddConfetti;
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Start();

            // Hook up the mouse click event to exit the app
            MouseLeftButtonDown += (s, e) => Close();
        }
        private void AddConfetti(object sender, EventArgs e)
        {
            for (int i = 0; i < NumConfettiPieces; i++)
            {
                var confetti = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256))),
                    Opacity = 0.8
                };

                var transform = new TranslateTransform
                {
                    X = random.NextDouble() * ActualWidth,
                    Y = -20
                };

                confetti.RenderTransform = transform;
                MainCanvas.Children.Add(confetti);

                var animation = new DoubleAnimation
                {
                    To = ActualHeight + 20,
                    Duration = TimeSpan.FromSeconds(5)
                };

                transform.BeginAnimation(TranslateTransform.YProperty, animation);
            }
        }
    }
}