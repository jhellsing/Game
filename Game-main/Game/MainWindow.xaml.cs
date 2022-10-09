using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Game
{
    public partial class MainWindow : Window
    {
        Ellipse Player;
        Ellipse bonus;
        Rectangle stena;
        const int Size = 50;
        static int ochki;
        static int point;   
        public MainWindow()
        {
            InitializeComponent();
            DrawGameArea();
        }

        private void DrawGameArea() //рисует игровое поле
        {
            ochki = 0;
            point = 0;
            bool doneDrawingBackground = false;
            int nextX = 0, nextY = 0;
            int rowCounter = 0;
            Random random = new Random();
            if(Player == null)
                Player = Object;
            else
            {
                Pole.Children.Add(Player);
                Canvas.SetTop(Player, 10);
                Canvas.SetLeft(Player, 10);
            }
            while (doneDrawingBackground == false)
            {
                int rand0 = random.Next(1, 10); 
                int rand1 = random.Next(1, 11); 
                Rectangle rect = new Rectangle
                {
                    Width = Size,
                    Height = Size,
                    Stroke = Brushes.Gray
                };
                bonus = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Green
                };
                stena = new Rectangle
                {
                    Width = Size,
                    Height = Size,
                    Fill = Brushes.Red
                };
                Pole.Children.Add(rect);
                Canvas.SetTop(rect, nextY);
                Canvas.SetLeft(rect, nextX);
                nextX += Size;
                if (nextX >= 750)
                {
                    nextX = 0;
                    nextY += Size;
                    rowCounter++;
                }
                if (nextY >= 550)
                    doneDrawingBackground = true;
                if (rand0 > 7)
                {
                    Pole.Children.Add(stena);
                    Canvas.SetTop(stena, nextY);
                    Canvas.SetLeft(stena, nextX);
                }
                if (rand1 > 8)
                {
                    if (Canvas.GetTop(stena) != nextY && Canvas.GetLeft(stena) != nextX)
                    {
                        Pole.Children.Add(bonus);
                        Canvas.SetTop(bonus, nextY + 15);
                        Canvas.SetLeft(bonus, nextX + 15);
                        ochki++;
                    }
                }
                ScoreLabel.Content = $"Счет 0 из {ochki}";
            }
        }
        private void Restart() //рестарт (что ещё сказать, как будто сразу было не понятно)
        {
            Pole.Children.Clear();
            DrawGameArea();
            ScoreLabel.HorizontalAlignment = HorizontalAlignment.Left;
            ScoreLabel.VerticalAlignment = VerticalAlignment.Top;
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) //передвижение
        {
            if (Canvas.GetTop(Object) > 10)
            {
                if (e.Key == Key.Up)
                {
                    Canvas.SetTop(Object, Canvas.GetTop(Object) - 50);
                }
            }
            if (Canvas.GetTop(Object) < 510)
            {
                if (e.Key == Key.Down)
                {
                    Canvas.SetTop(Object, Canvas.GetTop(Object) + 50);
                }
            }
            if (Canvas.GetLeft(Object) < 710)
            {
                if (e.Key == Key.Right)
                {
                    Canvas.SetLeft(Object, Canvas.GetLeft(Object) + 50);
                }
            }
            if (Canvas.GetLeft(Object) > 10)
            {
                if (e.Key == Key.Left)
                {
                    Canvas.SetLeft(Object, Canvas.GetLeft(Object) - 50);
                }
            }
            if (e.Key == Key.R)
            {
                Restart();
            }
        }
        private void Window_PreviewKeyUp(object sender, KeyEventArgs e) 
        {
            for (int i = 1; i < Pole.Children.Count; i++)
            {
                UIElement child = Pole.Children[i];
                int bon = 5;
                int blok = 10;
                if (Canvas.GetLeft(Object) == Canvas.GetLeft(child) - bon && Canvas.GetTop(Object) == Canvas.GetTop(child) - bon && (child as Ellipse).Fill == Brushes.Green) //получение бонуса
                {
                    Pole.Children.Remove(child);
                    point++;
                }
                ScoreLabel.Content = $"Счет {point} из {ochki}";
                if (Canvas.GetLeft(Object) == Canvas.GetLeft(child) + blok && Canvas.GetTop(Object) == Canvas.GetTop(child) + blok && (child as Rectangle).Fill == Brushes.Red) //столкновение со стеной
                {
                    MessageBox.Show("ты ботик");
                    Restart();
                }
                if (point == ochki) //победа
                {
                    ScoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
                    ScoreLabel.VerticalAlignment = VerticalAlignment.Center;
                    MessageBox.Show("люти про");
                    Restart();
                }
            }
        }
    }
}
