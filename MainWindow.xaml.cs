using Microsoft.Win32;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDFTransform
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<FileFormat> li = new List<FileFormat>();
            li.Add(FileFormat.PDF);
            li.Add(FileFormat.XPS);
            li.Add(FileFormat.DOC);
            li.Add(FileFormat.DOCX);
            li.Add(FileFormat.HTML);
            li.Add(FileFormat.SVG);
            li.Add(FileFormat.PCL);
            li.Add(FileFormat.POSTSCRIPT);
            this.cob_Format.ItemsSource = li;
            this.cob_Format.SelectedIndex = 0;
        }
        string name = "";
        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if ((bool)openFile.ShowDialog())
            {
                name = openFile.SafeFileName;
                string typeName = name.Substring(name.LastIndexOf('.')).ToUpper();
                name = name.Substring(0, name.LastIndexOf('.'));
                if (typeName.Contains("PDF"))
                {
                    txb_FileName.Text = openFile.FileName;
                }
            }
        }
        private void Btn_tarsform_Click(object sender, RoutedEventArgs e)
        {
            if (txb_FileName.Text == "")
            {
                MessageBox.Show("请先导入PDF文件");
                return;
            }
            if (SharParm.flage)
            {
                MessageBox.Show("正在转换文件，请转换完毕再执行下一个文件转换！");
                return;
            }
            this.progressSpeed.Myprogress.Value = 0;
            progressSpeed.bindtxt.Text = "0";
            progressSpeed.percent.Text = "%";
            FileFormat fileFormat = (FileFormat)cob_Format.SelectedIndex;
            if (!SharParm.IsSave)
            {
                MessageBox.Show("暂时不能提供！");
                SharParm.flage = false;
                return;
                //SharParm.flage = true;
                //string fileName = txb_FileName.Text;
                //string saveFileName = "";
                //ThreadPool.QueueUserWorkItem((o) =>
                //{
                //    this.progressSpeed.trasformText.Dispatcher.Invoke(() =>
                //    {
                //        this.progressSpeed.trasformText.Text = "正在转换中";
                //    });
                //    TrasformFile trasform = new TrasformFile();
                //    trasform.PdfTrasformOther(fileName, saveFileName, fileFormat, this);
                //});
            }
            else
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.FileName = name + "." + cob_Format.SelectedValue;
                if ((bool)saveFile.ShowDialog())
                {
                    SharParm.flage = true;
                    string fileName = txb_FileName.Text;
                    string saveFileName = saveFile.FileName;
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        this.progressSpeed.trasformText.Dispatcher.Invoke(() =>
                        {
                            this.progressSpeed.trasformText.Text = "正在转换中";
                        });
                        TrasformFile trasform = new TrasformFile();
                        trasform.PdfTrasformOther(fileName, saveFileName, fileFormat, this);
                    });
                }
            }

        }

        private void CheckSave_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkSave.IsChecked)
            {
                SharParm.IsSave = true;
            }
            else
            {
                SharParm.IsSave = false;
            }
        }
    }
}
