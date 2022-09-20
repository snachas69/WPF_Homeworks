using System;
using System.Collections.Generic;
using System.IO;
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

namespace _12_09_2022
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string? _player;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                string? player = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\Players.txt")
                    .Where(i => i == $"{nicknameTextBox.Text}|{passwordTextBox.Password}")
                    .FirstOrDefault();
                if (player is null)
                {
                    MessageBox.Show("There's no such player signed up", "The player hasn't been found",
                       MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                _player = nicknameTextBox.Text;

                MessageBox.Show($"Welcome, {nicknameTextBox.Text}!");
                startGameButton.IsEnabled = true;
            }
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                if (nicknameTextBox.Text == string.Empty || passwordTextBox.Password == string.Empty)
                {
                    MessageBox.Show("Each field must be filled", "Empty field", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (nicknameTextBox.Text.Length <= 4 || nicknameTextBox.Text.Length >= 12)
                {
                    MessageBox.Show("There should be more than 4 and less than 12 characters in the nickname", "Wrong charcters quantity",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (passwordTextBox.Password.Length <= 4 || passwordTextBox.Password.Length >= 9)
                {
                    MessageBox.Show("There should be more than 4 and less than 9 characters in the password", "Wrong charcters quantity",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (nicknameTextBox.Text.Count(i => i < 48 || (i > 57 && i < 64) || (i > 90 && i < 97) || i > 123) > 0)
                {
                    MessageBox.Show("There should be only numbers and latin letters in the nickname", "Wrong charcters quantity",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (passwordTextBox.Password.Count(i => i < 48 || (i > 57 && i < 64) || (i > 90 && i < 97) || i > 123) > 0)
                {
                    MessageBox.Show("There should be only numbers and latin letters in the password", "Wrong charcters quantity",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!File.Exists(Directory.GetCurrentDirectory() + "\\Players.txt"))
                    File.Create(Directory.GetCurrentDirectory() + "\\Players.txt");

                File.AppendAllText(Directory.GetCurrentDirectory() + "\\Players.txt", $"{nicknameTextBox.Text}|{passwordTextBox.Password}\n");
                _player = nicknameTextBox.Text;

                MessageBox.Show($"Welcome, {nicknameTextBox.Text}!");
                startGameButton.IsEnabled = true;
            }
        }

        private void ButtonStartGame_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Hide();
                new GameWindow(_player).ShowDialog();
                Environment.Exit(0);
            }
        }
    }
}
