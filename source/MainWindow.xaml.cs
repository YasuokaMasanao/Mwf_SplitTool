using Microsoft.Win32;
using System.Data;
using System.Formats.Asn1;
using System.Text;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace SplitTool;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    public const int SPLIT_SIZE = 10 * 24 * 60 * 60 * 250; // 10 day
    public MainWindow() {
        InitializeComponent();
    }

    private void btn_cite_Click(object sender, RoutedEventArgs e) {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "Text files (*.mwf)|*.mwf|All files (*.*)|*.*";
        if (openFileDialog.ShowDialog() == true) {
            string filePath = openFileDialog.FileName;
            txb_filename.Text = filePath;
        }
    }

    private void btn_split_Click(object sender, RoutedEventArgs e) {
        btn_split.IsEnabled = false;
        string filePath = txb_filename.Text;
        MwfReader reader = new MwfReader(filePath);
        filePath = filePath.Substring(0, filePath.Length - 4);
        int i = 1;
        int startidx = 0;
        int endidx = SPLIT_SIZE;
        int max = reader.Signal.Length / SPLIT_SIZE;
        DateTime recordingTime = (DateTime)reader.MetaData["Recording time"];
        Window1 progressWindow = new Window1();
        progressWindow.Show();
        while (endidx < reader.Signal.Length) {
            reader.WriteMFERHeader(filePath + "_" + i.ToString("D3") + ".mwf", reader.Signal[startidx..endidx], recordingTime);
            recordingTime = recordingTime.AddSeconds(SPLIT_SIZE / 250);
            i += 1;
            startidx = endidx;
            endidx += SPLIT_SIZE;
            progressWindow.UpdateProgress(i, max);
        }
        reader.WriteMFERHeader(filePath + "_" + i.ToString("D3") + ".mwf", reader.Signal[startidx..], recordingTime);
        progressWindow.btn_ok.IsEnabled = true;
        btn_split.IsEnabled = true;
    }
}
