using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;

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
            currentText = textBox.Text; 
            return currentText;
        }

        // variable for current zoom level
        private readonly double _zoomLevel;
        //path to file
        private string _path = string.Empty;
        // name of file
        private string _fileName = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            _zoomLevel = textBox.FontSize;
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
           OpenFileDialog open = new OpenFileDialog();
        
            open.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            open.Title = "Open File";
            open.FileName = "";
        
            if (open.ShowDialog() == true)
            {
                // save the opened FileName in our variable
                _fileName = open.FileName;
                // open the file
                textBox.Text = File.ReadAllText(_fileName);
                textBox.Text = $"{Path.GetFileNameWithoutExtension(open.FileName)}";
                var reader = new StreamReader(open.FileName);
                textBox.Text = reader.ReadToEnd();
                // set the title of the window to the name of the file with the extension
                Title = $"{Path.GetFileName(open.FileName)} - Protepad";
                reader.Close();
            }
        }

        private void WordWrapMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.TextWrapping = WordWrapMenuButton.IsChecked ? TextWrapping.Wrap : TextWrapping.NoWrap;
        }

        private void ZoomInMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.FontSize += 1;
        }

        private void ZoomOutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.FontSize -= 1;
        }

        private void ResetDefZoomMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.FontSize = _zoomLevel;
        }

        private void SaveMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // if file name is empty, open save dialog
            if (string.IsNullOrEmpty(_fileName))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                    AddExtension = true,
                    DefaultExt = "txt"
                };
                // if user clicks ok, save file
                if (saveFileDialog.ShowDialog() == true)
                {
                    _path = saveFileDialog.FileName;
                    _fileName = saveFileDialog.SafeFileName;
                    // File.WriteAllText(_path, text);
                }
            }
            // if file name is not empty, save file
            else
            {
                // File.WriteAllText(_path, text);
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
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                var streamWriter = new StreamWriter(fileStream);
                streamWriter.Close();
                fileStream.Close();
            }
            // USE METHOD HERE 
        }

        private void UndoMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Undo();
        }

        private void RedoMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Redo();
        }

        private void CutMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Cut();
        }

        private void CopyMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Copy();
        }

        private void PasteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.Paste();
        }

        private void DeleteMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectedText = "";
        }

        private void SelectAllMenuButton_Click(object sender, RoutedEventArgs e)
        {
            textBox.SelectAll();
        }

        private void FindMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var findDialog = new FindDialog
            {
                // Code from https://bit.ly/3x8PixS
                Topmost = false,
                Owner = this
            };
            findDialog.Show();
        }


        private void ReplaceMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // var replaceDialog = new ReplaceDialog();
            // replaceDialog.Owner = this;
            // replaceDialog.ShowDialog();
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
            return textBox.Text;
        }
    }
}