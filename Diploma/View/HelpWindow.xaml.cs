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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        private XElement xtv;
        private XElement node;
        private XElement attribute;

        public HelpWindow()
        {
            InitializeComponent();

            string uri = @"pack://application:,,,/Help/XMLHelp.xml";
            TreeView tvOld = Root.Children.OfType<TreeView>().FirstOrDefault();
            if (tvOld != null)
            {
                Root.Children.Remove(tvOld);
            }
            ReadFiles();
            XElement target = GetXElement(uri);

            foreach (XElement el in target.Elements())
            {
                XElement xaml = new XElement(node);
                Process(el, xaml);
                xtv.Add(xaml);
            }
            string tvString = xtv.ToString();
            ParserContext context = new ParserContext();
            context.XmlnsDictionary.Add("", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            context.XmlnsDictionary.Add("x", "http://schemas.microsoft.com/winfx/2006/xaml");
            TreeView tv = (TreeView)XamlReader.Parse(tvString, context);
            Root.Children.Add(tv);
        }

        private void ReadFiles()
        {
            string filename = System.AppDomain.CurrentDomain.BaseDirectory;

            xtv = GetXElement(@"pack://application:,,,/Help/treeview.txt");
            node = GetXElement(@"pack://application:,,,/Help/node.txt");
            attribute = GetXElement(@"pack://application:,,,/Help/attribute.txt");
        }

        private XElement GetXElement(string uri)
        {
            XDocument xmlDoc = new XDocument();
            var iob = new Uri(uri, UriKind.Absolute);
            var xmltxt = Application.GetContentStream(iob);
            string elfull = new StreamReader(xmltxt.Stream).ReadToEnd();
            xmlDoc = XDocument.Parse(elfull);
            return xmlDoc.Root;
        }

        private void Process(XElement xml, XElement tv)
        {
            SetNameValue(tv, xml.Name.ToString(), "");
            if (xml.HasAttributes)
            {
                foreach (XAttribute attr in xml.Attributes())
                {
                    XElement xa = new XElement(attribute);
                    SetNameValue(xa, attr.Name.ToString(), attr.Value.ToString());
                    tv.Add(xa);
                }
            }
            if (xml.HasElements)
            {
                // Not a leaf node - process children
                foreach (var childXml in xml.Elements())
                {
                    XElement child = new XElement(node);
                    Process(childXml, child);
                    tv.Add(child);
                }
            }
            else
            {
                // Leaf node 
                SetNameValue(tv, xml.Name.ToString(), xml.Value.ToString());
            }
        }

        private void SetNameValue(XElement xaml, string name, string value)
        {
            var nameEl = xaml.Descendants("TextBlock")
                            .Single();
            nameEl.SetAttributeValue("Text", name);
            var valEl = xaml.Descendants("Label")
                           .Single();
            valEl.SetAttributeValue("Content", value);
        }
    }
}
