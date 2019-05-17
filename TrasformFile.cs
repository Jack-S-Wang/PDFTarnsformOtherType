using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Spire.Pdf;

namespace PDFTransform
{
    public class TrasformFile
    {
        private MainWindow mainWindow;
        public void PdfTrasformOther(string fileName, string storeFileName, FileFormat fileFormat, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            PdfDocument document = new PdfDocument();
            document.LoadFromFile(fileName);
            using (MemoryStream stream = new MemoryStream())
            {
                document.SaveToStream(stream, fileFormat);
                document.Close();
                byte[] data = stream.ToArray();
                long len = stream.Length;
                saveFile(storeFileName, mainWindow, data, len);
            }
        }

        private static void saveFile(string storeFileName, MainWindow mainWindow, byte[] data, long len)
        {
            using (FileStream file = File.Create(storeFileName))
            {
                ParmData pdata = new ParmData();
                long everylen = 0;
                if (len % 100 == 0)
                {
                    everylen = len / 100;
                }
                else
                {
                    everylen = (len / 100) + 1;

                }
                mainWindow.progressSpeed.Myprogress.Dispatcher.Invoke(() =>
                {
                    mainWindow.progressSpeed.dataText.DataContext = pdata;
                    mainWindow.progressSpeed.percent.Text = "%";
                });
                int sendlen = 0;
                for (long i = 0; i <= 100; i++)
                {
                    if (data.Length - everylen >= 0)
                    {
                        sendlen = (int)everylen;
                    }
                    else
                    {
                        sendlen = data.Length;
                    }
                    file.Write(data, 0, (int)sendlen);
                    byte[] copyData = new byte[data.Length - sendlen];
                    Array.Copy(data, sendlen, copyData, 0, copyData.Length);
                    data = copyData;
                    mainWindow.progressSpeed.Myprogress.Dispatcher.Invoke(() =>
                    {
                        mainWindow.progressSpeed.Myprogress.Value = i;
                        pdata.ValueText = (int)i;
                    });
                    if (i == 100)
                    {
                        mainWindow.progressSpeed.Myprogress.Dispatcher.Invoke(() =>
                        {
                            mainWindow.progressSpeed.trasformText.Text = "";
                        });
                        SharParm.flage = false;
                    }
                }
            }
        }


    }
}
