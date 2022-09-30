using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace _26_09_2022
{
    public partial class NumericUpDown : UserControl
    {
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0));



        public Brush ButtonColor
        {
            get { return (Brush)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(Brush), typeof(NumericUpDown), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(221, 221, 221))));


        public NumericUpDown()
        {
            InitializeComponent();
        }

        private void increment_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button) Value++;
        }

        private void decrement_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button) Value--;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (sender is TextBox textBox)
                e.Handled = !(Regex.IsMatch(e.Text, @"[0-9\-]+"));
        }
    }
}
