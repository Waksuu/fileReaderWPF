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
    // TODO: Add tests
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Dependencies
        private Lazy<ISearchLogicService> _searchLogicService;
        #endregion Dependencies

        private const string OK = "OK";
        private const string EN_US = "en-us";
        private static object _synchronizationLock = new object();

        private string _selectedDirectoryPathForPhraseSearch;
        private HashSet<string> _selectedFileExtensions = new HashSet<string>();
        private ObservableCollection<PhraseLocation> _phraseLocations = new ObservableCollection<PhraseLocation>();

        public MainWindow()
        {
            ResolveDependencies();
            SetUICulture(EN_US);
            InitializeComponent();
            BindPhraseLocations();
        }

        private void ResolveDependencies()
        {
            var diContainer = ServiceLocator.WpfContainer;
            _searchLogicService = diContainer.Resolve<Lazy<ISearchLogicService>>();
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
                _selectedDirectoryPathForPhraseSearch = folderBrowserDialog.SelectedPath;
                runSearchBtn.IsEnabled = true;
            }
        }

        private static FolderBrowserDialog CreateFolderBrowserDialog() => new FolderBrowserDialog
        {
            RootFolder = Environment.SpecialFolder.MyComputer,
            Description = Base.Properties.Resources.FolderBrowserDialogDescription
        };

        private static bool ClickedOKInDialog(CommonDialog dialog) => dialog.ShowDialog().ToString().Equals(OK);

        private async void RunSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO: UX Need to click twice on the first search?
            // TODO: UX This probably should reset paragraph, as it's content is no longer relevant

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

            // TODO: UX there is no indication if something failed or there are no files to search from

            var foundSentences = await FindSentencesInFolderPath();

            AddAllFoundSentencesToPhraseLocations(foundSentences);
        }

        private bool ThereIsNoSelectedFileExtensions() => !_selectedFileExtensions.Any();

        private bool SearchDirectoryDoesNotExist() => !Directory.Exists(_selectedDirectoryPathForPhraseSearch);

        private static void ShowMessageBox(string message) => System.Windows.MessageBox.Show(message);

        private async System.Threading.Tasks.Task<IEnumerable<PhraseLocation>> FindSentencesInFolderPath()
        {
            var soughtPhrase = this.soughtPhrase.Text;

            return await _searchLogicService.Value.SearchWordsInFilesAsync(_selectedFileExtensions, soughtPhrase, _selectedDirectoryPathForPhraseSearch);
        }

        private void AddAllFoundSentencesToPhraseLocations(IEnumerable<PhraseLocation> foundSentences)
        {
            _phraseLocations.Clear();
            foundSentences.ForEach(sentence => _phraseLocations.Add(sentence));
        }

        private void DisplayParagraphFromClickedResult_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // TODO: UX There is no indication which file is selected

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

        private void ResetComboBoxSelectedItem_SelectionChanged(object comboBox, System.Windows.Controls.SelectionChangedEventArgs e) => ((System.Windows.Controls.ComboBox)comboBox).SelectedItem = null;

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