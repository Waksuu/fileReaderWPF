using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Model;
using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Configuration;
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
using Unity;
using Unity.Attributes;
using Unity.Interception.Utilities;

namespace fileReaderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Dependencies

        [Dependency]
        public Lazy<IFolderRepository> FolderRepository { get; set; }

        [Dependency]
        public Lazy<ISearchLogic> SearchLogic { get; set; }
        #endregion Dependencies

        private IEnumerable<string> filePaths;
        private static object _syncLock = new object();

        private ObservableCollection<PhraseLocation> location = new ObservableCollection<PhraseLocation>();

        public MainWindow()
        {
            var container = ServiceLocator.WpfContainer;
            ResolveDependencies(container);

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            InitializeComponent();

            BindingOperations.EnableCollectionSynchronization(location, _syncLock);
            dataGridViev.ItemsSource = location;
        }

        private void ResolveDependencies(IUnityContainer container)
        {
            FolderRepository = container.Resolve<Lazy<IFolderRepository>>();
            SearchLogic = container.Resolve<Lazy<ISearchLogic>>();
        }

        private void FolderSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.Description = "Select folder with files to search";

            if (fbd.ShowDialog().ToString().Equals("OK"))
            {
                string folderPath = fbd.SelectedPath;

                ISpecification<string> specification = new ExpressionSpecification<string>(o => Path.GetExtension(o).Equals(".txt")); // TODO Nie hard code txt
                filePaths = FolderRepository.Value.GetFilePaths(folderPath, specification);

                runSearchBtn.IsEnabled = true;
            }
        }

        private async void RunSeatchBtn_Click(object sender, RoutedEventArgs e)
        {
            string regexText = phraseTxt.Text;
            var sentences = await SearchLogic.Value.SearchWords(filePaths, regexText,_syncLock);
            location.Clear();
            sentences.ForEach(x => location.Add(x));
        }

        private void DataGridViev_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //int i = 0;
            List<string> phraseIntoList = new List<string>();
            richTextBoxRun.Text = string.Empty;
            string line = string.Empty;
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