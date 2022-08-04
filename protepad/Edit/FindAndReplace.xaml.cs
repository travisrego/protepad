using System.Windows;

namespace protepad.Edit;

public partial class FindAndReplaceDialog
{
    private bool _shouldUpdateSearchstring = true;
    private readonly MainWindow _protepad = new(); 
    public FindAndReplaceDialog()
    {
        InitializeComponent();
    }

    private void FindNextButton_Click(object sender, RoutedEventArgs e)
    {

    }
}