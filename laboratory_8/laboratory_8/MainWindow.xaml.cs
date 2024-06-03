using Microsoft.Win32;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MessageBox = System.Windows.MessageBox;
using System;
using System.Text.RegularExpressions;
using Path = System.IO.Path;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace laboratory_8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private DirectoryInfo? selected_directory = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog()
            {
                Description = "Select directory to open",
            };

            // if user chose a directory and clicked OK
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!Directory.Exists(dlg.SelectedPath))
                {
                    MessageBox.Show(this, "Selected invalid path. Try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                // load path to a chosen folder
                selected_directory = new DirectoryInfo(dlg.SelectedPath);
                DisplayRootDirectory();
            }
        }

        private void DisplayRootDirectory()
        {
            treeView.Items.Clear();

            if (selected_directory is null)
            {
                return;
            }

            try
            {
                var rootItem = GetDirectory(selected_directory);
                treeView.Items.Add(rootItem);
                treeView.ContextMenu?.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while displaying root directory: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private TreeViewItem GetDirectory(DirectoryInfo directoryInfo)
        {
            var subDirs = directoryInfo.EnumerateDirectories().Select(GetDirectory);
            var subFiles = directoryInfo.EnumerateFiles().Select(GetFile);

            var root = new TreeViewItem
            {
                Header = directoryInfo.Name,
                Tag = directoryInfo.FullName,
                ContextMenu = new ContextMenu()
            };

            var createMenuItem = new MenuItem
            {
                Header = "Create",
                Tag = directoryInfo.FullName,
            };
            createMenuItem.Click += Create_NewItem;
            root.ContextMenu.Items.Add(createMenuItem);

            var deleteMenuItem = new MenuItem
            {
                Header = "Delete",
                Tag = directoryInfo.FullName
            };
            deleteMenuItem.Click += Directory_OnDelete;
            root.ContextMenu.Items.Add(deleteMenuItem);

            root.Items.Add(new TreeViewItem { Header = "Loading..." });

            root.Expanded += Directory_OnExpanded;

            return root;
        }

        private void Directory_OnExpanded(object sender, RoutedEventArgs e)
        {
            var directoryItem = (TreeViewItem)sender;

            if (directoryItem.Items.Count != 1 || !(directoryItem.Items[0] is TreeViewItem))
            {
                return;
            }

            directoryItem.Items.Clear();

            var directoryPath = (string)directoryItem.Tag;

            LoadDirectoryItems(directoryPath, directoryItem);
        }

        private void LoadDirectoryItems(string directoryPath, TreeViewItem directoryItem)
        {
            try
            {
                var directoryInfo = new DirectoryInfo(directoryPath);
                var subDirectories = directoryInfo.EnumerateDirectories();
                var subFiles = directoryInfo.EnumerateFiles();

                foreach (var subDirectory in subDirectories)
                {
                    var subDirectoryItem = GetDirectory(subDirectory);
                    directoryItem.Items.Add(subDirectoryItem);
                }

                foreach (var subFile in subFiles)
                {
                    var fileItem = GetFile(subFile);
                    directoryItem.Items.Add(fileItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading directory items: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private TreeViewItem GetFile(FileInfo fileInfo)
        {
            var item = new TreeViewItem
            {
                Header = fileInfo.Name,
                Tag = fileInfo.FullName,
                ContextMenu = new ContextMenu(),
            };
            item.Selected += ViewFile_OnSelected;
            item.MouseDoubleClick += ViewFile_OnDoubleClick;
            
            var openMenuItem = new MenuItem
            {
                Header = "Open",
                Tag = fileInfo.FullName
            };
            openMenuItem.Click += File_OnOpen;
            item.ContextMenu.Items.Add(openMenuItem);

            var deleteMenuItem = new MenuItem
            {
                Header = "Delete",
                Tag = fileInfo.FullName
            };
            deleteMenuItem.Click += File_OnDelete;
            item.ContextMenu.Items.Add(deleteMenuItem);

            return item;
        }

        private void ViewFile_OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            string filePath = item.Tag as string;
            if (File.Exists(filePath))
            {
                try
                {
                    FileViewer.Text = File.ReadAllText(filePath, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}");
                }
            }
        }

        private void File_OnOpen(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string filePath = menuItem.Tag as string;
            if (File.Exists(filePath))
            {
                try
                {
                    FileViewer.Text = File.ReadAllText(filePath, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}");
                }
            }
        }

        private void Directory_OnDelete(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string path = menuItem.Tag as string;

            try
            {
                foreach (var file in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                {
                    if (File.GetAttributes(file).HasFlag(FileAttributes.ReadOnly))
                    {
                        File.SetAttributes(file, ~FileAttributes.ReadOnly);
                    }

                    File.Delete(file);
                }
                Directory.Delete(path, true);
                FileViewer.Text = string.Empty;
                DisplayRootDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting directory: {ex.Message}");
            }
        }

        private void File_OnDelete(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string? path = menuItem.Tag as string;

            try
            {
                if (File.GetAttributes(path).HasFlag(FileAttributes.ReadOnly))
                {
                    File.SetAttributes(path, File.GetAttributes(path) & ~FileAttributes.ReadOnly);
                }

                File.Delete(path);
                FileViewer.Text = string.Empty;
                DisplayRootDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting file: {ex.Message}");
            }
        }

        private void ViewFile_OnSelected(object sender, RoutedEventArgs e)
        {
            var menuItem = e.Source as TreeViewItem;

            if (menuItem?.Tag is not string path || !File.Exists(path))
            {
                MessageBox.Show(this, "Invalid path", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var attributes = File.GetAttributes(path);
            var dosAttributes = new StringBuilder();
            dosAttributes.Append((attributes & FileAttributes.ReadOnly) != 0 ? 'r' : '-');
            dosAttributes.Append((attributes & FileAttributes.Archive) != 0 ? 'a' : '-');
            dosAttributes.Append((attributes & FileAttributes.Hidden) != 0 ? 'h' : '-');
            dosAttributes.Append((attributes & FileAttributes.System) != 0 ? 's' : '-');

            AttributeTextBlock.Text = dosAttributes.ToString();
        }

        private void Create_NewItem(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            string? parentDirectory = menuItem.Tag as string;

            try
            {
                var window = new CreateNewElement(parentDirectory);
                window.ShowDialog();
                DisplayRootDirectory();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while creating a new element: {ex.Message}");
            }
        }




        private void Exit_OnClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}