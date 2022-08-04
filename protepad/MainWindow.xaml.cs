using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using protepad.Edit;

namespace protepad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow
    {
        public string TextBoxText(string currentText)
        {
            if (currentText == null) throw new ArgumentNullException(nameof(currentText));
            currentText = TextBox.Text; 
            return currentText;
        }

        // variable for current zoom level
        private readonly double _zoomLevel;
        // path and name of file
        private string _fileName = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            _zoomLevel = TextBox.FontSize;
        }
        
        private void ExitMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void NewMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
            Close();
        }

        private void NewWindowMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new MainWindow();
            newWindow.Show();
        }

        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
           var open = new OpenFileDialog
           {
               Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*",
               Title = "Open File",
               FileName = ""
           };

           if (open.ShowDialog() != true) return;
            // save the opened FileName in our variable
            _fileName = open.FileName;
            // open the file
            TextBox.Text = File.ReadAllText(_fileName);
            TextBox.Text = $"{Path.GetFileNameWithoutExtension(open.FileName)}";
            var reader = new StreamReader(open.FileName);
            TextBox.Text = reader.ReadToEnd();
            // set the title of the window to the name of the file with the extension
            Title = $"{Path.GetFileName(open.FileName)} - Protepad";
            // change the currentDirectory to the new directory
            reader.Close();
        }

        private void WordWrapMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.TextWrapping = WordWrapMenuButton.IsChecked ? TextWrapping.Wrap : TextWrapping.NoWrap;
        }

        private void ZoomInMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.FontSize += 1;
        }

        private void ZoomOutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.FontSize -= 1;
        }

        private void ResetDefZoomMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.FontSize = _zoomLevel;
        }

        private void SaveMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // if file name is empty, open save dialog
            if (string.IsNullOrEmpty(_fileName))
            {
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    AddExtension = true,
                    DefaultExt = "txt"
                };
                // if user clicks ok, save file
                if (saveFileDialog.ShowDialog() == true)
                {
                    _fileName = saveFileDialog.FileName;
                    // open the file
                    TextBox.Text = $"{Path.GetFileNameWithoutExtension(saveFileDialog.FileName)}";
                    // set the title of the window to the name of the file with the extension
                    Title = $"{Path.GetFileName(saveFileDialog.FileName)} - Protepad";
                    // change the currentDirectory to the new directory
                    File.WriteAllText(_fileName, TextBox.Text);
                }
            }
            // if file name is not empty, save file
            else
            {
                File.WriteAllText(_fileName, TextBox.Text);
            }
            // if (string.IsNullOrEmpty(_path))
            // {
            //     var sfd = new SaveFileDialog()
            //     {
            //         Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
            //         InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            //     };
            //     if (sfd.ShowDialog() == true)
            //     {
            //         using (var sw = new StreamWriter(sfd.FileName)) 
            //         {
            //             await sw.WriteAsync(TextBox.Text);
            //         }
            //     }
            // }
            // else
            // {
            //     using (var sw = new StreamWriter(_path))
            //     {
            //         await sw.WriteLineAsync(TextBox.Text); 
            //     }
            // }
            
            // USE METHOD CODE HERE
        }

        private void SaveAsMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var tempPath = _fileName;
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (saveFileDialog.ShowDialog() == true)
            {           
                _fileName = saveFileDialog.FileName;
                // open the file
                TextBox.Text = File.ReadAllText(tempPath);
                TextBox.Text = $"{Path.GetFileNameWithoutExtension(saveFileDialog.FileName)}";
                var reader = new StreamReader(tempPath);
                TextBox.Text = reader.ReadToEnd();
                // set the title of the window to the name of the file with the extension
                Title = $"{Path.GetFileName(saveFileDialog.FileName)} - Protepad";
                // change the currentDirectory to the new directory
                reader.Close();

                File.WriteAllText(_fileName, TextBox.Text);
                
            }
            // USE METHOD HERE 
        }

        private void UndoMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Undo();
        }

        private void RedoMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Redo();
        }

        private void CutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Cut();
        }

        private void CopyMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Copy();
        }

        private void PasteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Paste();
        }

        private void DeleteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.SelectedText = "";
        }

        private void SelectAllMenuButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.SelectAll();
        }

        private void FindMenuButton_Click(object sender, RoutedEventArgs e)
        {
        }


        private void ReplaceMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var replaceDialog = new FindAndReplaceDialog
            {
                // Code from https://bit.ly/3x8PixS
                Topmost = false,
                Owner = this
            };
            replaceDialog.Show();
        }

        private void GoToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // var goToDialog = new GoToDialog();
            // goToDialog.Owner = this;
            // goToDialog.ShowDialog();
        }
        
        // create a method parameter for text in textbox and return the text
        public string GetText()
        {
            return TextBox.Text;
        }
    }
}