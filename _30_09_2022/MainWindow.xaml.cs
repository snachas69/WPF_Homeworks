using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace _30_09_2022
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new() { Interval = new TimeSpan(0, 0, 0, 0, 500) };
        public int Lines
        {
            get { return (int)GetValue(LinesProperty); }
            set { SetValue(LinesProperty, value); }
        }
        public static readonly DependencyProperty LinesProperty =
            DependencyProperty.Register("Lines", typeof(int), typeof(MainWindow), new PropertyMetadata(1));


        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }
        public static readonly DependencyProperty ColumnsProperty =
            DependencyProperty.Register("Columns", typeof(int), typeof(MainWindow), new PropertyMetadata(1));


        public int Words
        {
            get { return (int)GetValue(WordsProperty); }
            set { SetValue(WordsProperty, value); }
        }
        public static readonly DependencyProperty WordsProperty =
            DependencyProperty.Register("Words", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        public DateTime Now
        {
            get { return (DateTime)GetValue(NowProperty); }
            set { SetValue(NowProperty, value); }
        }
        public static readonly DependencyProperty NowProperty =
            DependencyProperty.Register("Now", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(DateTime.Now));

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += RefreshTime;
            timer.Start();
        }

        private void RefreshTime(object? sender, EventArgs e)
        {
            if (sender is DispatcherTimer)
                Now = DateTime.Now;
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Prompt prompt = new();
            if (prompt.ShowDialog() == true)
            {
                RichTextBox richTextBox = new();
                tabPannel.Items.Add(new TabItem() { Header = prompt.Name, Content = richTextBox });
                Binding binding1 = new("FontColor");
                Binding binding2 = new("BackColor");
                richTextBox.SetBinding(ForegroundProperty, binding1);
                richTextBox.SetBinding(BackgroundProperty, binding2);

                FillTetxInfo(richTextBox);
            }
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
            {
                RichTextBox richTextBox = new();
                FlowDocument document = new();
                foreach (var item in File.ReadAllText(openFileDialog.FileName).Split('\n'))
                {
                    Paragraph paragraph = new();
                    paragraph.Inlines.Add(new Run(item));
                    document.Blocks.Add(paragraph);
                }
                richTextBox.Document = document;
                tabPannel.Items.Add(new TabItem()
                {
                    Header = openFileDialog.FileName.Split('\\', StringSplitOptions.RemoveEmptyEntries).Last(),
                    Content = richTextBox,
                    Tag = openFileDialog.FileName,
                });
                richTextBox.KeyDown += WhenKeyDown;
                richTextBox.KeyUp += WhenKeyUp;

                Binding binding1 = new("FontColor");
                Binding binding2 = new("BackColor");
                richTextBox.SetBinding(ForegroundProperty, binding1);
                richTextBox.SetBinding(BackgroundProperty, binding2);
            }

        }

        private void WhenKeyDown(object sender, KeyEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
                FillTetxInfo(richTextBox);
        }

        private void WhenKeyUp(object sender, KeyEventArgs e)
        {
            if (sender is RichTextBox richTextBox)
                FillTetxInfo(richTextBox);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            TabItem? tabItem = (tabPannel.SelectedItem as TabItem);
            if (tabItem?.Tag is null)
            {
                var browser = new CommonOpenFileDialog() { IsFolderPicker = true };
                if (browser.ShowDialog() == CommonFileDialogResult.Ok)
                    tabItem.Tag = browser.FileName + $"\\{tabItem.Header}";
                else
                    return;
            }
            string content = new TextRange((tabItem?.Content as RichTextBox)?.Document.ContentStart, (tabItem?.Content as RichTextBox)?.Document.ContentEnd).Text;
            File.AppendAllText(tabItem.Tag.ToString(), content);
            MessageBox.Show("File has been saved", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            if (tabPannel.SelectedItem is not null)
                tabPannel.Items.Remove(tabPannel.SelectedItem);
        }

        private void FillTetxInfo(RichTextBox richTextBox)
        {
            TextPointer tp1 = richTextBox.Selection.Start.GetLineStartPosition(0);
            TextPointer tp2 = richTextBox.Selection.Start;

            int column = tp1.GetOffsetToPosition(tp2);

            int someBigNumber = int.MaxValue;
            int lineMoved, currentLineNumber;
            richTextBox.Selection.Start.GetLineStartPosition(-someBigNumber, out lineMoved);
            currentLineNumber = -lineMoved;

            Lines = currentLineNumber + 1;
            Columns = column + 1;

            string content = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;
            Words = Regex.Matches(content, @"\b[\w]+\b").Count();
        }

        private void Copy_Click(object sender, RoutedEventArgs e) => ((tabPannel?.SelectedItem as TabItem)?.Content as RichTextBox)?.Copy();
        private void Paste_Click(object sender, RoutedEventArgs e) => ((tabPannel?.SelectedItem as TabItem)?.Content as RichTextBox)?.Paste();
        private void SelectAll_Click(object sender, RoutedEventArgs e) => ((tabPannel?.SelectedItem as TabItem)?.Content as RichTextBox)?.SelectAll();
        private void Cut_Click(object sender, RoutedEventArgs e) => ((tabPannel?.SelectedItem as TabItem)?.Content as RichTextBox)?.Cut();
        private void ViewHelp_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Sorry, help yourself");
        private void SendHelp_Click(object sender, RoutedEventArgs e) => MessageBox.Show("We don't work these eternity");
        private void About_Click(object sender, RoutedEventArgs e) => MessageBox.Show("This notepad is badass");
        private void tabPannel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl && tabControl.SelectedItem is not null)
            {
                RichTextBox? rtb = (tabControl.SelectedItem as TabItem)?.Content as RichTextBox;
                rtb.KeyDown += WhenKeyDown;
                rtb.KeyUp += WhenKeyUp;
                FillTetxInfo(rtb);
            }
        }
    }
}
