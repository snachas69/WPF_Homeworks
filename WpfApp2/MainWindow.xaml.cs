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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CreateInterface();
        }

        private void CreateInterface()
        {
            for (int i = 0; i < 6; i++)
                (Content as Grid)?.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            for (int i = 0; i < 8; i++)
                (Content as Grid)?.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });

            Button[] buttons = new Button[22];
            for (int i = 0; i < 22; i++)
            {
                buttons[i] = new Button() { Content = i + 1 };
                (Content as Grid)?.Children.Add(buttons[i]);
            }

            Grid.SetColumn(buttons[0], 0);
            Grid.SetRow(buttons[0], 0);
            Grid.SetColumnSpan(buttons[0], 3);

            Grid.SetColumn(buttons[1], 3);
            Grid.SetRow(buttons[1], 0);

            Grid.SetColumn(buttons[2], 4);
            Grid.SetRow(buttons[2], 0);
            Grid.SetColumnSpan(buttons[2], 2);

            Grid.SetColumn(buttons[3], 0);
            Grid.SetRow(buttons[3], 1);
            Grid.SetColumnSpan(buttons[3], 2);

            Grid.SetColumn(buttons[4], 2);
            Grid.SetRow(buttons[4], 1);

            Grid.SetColumn(buttons[5], 3);
            Grid.SetRow(buttons[5], 1);
            Grid.SetColumnSpan(buttons[5], 2);

            Grid.SetColumn(buttons[6], 5);
            Grid.SetRow(buttons[6], 1);
            Grid.SetRowSpan(buttons[6], 2);

            Grid.SetColumn(buttons[7], 0);
            Grid.SetRow(buttons[7], 2);
            Grid.SetColumnSpan(buttons[7], 3);

            Grid.SetColumn(buttons[8], 3);
            Grid.SetRow(buttons[8], 2);

            Grid.SetColumn(buttons[9], 4);
            Grid.SetRow(buttons[9], 2);

            Grid.SetColumn(buttons[10], 0);
            Grid.SetRow(buttons[10], 3);

            Grid.SetColumn(buttons[11], 1);
            Grid.SetRow(buttons[11], 3);

            Grid.SetColumn(buttons[12], 2);
            Grid.SetRow(buttons[12], 3);

            Grid.SetColumn(buttons[13], 3);
            Grid.SetRow(buttons[13], 3);
            Grid.SetColumnSpan(buttons[13], 3);
            Grid.SetRowSpan(buttons[13], 4);

            Grid.SetColumn(buttons[14], 0);
            Grid.SetRow(buttons[14], 4);
            Grid.SetColumnSpan(buttons[14], 3);
            Grid.SetRowSpan(buttons[14], 2);

            Grid.SetColumn(buttons[15], 0);
            Grid.SetRow(buttons[15], 6);
            Grid.SetRowSpan(buttons[15], 2);

            Grid.SetColumn(buttons[16], 1);
            Grid.SetRow(buttons[16], 6);

            Grid.SetColumn(buttons[17], 1);
            Grid.SetRow(buttons[17], 7);

            Grid.SetColumn(buttons[18], 2);
            Grid.SetRow(buttons[18], 6);
            Grid.SetRowSpan(buttons[18], 2);

            Grid.SetColumn(buttons[19], 3);
            Grid.SetRow(buttons[19], 7);

            Grid.SetColumn(buttons[20], 4);
            Grid.SetRow(buttons[20], 7);

            Grid.SetColumn(buttons[21], 5);
            Grid.SetRow(buttons[21], 7);
        }
    }
}
