using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace _12_09_2022
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        public string? Player { get; private set; }
        public bool? IsMultiplying { get; private set; }
        public int Complexity { get; private set; }
        public DateTime SessionBeginning { get; private set; }
        private int correctAnswers = 0;

        public GameWindow(string? player)
        {
            InitializeComponent();
            Player = player;
            if (!File.Exists("scoreTab.txt"))
                File.Create("scoreTab.txt");
            FillTheTableIsPossiable();
        }

        private void EndGame()
        {
            x.Text = string.Empty;
            y.Text = string.Empty;

            integerUpDown.IsEnabled = true;
            complexitySlider.IsEnabled = true;
            playButton.IsEnabled = true;
            multiplyingRadioButton.IsEnabled = true;
            dividingRadioButton.IsEnabled = true;
            scoreTab.IsEnabled = true;

            answer.IsEnabled = false;
            stopButton.IsEnabled = false;
            checkButton.IsEnabled = false;
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
                EndGame();
        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                SessionBeginning = DateTime.Now;
                resaultsListBox.Items.Clear();
                prgressBar.Value = 0;

                IsMultiplying = multiplyingRadioButton.IsChecked;
                Complexity = (int)complexitySlider.Value;


                integerUpDown.IsEnabled = false;
                complexitySlider.IsEnabled = false;
                playButton.IsEnabled = false;
                multiplyingRadioButton.IsEnabled = false;
                dividingRadioButton.IsEnabled = false;
                scoreTab.IsEnabled = false;

                answer.IsEnabled = true;
                stopButton.IsEnabled = true;
                checkButton.IsEnabled = true;

                GenerateNumbers();
            }
        }

        private void ButtonCheck_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                if (!int.TryParse(answer.Text, out int answerInt))
                {
                    MessageBox.Show("Your input is not an integer number!", "Wrong input", MessageBoxButton.OK, MessageBoxImage.Error);
                    answer.Text = string.Empty;
                    return;
                }

                int xInt = int.Parse(x.Text);
                int yInt = int.Parse(y.Text);
                TextBlock? text = null;

                if ((IsMultiplying == true && xInt * yInt == answerInt) || (IsMultiplying == false && xInt / yInt == answerInt))
                {
                    text = new()
                    {
                        Text = "Correct! " + x.Text + (IsMultiplying == true ? " X " : " / ") + y.Text + " = " + answerInt,
                        Foreground = new SolidColorBrush(Colors.LawnGreen)
                    };
                    correctAnswers++;
                }
                else
                {
                    int correctAnswer = IsMultiplying == true ? xInt * yInt : xInt / yInt;
                    text = new()
                    {
                        Text = "Wrong! " + x.Text + (IsMultiplying == true ? " X " : " / ") + y.Text + " = " + correctAnswer + "\nYour answer: " + answerInt,
                        Foreground = new SolidColorBrush(Colors.Red)
                    };
                }

                resaultsListBox.Items.Add(text);
                answer.Text = string.Empty;
                prgressBar.Value++;
            }
        }

        private void GenerateNumbers()
        {
            Random random = new();
            int xInt = 0;
            int yInt = 0;
            switch (Complexity)
            {
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            x.Text = random.Next(12).ToString();
                            y.Text = random.Next(12).ToString();
                            break;
                        case 1:
                            xInt = random.Next(1, 11) * 100;
                            yInt = random.Next(1, 11);
                            x.Text = xInt.ToString();
                            y.Text = yInt.ToString();
                            break;
                        default:
                            xInt = random.Next(1, 11) * 10;
                            yInt = random.Next(1, 11);
                            x.Text = xInt.ToString();
                            y.Text = yInt.ToString();
                            break;
                    }
                    break;
                case 2:
                    x.Text = random.Next(101).ToString();
                    y.Text = random.Next(101).ToString();
                    break;
                case 3:
                    do
                    {
                        xInt = random.Next(12, 101);
                        yInt = random.Next(12, 101);
                    } while (xInt % 2 == 0 || xInt % 5 == 0 || xInt % 10 == 0 || yInt % 2 == 0 || yInt % 5 == 0 || yInt % 10 == 0);
                    x.Text = xInt.ToString();
                    y.Text = yInt.ToString();
                    break;
            }

            if (IsMultiplying is not null && !(bool)IsMultiplying)
                x.Text = (xInt * yInt).ToString();
        }


        //Можете в ЛС написати як це можна було б реалізувати в XAML? Та й взагалі, я витратив не мало часу на це і хотілося б почути хоча б якийсь відгук

        private void multiplyingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
                mathOperationTextBlock.Text = "x";
        }

        private void dividingRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
                mathOperationTextBlock.Text = "/";
        }

        private void prgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is ProgressBar)
            {
                if (prgressBar.Value == prgressBar.Maximum)
                {
                    EndGame();
                    TimeSpan timeSpan = DateTime.Now.Subtract(SessionBeginning);
                    string duration = $"{timeSpan.Hours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
                    string type = (IsMultiplying == true ? "Multiplying" : "Dividing");
                    string info = $"{Player}|{SessionBeginning:yyyy.MM.dd HH:mm:ss}|{duration}|{Complexity}|{type}|{correctAnswers}/{prgressBar.Maximum}\n";
                    File.AppendAllText("scoreTab.txt", info);
                }
                else
                    GenerateNumbers();
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                scoreTable.Items.Clear();
                FillTheTableIsPossiable();
            }
        }

        private void FillTheTableIsPossiable()
        {
            string[] lines = File.ReadAllLines("scoreTab.txt");
            if (lines.Length == 0)
                return;
            string[][] data = lines
                                .Select(i => i.Split('|'))
                                .Where(i => i[0] == Player)
                                .ToArray();
            foreach (string[] item in data)
            {
                ScoreObject score = new()
                {
                    SessionBeginning = item[1],
                    Duration = item[2],
                    Complexity = item[3],
                    Type = item[4],
                    Answers = item[5]
                };
                scoreTable.Items.Add(score);
            }
        }
    }
}
