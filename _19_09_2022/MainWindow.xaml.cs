using System;
using System.Windows;
using System.Windows.Controls;

namespace _19_09_2022
{
    public partial class MainWindow : Window
    {
        private Button[,] buttons = new Button[4, 4];
        private Point emptinessCordinates = new() { X = 3, Y = 3 };
        private Random random = new();
        public MainWindow()
        {
            InitializeComponent();
            SetGrid();
            ShuffleGrid();
        }

        private void SetGrid()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (buttons.GetLength(0) * i + j == 15)
                        break;
                    
                    buttons[i, j] = new Button()
                    {
                        Content = (buttons.GetLength(0) * i + j) + 1,
                        Style = (Style)Resources["NumericButton"]
                    };
                    field.Children.Add(buttons[i, j]);
                    Grid.SetColumn(buttons[i, j], j);
                    Grid.SetRow(buttons[i, j], i);
                }
            }
            
            buttons[3, 3] = new Button() { Style = (Style)Resources["TotalTransperecy"] };
            field.Children.Add(buttons[3,3]);
            Grid.SetColumn(buttons[3,3], 3);
            Grid.SetRow(buttons[3,3], 3);
        }

        private void ShuffleGrid()
        {
            for (int i = 0; i < 1000; i++)
            {
                int x = 0, y = 0;
                
                do
                {
                    x = random.Next(100) % 3; if (x == 2) x = -1;
                    y = random.Next(100) % 3; if (y == 2) y = -1; 
                } while ((int)emptinessCordinates.X + x > 3 || (int)emptinessCordinates.X + x < 0
                       || (int)emptinessCordinates.Y + y > 3 || (int)emptinessCordinates.Y + y < 0);
                
                SwapWithEmptinessIfPossiable(buttons[(int)emptinessCordinates.X + x, (int)emptinessCordinates.Y + y]);
            }
        }

        private void SwapWithEmptinessIfPossiable(Button button)
        {
            int x = Grid.GetColumn(button);
            int y = Grid.GetRow(button);

            if (Math.Abs(x - emptinessCordinates.X) == 1 && y == emptinessCordinates.Y
                || Math.Abs(y - emptinessCordinates.Y) == 1 && x == emptinessCordinates.X)
            {
                Button temp = buttons[x, y];
                buttons[x, y] = buttons[(int)emptinessCordinates.X, (int)emptinessCordinates.Y];
                buttons[(int)emptinessCordinates.X, (int)(emptinessCordinates.Y)] = temp;

                Grid.SetColumn(button, (int)emptinessCordinates.X);
                Grid.SetRow(button, (int)emptinessCordinates.Y);

                Grid.SetColumn(buttons[x, y], x);
                Grid.SetRow(buttons[x, y], y);

                emptinessCordinates.X = x;
                emptinessCordinates.Y = y;
            }
        }

        private void Move(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                SwapWithEmptinessIfPossiable(button);
                if (IsAssabled())
                {
                    MessageBox.Show("Puzzle is assambled!");
                    Environment.Exit(0);
                }

            }
        }
        private bool IsAssabled()
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    if (buttons[i, j].Content is null)
                        continue;
                    if (buttons[i, j].Content.ToString() != ((buttons.GetLength(0) * i + j) + 1).ToString())
                        return false;
                }
          
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < buttons.GetLength(0); i++)
                for (int j = 0; j < buttons.GetLength(1); j++)
                    if (buttons[i, j].Content is not null)
                        buttons[i, j].Click += Move;
        }
    }
}