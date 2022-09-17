using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _09_09_2022
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Button button)
            {
                Random random = new();
                Canvas.SetBottom(button, random.Next((int)button.Height, (int)button.Height * 6));
                Canvas.SetLeft(button, random.Next((int)button.Width, (int)button.Width * 6));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                MessageBox.Show("We're glad that you decided to safe some money for our company!",
                "Thanks for an answer", MessageBoxButton.OK);
                Environment.Exit(0);
            }
        }
    }
}
