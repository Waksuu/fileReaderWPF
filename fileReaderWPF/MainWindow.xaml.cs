using fileReaderWPF.Base.Helpers;
using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Model;
using fileReaderWPF.Base.Patterns.Specification;
using fileReaderWPF.Base.Repository;
using fileReaderWPF.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        [Dependency]
        public Lazy<ISpecificationHelper> SpecificationHelper { get; set; }

        #endregion Dependencies

        private IEnumerable<string> filesForPath;
        private HashSet<string> extensions = new HashSet<string>();

        private static object _syncLock = new object();
        private IUnityContainer container;

        private ObservableCollection<PhraseLocation> phraseLocations = new ObservableCollection<PhraseLocation>();

        public MainWindow()
        {
            container = ServiceLocator.WpfContainer;
            ResolveDependencies(container);

            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");

            InitializeComponent();

            BindingOperations.EnableCollectionSynchronization(phraseLocations, _syncLock);
            searchResultsGrid.ItemsSource = phraseLocations;
        }

        private void ResolveDependencies(IUnityContainer container)
        {
            FolderRepository = container.Resolve<Lazy<IFolderRepository>>();
            SearchLogic = container.Resolve<Lazy<ISearchLogic>>();
            SpecificationHelper = container.Resolve<Lazy<ISpecificationHelper>>();
        }

        private void FolderSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!extensions.Any())
            {
                System.Windows.MessageBox.Show(Base.Properties.Resources.CouldNotFindAnyExtensions);
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.Description = Base.Properties.Resources.FolderBrowserDialogDescription;

            if (fbd.ShowDialog().ToString().Equals("OK"))
            {
                string folderPath = fbd.SelectedPath;

                ISpecification<string> extensionSpecification = SpecificationHelper.Value.SpecifyExtensions(extensions);
                filesForPath = FolderRepository.Value.GetFilesForPath(folderPath, extensionSpecification);

                runSearchBtn.IsEnabled = true;
            }
        }

        private async void RunSeatchBtn_Click(object sender, RoutedEventArgs e)
        {
            string soughtPhrase = this.soughtPhrase.Text;
            var sentences = await SearchLogic.Value.SearchWordsAsync(filesForPath, soughtPhrase, container);

            phraseLocations.Clear();
            sentences.ForEach(x => phraseLocations.Add(x));
        }

        private void searchResultsGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //int i = 0;
            List<string> phraseIntoList = new List<string>();
            sentencePreview.Text = string.Empty;
            string paragraph = string.Empty;
            if (searchResultsGrid.SelectedIndex >= 0)
            {
                paragraph = phraseLocations[searchResultsGrid.SelectedIndex].Sentence;
                sentencePreview.Text = paragraph;
            }
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

        private void SelectOrDeselectAllExtensions_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = (System.Windows.Controls.CheckBox)sender;

            if (checkBox.IsChecked.HasValue && checkBox.IsChecked.Value)
            {
                foreach (var item in extensionComboBox.Items)
                {
                    (item as System.Windows.Controls.CheckBox).IsChecked = true;
                }
            }
            else
            {
                foreach (var item in extensionComboBox.Items)
                {
                    (item as System.Windows.Controls.CheckBox).IsChecked = false;
                }
            }
        }

        private void ResetComboBoxSelectedItem_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var comboBox = (System.Windows.Controls.ComboBox)sender;
            comboBox.SelectedItem = null;
        }

        private void AddExtension_Checked(object sender, RoutedEventArgs e)
        {
            var checkbox = (System.Windows.Controls.CheckBox)sender;
            var extension = checkbox.Content.ToString();

            if (!extensions.Contains(extension))
            {
                extensions.Add(extension);
            }
        }

        private void RemoveExtension_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = (System.Windows.Controls.CheckBox)sender;
            var extension = checkbox.Content.ToString();

            if (extensions.Contains(extension))
            {
                extensions.Remove(extension);
            }
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