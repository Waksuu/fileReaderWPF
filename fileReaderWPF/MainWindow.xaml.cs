using fileReaderWPF.Base.Model;
using fileReaderWPF.Base.Service;
using fileReaderWPF.Services.Factory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
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
        private const string OK = "OK";
        private const string EN_US = "en-us";
        private static object _synchronizationLock = new object();

        private string _selectedDirectoryPathForPhraseSearch;
        private HashSet<string> _selectedFileExtensions = new HashSet<string>();
        private ObservableCollection<PhraseLocation> _phraseLocations = new ObservableCollection<PhraseLocation>();

        public MainWindow()
        {
            SetUICulture(EN_US);
            InitializeComponent();
            BindPhraseLocations();
        }

        private static void SetUICulture(string uiCulture) => Thread.CurrentThread.CurrentUICulture = new CultureInfo(uiCulture);

        private void BindPhraseLocations()
        {
            BindingOperations.EnableCollectionSynchronization(_phraseLocations, _synchronizationLock);
            searchResultsGrid.ItemsSource = _phraseLocations;
        }

        #region Actions

        private void FolderSelectBtn_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = CreateFolderBrowserDialog();

            if (ClickedOKInDialog(folderBrowserDialog))
            {
                var selectedPath = folderBrowserDialog.SelectedPath;
                _selectedDirectoryPathForPhraseSearch = selectedPath;
                ShowSelectedPath(selectedPath);
                runSearchBtn.IsEnabled = true;
            }
        }

        private static FolderBrowserDialog CreateFolderBrowserDialog() => new FolderBrowserDialog
        {
            RootFolder = Environment.SpecialFolder.MyComputer,
            Description = Base.Properties.Resources.FolderBrowserDialogDescription
        };

        private static bool ClickedOKInDialog(CommonDialog dialog) => dialog.ShowDialog().ToString().Equals(OK);

        private static void ShowSelectedPath(string selectedPath) => ShowMessageBox(string.Format(Base.Properties.Resources.ShowSelectedFolder, selectedPath));

        private async void RunSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            _phraseLocations.Clear();
            IEnumerable<PhraseLocation> foundSentences = new List<PhraseLocation>();

            if (ThereIsNoSelectedFileExtensions())
            {
                ShowMessageBox(Base.Properties.Resources.CouldNotFindAnyExtensions);
                return;
            }

            if (SearchDirectoryDoesNotExist())
            {
                ShowMessageBox(Base.Properties.Resources.DirectoryDoesntExist);
                return;
            }

            try
            {
                foundSentences = await ExecuteSearchLogicService();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
                return;
            }

            if (CouldNotFindMatchIn(foundSentences))
            {
                ShowMessageBox(Base.Properties.Resources.CouldNotFindPhrase);
                return;
            }

            AddAllFoundSentencesToPhraseLocations(foundSentences);
        }

        private bool ThereIsNoSelectedFileExtensions() => !_selectedFileExtensions.Any();

        private bool SearchDirectoryDoesNotExist() => !Directory.Exists(_selectedDirectoryPathForPhraseSearch);

        private static void ShowMessageBox(string message) => System.Windows.MessageBox.Show(message);

        private async Task<IEnumerable<PhraseLocation>> ExecuteSearchLogicService()
        {
            var soughtPhrase = this.soughtPhrase.Text;
            ISearchLogicService searchLogicService = SearchLogicServiceFactory.CreateSearchLogicService();

            return await searchLogicService.FindSentencesInFolderPath(_selectedFileExtensions, soughtPhrase, _selectedDirectoryPathForPhraseSearch);
        }

        private bool CouldNotFindMatchIn(IEnumerable<PhraseLocation> foundSentences) => !foundSentences.Any();

        private void AddAllFoundSentencesToPhraseLocations(IEnumerable<PhraseLocation> foundSentences) => foundSentences.ToList().ForEach(sentence => _phraseLocations.Add(sentence));

        private void DisplayParagraphFromClickedResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ResetSentencePreview();

            if (ItemOnGridIsSelected())
            {
                sentencePreview.Text = _phraseLocations[searchResultsGrid.SelectedIndex].Sentence;
            }
        }

        private void ResetSentencePreview() => sentencePreview.Text = string.Empty;

        private bool ItemOnGridIsSelected() => searchResultsGrid.SelectedIndex >= 0;

        private void SelectOrDeselectAllExtensions_Click(object checkAllCheckbox, RoutedEventArgs e) => ChangeSelectionStatusOfAllCheckboxes((System.Windows.Controls.CheckBox)checkAllCheckbox);

        private void ChangeSelectionStatusOfAllCheckboxes(System.Windows.Controls.CheckBox checkBox)
        {
            if (!checkBox.IsChecked.HasValue)
            {
                return;
            }

            foreach (System.Windows.Controls.CheckBox extensionCheckBox in extensionComboBox.Items)
            {
                extensionCheckBox.IsChecked = checkBox.IsChecked.Value;
            }
        }

        private void ResetComboBoxSelectedItem_SelectionChanged(object extensionCheckbox, System.Windows.Controls.SelectionChangedEventArgs e) => ((System.Windows.Controls.ComboBox)extensionCheckbox).SelectedItem = null;

        private void AddExtension_Checked(object extensionCheckbox, RoutedEventArgs e) => HandleExtensionsCheckboxClick((System.Windows.Controls.CheckBox)extensionCheckbox);

        private void RemoveExtension_Unchecked(object extensionCheckbox, RoutedEventArgs e) => HandleExtensionsCheckboxClick((System.Windows.Controls.CheckBox)extensionCheckbox);

        private void HandleExtensionsCheckboxClick(System.Windows.Controls.CheckBox checkbox)
        {
            string extensionFromCheckbox = GetExtensionFromCheckbox(checkbox);

            UpdateSelectedExtensionList(extensionFromCheckbox);
        }

        private static string GetExtensionFromCheckbox(System.Windows.Controls.CheckBox checkbox) => checkbox.Content.ToString();

        private void UpdateSelectedExtensionList(string extension)
        {
            if (!_selectedFileExtensions.Contains(extension))
            {
                _selectedFileExtensions.Add(extension);
            }
            else
            {
                _selectedFileExtensions.Remove(extension);
            }
        }
    }

    #endregion Actions
}