using fileReaderWPF.Base.Logic;
using fileReaderWPF.Base.Model;
using fileReaderWPF.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
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
        public Lazy<ISearchLogic> SearchLogic { get; set; }

        #endregion Dependencies

        private string folderPath;
        private HashSet<string> extensions = new HashSet<string>();
        private ObservableCollection<PhraseLocation> phraseLocations = new ObservableCollection<PhraseLocation>();

        private static object _syncLock = new object();
        private IUnityContainer container;

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
            SearchLogic = container.Resolve<Lazy<ISearchLogic>>();
        }

        private void FolderSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.Description = Base.Properties.Resources.FolderBrowserDialogDescription;

            if (fbd.ShowDialog().ToString().Equals("OK"))
            {
                folderPath = fbd.SelectedPath;
                runSearchBtn.IsEnabled = true;
            }
        }

        private async void RunSeatchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!extensions.Any())
            {
                System.Windows.MessageBox.Show(Base.Properties.Resources.CouldNotFindAnyExtensions);
                return;
            }

            if (!Directory.Exists(folderPath))
            {
                System.Windows.MessageBox.Show(Base.Properties.Resources.DirectoryDoesntExist);
                return;
            }

            string soughtPhrase = this.soughtPhrase.Text;
            var sentences = await SearchLogic.Value.SearchWordsInFilesAsync(extensions, soughtPhrase, folderPath, container);

            phraseLocations.Clear();
            sentences.ForEach(x => phraseLocations.Add(x));
        }

        private void DisplayParagraphFromClickedResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<string> phraseIntoList = new List<string>();
            sentencePreview.Text = string.Empty;
            string paragraph = string.Empty;
            if (searchResultsGrid.SelectedIndex >= 0)
            {
                paragraph = phraseLocations[searchResultsGrid.SelectedIndex].Sentence;
                sentencePreview.Text = paragraph;
            }
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
    }
}