using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;
using MessageBox = System.Windows.MessageBox;

namespace laboratory_8
{
    /// <summary>
    /// Interaction logic for CreateNewElement.xaml
    /// </summary>
    public partial class CreateNewElement : Window
    {
        public string ItemName { get; set; }
        public string NewItemPath { get; set; }
        public string ParentDirectory { get; set; }
        public bool IsFile { get; set; }
        public FileAttributes Attributes { get; set; }

        public CreateNewElement(string path)
        {
            InitializeComponent();
            DataContext = this;

            ParentDirectory = path;
            NewItemPath = Path.Combine(path, "NewFileOrFolder");
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ItemName = NameTextBox.Text;
            IsFile = fileRadioButton.IsChecked == true ? true : false;

            Attributes = FileAttributes.Normal
                         | (readOnlyCheckBox.IsChecked == true ? FileAttributes.ReadOnly : FileAttributes.Normal)
                         | (archiveCheckBox.IsChecked == true ? FileAttributes.Archive : FileAttributes.Normal)
                         | (hiddenCheckBox.IsChecked == true ? FileAttributes.Hidden : FileAttributes.Normal)
                         | (systemCheckBox.IsChecked == true ? FileAttributes.System : FileAttributes.Normal);

            if (IsFile && !Regex.IsMatch(ItemName, @"^[\w~\-.]{1,8}\.(txt|php|html)$", RegexOptions.IgnoreCase))
                {
                    MessageBox.Show("Invalid file name. Please use up to 8 characters for the base name and one of the allowed extensions: txt, php, html.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string NewItemPath = Path.Combine(ParentDirectory, ItemName);

                MessageBox.Show($"NewItemPath: {NewItemPath}", "Debug Info", MessageBoxButton.OK, MessageBoxImage.Information);


                if (File.Exists(NewItemPath) || Directory.Exists(NewItemPath))
                {
                    MessageBox.Show("A file or folder with the same name already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (IsFile)
                {

                    File.Create(NewItemPath).Close();
                    //File.WriteAllText(NewItemPath, string.Empty);
                    File.SetAttributes(NewItemPath, Attributes);
                }
                else
                {
                    Directory.CreateDirectory(NewItemPath.TrimEnd('.'));
                }

                Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
