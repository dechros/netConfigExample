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
using System.Xml.Linq;

namespace nrtconfServer
{
    /// <summary>
    ///     MainWindow penceresi ile ilgili tüm lojik işlemler.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Private Variables
        private string path { get; set; }

        /// <summary>
        ///     MainWindow penceresi için constructor fonksiyonu.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            path = @"C:\Ulak";
            CheckXmlDirectory(path);
            WriteServerXml(path, "online");
            Task.Run(() => { CheckClientXml(); });
        }

        /// <summary>
        ///     Uygulama kapanmadan önce son kontrolleri yapar.
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            WriteServerXml(path, "offline");
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
                MessageBox.Show("Directory Create Error!");
            }
        }

        /// <summary>
        ///     XML haberleşmesi için gerekli istemci dosyalarını oluşturur.
        /// </summary>
        private void WriteServerXml(string path, string status)
        {
            try
            {
                XDocument doc = new XDocument(new XElement("netconf",
                                                new XElement("serverStatus", status)));

                doc.Save(path + @"\server.xml");

                doc = new XDocument(new XElement("netconf",
                                        new XElement("manName", "Netas"), new XElement("manUid", "NNTM31"), new XElement("modelName", "ULAKMacroENB"), new XElement("serialNo", "0K7558")));
                doc.Save(path + @"\serverConf.xml");
            }
            catch
            {
                MessageBox.Show("Config XML File Create Error!");
            }
        }

        /// <summary>
        ///     Netconf client isteklerini handle eder.
        /// </summary>
        private async void CheckClientXml()
        {
            while(true)
            {
                await Task.Delay(100);

                try
                {
                    await clientStatusTextBox.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        clientStatusTextBox.Text = DetectClientStatus();
                    }));

                    if (DetectClientStatus() == "online")
                    {
                        DetectClientRequest();
                    }
                    ReadXmlConfigAndWriteTextBoxes();
                }
                catch 
                {
                    MessageBox.Show("Client XML Read Error !");
                }
            }
        }

        private string DetectClientStatus()
        {
            try
            {
                XDocument xmlFile = XDocument.Load(path + @"\client.xml");
                var query = xmlFile.Elements("netconf").Elements("clientStatus").ToArray();
                return query[0].Value.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DetectClientRequest()
        {
            string manName;
            string uniqueId;
            string modelName;
            string serialNumber;

            XDocument xmlFile = XDocument.Load(path + @"\client.xml");

            var query = xmlFile.Elements("netconf").Elements("clientRequest").Elements("manName").ToArray();

            if (query[0].Value.ToString() != null)
            {
                manName = query[0].Value.ToString();
            }
            else
            {
                manName = "";
            }

            query = xmlFile.Elements("netconf").Elements("clientRequest").Elements("uniqueId").ToArray();
            if (query[0].Value.ToString() != null)
            {
                uniqueId = query[0].Value.ToString();
            }
            else
            {
                uniqueId = "";
            }

            query = xmlFile.Elements("netconf").Elements("clientRequest").Elements("modelName").ToArray();
            if (query[0].Value.ToString() != null)
            {
                modelName = query[0].Value.ToString();
            }
            else
            {
                modelName = "";
            }

            query = xmlFile.Elements("netconf").Elements("clientRequest").Elements("serialNumber").ToArray();
            if (query[0].Value.ToString() != null)
            {
                serialNumber = query[0].Value.ToString();
            }
            else
            {
                serialNumber = "";
            }

            if (manName != "" && uniqueId != "" && modelName != "" && serialNumber != "")
            {
                try
                {
                    XDocument doc = new XDocument(new XElement("netconf",
                                            new XElement("manName", manName), new XElement("manUid", uniqueId), new XElement("modelName", modelName), new XElement("serialNo", serialNumber)));
                    doc.Save(path + @"\serverConf.xml");
                }
                catch
                {
                    throw;
                }
            }
        }

        private async void ReadXmlConfigAndWriteTextBoxes()
        {
            try
            {
                XDocument xmlFile = XDocument.Load(path + @"\serverConf.xml");
                
                var query = xmlFile.Elements("netconf").Elements("manName").ToArray();
                await manNameTextBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    manNameTextBox.Text = query[0].Value.ToString();
                }));

                query = xmlFile.Elements("netconf").Elements("manUid").ToArray();
                await uniqueIdTextBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    uniqueIdTextBox.Text = query[0].Value.ToString();
                }));

                query = xmlFile.Elements("netconf").Elements("modelName").ToArray();
                await modelNameTextBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    modelNameTextBox.Text = query[0].Value.ToString();
                }));

                query = xmlFile.Elements("netconf").Elements("serialNo").ToArray();
                await serialTextBox.Dispatcher.BeginInvoke(new Action(() =>
                {
                    serialTextBox.Text = query[0].Value.ToString();
                }));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
