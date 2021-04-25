using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using Renci.SshNet;

namespace netconfClient
{
    /// <summary>
    ///     MainWindow penceresi ile ilgili tüm lojik işlemler.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Private Variables
        private string path { get; set; }
        private bool justGotOnline { get; set; }
        private bool resetButtonClicked { get; set; }

        /// <summary>
        ///     MainWindow penceresi için constructor fonksiyonu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            path = @"C:\Ulak";
            justGotOnline = false;
            resetButtonClicked = false;
            CheckXmlDirectory(path);
            WriteClientXml(path, "online", "", "", "", "");
            Task.Run(() => { CheckServerXml(); });
        }

        private async void CheckServerXml()
        {
            while(true)
            {
                await Task.Delay(100);

                try
                {
                    await serverStatusTextBox.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        serverStatusTextBox.Text = DetectServerStatus();
                    }));

                    if (DetectServerStatus() == "online" && (justGotOnline == false || resetButtonClicked == true))
                    {
                        justGotOnline = true;

                        if(resetButtonClicked == false)
                        {
                            await mainGrid.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                mainGrid.IsEnabled = true;
                            }));
                        }
                        else
                        {
                            resetButtonClicked = false;
                        }

                        await manNameTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            manNameTextBox.Text = ReadServerXml("manName");
                        }));
                        await uniqueIdTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            uniqueIdTextBox.Text = ReadServerXml("manUid");
                        }));
                        await modelNameTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            modelNameTextBox.Text = ReadServerXml("modelName");
                        }));
                        await serialTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            serialTextBox.Text = ReadServerXml("serialNo");
                        }));
                    }
                    else if (DetectServerStatus() == "offline")
                    {
                        justGotOnline = false;

                        await mainGrid.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            mainGrid.IsEnabled = false;
                        }));

                        await manNameTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            manNameTextBox.Text = "";
                        }));
                        await uniqueIdTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            uniqueIdTextBox.Text = "";
                        }));
                        await modelNameTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            modelNameTextBox.Text = "";
                        }));
                        await serialTextBox.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            serialTextBox.Text = "";
                        }));

                        WriteClientXml(path, "offline", "", "", "", "");
                    }
                }
                catch
                {
                    MessageBox.Show("Client XML Read Error !");
                }
            }
        }

        private string ReadServerXml(string searchTerm)
        {
            try
            {
                XDocument xmlFile = XDocument.Load(path + @"\serverConf.xml");
                var query = xmlFile.Elements("netconf").Elements(searchTerm).ToArray();
                return query[0].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string DetectServerStatus()
        {
            try
            {
                XDocument xmlFile = XDocument.Load(path + @"\server.xml");
                var query = xmlFile.Elements("netconf").Elements("serverStatus").ToArray();
                return query[0].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///     Uygulama kapanmadan önce son kontrolleri yapar.
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            WriteClientXml(path, "offline", "", "", "", "");
        }

        /// <summary>
        ///     XML haberleşmesi için gerekli klasörü oluşturur.
        /// </summary>
        private void CheckXmlDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch
            {
                MessageBox.Show("Directory Error!");
            }
        }

        /// <summary>
        ///     XML haberleşmesi için gerekli istemci dosyalarını oluşturur.
        /// </summary>
        private void WriteClientXml(string path, string status, string manName, string uniqueId, string modelName, string serialNumber)
        {
            try
            {
                XDocument doc = new XDocument(new XElement("netconf",
                                                new XElement("clientStatus", status), new XElement("clientRequest",
                                                                                new XElement("manName", manName), new XElement("uniqueId", uniqueId), new XElement("modelName", modelName), new XElement("serialNumber", serialNumber))));
                doc.Save(path + @"\client.xml");
            }
            catch
            {
                MessageBox.Show("XML File Error!");
            }
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            resetButtonClicked = true;
            mainGrid.IsEnabled = false;
            Task.Run(() => { EnableMainGrid(); });
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            WriteClientXml(path, "online", manNameTextBox.Text, uniqueIdTextBox.Text, modelNameTextBox.Text, serialTextBox.Text);
            mainGrid.IsEnabled = false;
            Task.Run(() => { EnableMainGrid(); });
        }

        private async void EnableMainGrid()
        {
            await Task.Delay(200);

            await mainGrid.Dispatcher.BeginInvoke(new Action(() =>
            {
                mainGrid.IsEnabled = true;
            }));
        }
    }
}
