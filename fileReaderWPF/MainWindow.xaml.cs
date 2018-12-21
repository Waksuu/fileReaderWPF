using fileReaderWPF.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace fileReaderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string folderPath;

        private static object _syncLock = new object();

        private List<string> vilableFilesToRead = new List<string>();
        private ObservableCollection<PhraseLocation> location = new ObservableCollection<PhraseLocation>();

        public MainWindow()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            InitializeComponent();

            BindingOperations.EnableCollectionSynchronization(location, _syncLock);
            dataGridViev.ItemsSource = location;
        }

        private void phraseLocation()
        {
            this.Dispatcher.Invoke(() =>
            {
                List<string> lines = new List<string>();
                location.Clear();

                string line;
                bool contains;
                string regexText = phraseTxt.Text;

                int lineCount = 0;

                foreach (var item in vilableFilesToRead)
                {
                    lock (_syncLock)
                    {
                        StreamReader sr = new StreamReader(item);
                        while ((line = sr.ReadLine()) != null)
                        {
                            lineCount++;

                            contains = Regex.IsMatch(line, @"\b" + regexText + @"\b");
                            if (contains)
                            {
                                location.Add(new PhraseLocation { Line = lineCount, Path = item, Sentence = line });
                            }
                        }
                        sr.Close();
                        lineCount = 0;
                    }
                }
            });
        }

        private Task phraseLocationAsync()
        {
            return Task.Run(() => phraseLocation());
        }

        private void updateGrid(List<PhraseLocation> list)
        {
            dataGridViev.ItemsSource = null;
            dataGridViev.ItemsSource = list;
            dataGridViev.Items.Refresh();
        }

        private void folderSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.Description = "Select folder with files to search";
            if (fbd.ShowDialog().ToString().Equals("OK"))
            {
                runSeatchBtn.IsEnabled = true;
                vilableFilesToRead.Clear();
                folderPath = fbd.SelectedPath;
                System.Windows.MessageBox.Show(folderPath);
                foreach (string file in Directory.GetFiles(@folderPath))
                {
                    if (System.IO.Path.GetExtension(file) == ".txt")
                    {
                        vilableFilesToRead.Add(file);
                    }
                }
            }
        }

        private async void runSeatchBtn_Click(object sender, RoutedEventArgs e)
        {
            await phraseLocationAsync();

            //updateGrid(wordsLocation2);
            // this.dataGridViev.ItemsSource = wordsLocation2;
            //foreach (var word in wordsLocation2)
            //{
            //    System.Windows.MessageBox.Show($"Path: {word.Path}\nLine: {word.Line}");
            //}
        }

        private void dataGridViev_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            List<string> phraseIntoList = new List<string>();
            richTextBoxRun.Text = "";
            String line = "";
            line = location[dataGridViev.SelectedIndex].Sentence;
            richTextBoxRun.Text = line;
            //phraseIntoList = phraseTxt.Text.Split(' ').ToList();

            //IEnumerable<TextRange> wordRanges = GetAllWordRanges(richTextBox1.Document);
            //foreach (TextRange wordRange in wordRanges)
            //{
            //    if (i < phraseIntoList.Count)
            //    {
            //        if (wordRange.Text == phraseIntoList[i])
            //        {
            //            wordRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            //            i++;
            //        }
            //    }
            //    else
            //    {
            //        i = 0;
            //    }
            //}
        }

        //public static IEnumerable<TextRange> GetAllWordRanges(FlowDocument document)
        //{
        //    string pattern = @"[^\W\d](\w|[-']{1,2}(?=\w))*";
        //    TextPointer pointer = document.ContentStart;
        //    while (pointer != null)
        //    {
        //        if (pointer.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
        //        {
        //            string textRun = pointer.GetTextInRun(LogicalDirection.Forward);
        //            MatchCollection matches = Regex.Matches(textRun, pattern);
        //            foreach (Match match in matches)
        //            {
        //                int startIndex = match.Index;
        //                int length = match.Length;
        //                TextPointer start = pointer.GetPositionAtOffset(startIndex);
        //                TextPointer end = start.GetPositionAtOffset(length);
        //                yield return new TextRange(start, end);
        //            }
        //        }

        //        pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
        //    }
        //}
    }
}