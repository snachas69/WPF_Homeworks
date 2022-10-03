using System.Windows;

namespace _30_09_2022
{
    public partial class Prompt : Window
    {
        public string? Name { get; set; }

        public Prompt()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Name = name.Text;
            DialogResult = true;
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
