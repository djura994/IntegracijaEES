using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using TelventDMS.Services.NetworkModelService.TestClient.Tests;

namespace UIClient
{
    /// <summary>
    public partial class MainWindow : Window
    {
        public Dictionary<string, string> listBox1Pairs = new Dictionary<string, string>();
        public Dictionary<string, string> listBox2Pairs = new Dictionary<string, string>();
        public Dictionary<string, string> listBox3Pairs = new Dictionary<string, string>();


        public MainWindow()
        {
            InitializeComponent();
            typeComboBox.ItemsSource = Enum.GetValues(typeof(DMSType)).Cast<DMSType>().Where(x => x != DMSType.MASK_TYPE).ToList();
            typeExtentcomboBox.ItemsSource = Enum.GetValues(typeof(DMSType)).Cast<DMSType>().Where(x => x != DMSType.MASK_TYPE).ToList();
            typeGetRelatedValuescomboBox.ItemsSource = Enum.GetValues(typeof(DMSType)).Cast<DMSType>().Where(x => x != DMSType.MASK_TYPE).ToList();
        }

        private void typeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestGda tgda = new TestGda();
            gidComboBox.ItemsSource = tgda.GetExtentValues(InputModelCode(typeComboBox.SelectedItem.ToString()));
            gidComboBox.SelectedItem = null;
            listBox1.SelectedItem = null;
            listBox1.Items.Clear();
            listBox1Pairs.Clear();
            richTextBox.Document.Blocks.Clear();
        }

        private void gidComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gidComboBox.SelectedItem == null)
            {
                return;
            }
            TestGda tgda = new TestGda();
            List<List<string>> path = tgda.GetValuesAndPath(Convert.ToInt64(gidComboBox.SelectedItem.ToString()));
            string xmlString = System.IO.File.ReadAllText(path[0][0]);
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(new Paragraph(new Run(xmlString)));
            listBox1.SelectedItem = null;
            listBox1.Items.Clear();
            listBox1Pairs.Clear();
            foreach (var item in path[1])
            {
                string[] propPair = item.Split('~');
                listBox1.Items.Add(propPair[0]);
                listBox1Pairs.Add(propPair[0], propPair[1]);
            }
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                return;
            }
            richTextBox.Document.Blocks.Clear();
            foreach (var item in listBox1.SelectedItems)
            {
                richTextBox.Document.Blocks.Add(new Paragraph(new Run(listBox1Pairs[item.ToString()])));
            }
        }

        private void GetExtentValuescomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestGda tgda = new TestGda();
            List<List<string>> path = tgda.GetExtentValuesAndPath(InputModelCode(typeExtentcomboBox.SelectedItem.ToString()));
            string xmlString = System.IO.File.ReadAllText(path[0][0]);
            richTextBox2.Document.Blocks.Clear();
            richTextBox2.Document.Blocks.Add(new Paragraph(new Run(xmlString)));
            listBox2.SelectedItem = null;
            listBox2.Items.Clear();
            listBox2Pairs.Clear();
            foreach (var item in path[1])
            {
                string[] propPair = item.Split('~');
                if (listBox2Pairs.Keys.Contains(propPair[0]))
                {
                    listBox2Pairs[propPair[0]] += "\n" + propPair[1];
                }
                else
                {
                    listBox2.Items.Add(propPair[0]);
                    listBox2Pairs.Add(propPair[0], propPair[1]);
                }
            }
        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox2.SelectedItem == null)
            {
                return;
            }
            richTextBox2.Document.Blocks.Clear();
            foreach (var item in listBox2.SelectedItems)
            {
                richTextBox2.Document.Blocks.Add(new Paragraph(new Run(listBox2Pairs[item.ToString()])));
            }
        }

        private void typeGetRelatedValuescomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TestGda tgda = new TestGda();
            gidGetRelatedValuescomboBox.ItemsSource = tgda.GetExtentValues(InputModelCode(typeGetRelatedValuescomboBox.SelectedItem.ToString()));
            gidGetRelatedValuescomboBox.SelectedItem = null;
            refGetRelatedValuescomboBox.SelectedItem = null;
            refGetRelatedValuescomboBox.ItemsSource = null;
            refTypeGetRelatedValuescomboBox.SelectedItem = null;
            refTypeGetRelatedValuescomboBox.ItemsSource = null;
            refTypeGetRelatedValuescomboBox.Items.Clear();
            listBox3.SelectedItem = null;
            listBox3.Items.Clear();
            listBox3Pairs.Clear();
            richTextBox3.Document.Blocks.Clear();
        }

        private void gidGetRelatedValuescomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (gidGetRelatedValuescomboBox.SelectedItem == null)
            {
                return;
            }
            TestGda tgda = new TestGda();
            refGetRelatedValuescomboBox.ItemsSource = tgda.GetReferences(Convert.ToInt64(gidGetRelatedValuescomboBox.SelectedItem.ToString()));
            refGetRelatedValuescomboBox.SelectedItem = null;
            refTypeGetRelatedValuescomboBox.SelectedItem = null;
            refTypeGetRelatedValuescomboBox.ItemsSource = null;
            refTypeGetRelatedValuescomboBox.Items.Clear();
            listBox3.SelectedItem = null;
            listBox3.Items.Clear();
            listBox3Pairs.Clear();
            richTextBox3.Document.Blocks.Clear();
        }

        private void refGetRelatedValuescomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (refGetRelatedValuescomboBox.SelectedItem == null)
            {
                return;
            }
            TestGda tgda = new TestGda();
            List<List<string>> path = tgda.GetRelatedValuesAndPath(Convert.ToInt64(gidGetRelatedValuescomboBox.SelectedItem.ToString()), new Association(refGetRelatedValuescomboBox.SelectedItem.ToString(), "0"));
            string xmlString = System.IO.File.ReadAllText(path[0][0]);
            richTextBox3.Document.Blocks.Clear();
            richTextBox3.Document.Blocks.Add(new Paragraph(new Run(xmlString)));
            listBox3.SelectedItem = null;
            listBox3.Items.Clear();
            listBox3Pairs.Clear();
            foreach (var item in path[1])
            {
                string[] propPair = item.Split('~');
                if (listBox3Pairs.Keys.Contains(propPair[0]))
                {
                    listBox3Pairs[propPair[0]] += "\n" + propPair[1];
                }
                else
                {
                    listBox3.Items.Add(propPair[0]);
                    listBox3Pairs.Add(propPair[0], propPair[1]);
                }
            }
            refTypeGetRelatedValuescomboBox.SelectedItem = null;
            refTypeGetRelatedValuescomboBox.ItemsSource = null;
            refTypeGetRelatedValuescomboBox.Items.Clear();
            refTypeGetRelatedValuescomboBox.ItemsSource = Enum.GetValues(typeof(DMSType)).Cast<DMSType>().Where(x => x != DMSType.MASK_TYPE).ToList();
        }

        private void refTypeGetRelatedValuescomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (refTypeGetRelatedValuescomboBox.SelectedItem == null)
            {
                return;
            }
            TestGda tgda = new TestGda();
            List<List<string>> path = tgda.GetRelatedValuesAndPath(Convert.ToInt64(gidGetRelatedValuescomboBox.SelectedItem.ToString()), new Association(refGetRelatedValuescomboBox.SelectedItem.ToString(), refTypeGetRelatedValuescomboBox.SelectedItem.ToString()));
            string xmlString = System.IO.File.ReadAllText(path[0][0]);
            richTextBox3.Document.Blocks.Clear();
            richTextBox3.Document.Blocks.Add(new Paragraph(new Run(xmlString)));
            listBox3.SelectedItem = null;
            listBox3.Items.Clear();
            listBox3Pairs.Clear();
            foreach (var item in path[1])
            {
                string[] propPair = item.Split('~');
                if (listBox3Pairs.Keys.Contains(propPair[0]))
                {
                    listBox3Pairs[propPair[0]] += "\n" + propPair[1];
                }
                else
                {
                    listBox3.Items.Add(propPair[0]);
                    listBox3Pairs.Add(propPair[0], propPair[1]);
                }
            }
        }

        private void listBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox3.SelectedItem == null)
            {
                return;
            }
            richTextBox3.Document.Blocks.Clear();
            foreach (var item in listBox3.SelectedItems)
            {
                richTextBox3.Document.Blocks.Add(new Paragraph(new Run(listBox3Pairs[item.ToString()])));
            }
        }






        private static ModelCode InputModelCode(string type)
        {
            CommonTrace.WriteTrace(CommonTrace.TraceVerbose, "Entering Model Code started.");

            try
            {
                Console.Write("Enter Model Code: ");
                string userModelCode = type;
                ModelCode modelCode = 0;

                if (!ModelCodeHelper.GetModelCodeFromString(userModelCode, out modelCode))
                {
                    if (userModelCode.StartsWith("0x", StringComparison.Ordinal))
                    {
                        modelCode = (ModelCode)long.Parse(userModelCode.Substring(2), System.Globalization.NumberStyles.HexNumber);
                    }
                    else
                    {
                        modelCode = (ModelCode)long.Parse(userModelCode);
                    }
                }

                return modelCode;
            }
            catch (Exception ex)
            {
                string message = string.Format("Entering Model Code failed. {0}", ex);
                CommonTrace.WriteTrace(CommonTrace.TraceError, message);
                Console.WriteLine(message);
                throw ex;
            }
        }

        private void tabControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            richTextBox.Height = e.NewSize.Height;
            richTextBox.Width = e.NewSize.Width;
            richTextBox2.Height = e.NewSize.Height;
            richTextBox2.Width = e.NewSize.Width;
            richTextBox3.Height = e.NewSize.Height;
            richTextBox3.Width = e.NewSize.Width;
        }

        private void mainw_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            tabControl.Height = e.NewSize.Height;
            tabControl.Width = e.NewSize.Width;
            listBox1.Height = e.NewSize.Width;
            listBox2.Height = e.NewSize.Width;
            listBox3.Height = e.NewSize.Width;

        }
    }

}
