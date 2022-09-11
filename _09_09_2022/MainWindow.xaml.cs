using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace _09_09_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int NO_ROW = 5;
        private const int NO_COLUMN = 4;
        private int yesCurrentRow;
        private int yesCurrentColumn;

        public MainWindow()
        {
            InitializeComponent();
            CreatePositions();
        }

        private void CreatePositions()
        {
            for (int i = 0; i < Height / 50 - 1; i++)
                buttonPositionGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(50) });
            
            for (int i = 0; i < Width / 100 - 1; i++)
                buttonPositionGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(100) });

            Button yesButton = new Button() { Content = "Yes" };
            Button noButton = new Button() { Content = "No" };

            buttonPositionGrid.Children.Add(yesButton);
            buttonPositionGrid.Children.Add(noButton);

            Grid.SetColumn(yesButton, 2);
            Grid.SetRow(yesButton, 5);
            Grid.SetColumn(noButton, NO_COLUMN);
            Grid.SetRow(noButton, NO_ROW);

            yesCurrentRow = 5;
            yesCurrentColumn = 4;
        }

        private void Button_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (sender is Button button)
            {
                Random random = new Random();
                int row = 0, column = 0;
                do
                {
                    row = random.Next(buttonPositionGrid.RowDefinitions.Count);
                    column = random.Next(buttonPositionGrid.ColumnDefinitions.Count);
                } while ((row == NO_ROW && column == NO_COLUMN) || (row == yesCurrentRow && column == yesCurrentColumn));
                Grid.SetRow(button, row);
                Grid.SetColumn(button, column);

                yesCurrentColumn = column;
                yesCurrentRow = row;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                MessageBox.Show("We're glad that you decided to safe some money for our company!",
                    "Thanks for ", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            buttonPositionGrid.Children.OfType<Button>().ToList().ForEach(b =>
            {
                if (b.Content is string && b.Content == "Yes")
                    b.MouseEnter += Button_MouseEnter;
                else
                    b.Click += Button_Click;
            });
        }
    }
}
