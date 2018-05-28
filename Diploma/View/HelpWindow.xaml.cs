using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;

namespace Diploma.View
{
    /// <summary>
    /// Логика взаимодействия для HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();

	        Theory test1 = new Theory
	        {
		        Name = "ГОСТ",
		        Specificity = new List<Specificity>
		        {
			        new Specificity
			        {
				        Name = "ГОСТ 25192-2012 Бетоны – Классификация и общие технические требования",
				        Description = "http://www.gvozdem.ru/stroim-dom/kalkulyatory/calc_beton/file/25192-2012-gost.pdf"
					},

			        new Specificity
					{
				        Name = "ГОСТ 26633-91 Бетоны тяжелые и мелкозернистые",
				        Description = "http://www.gvozdem.ru/stroim-dom/kalkulyatory/calc_beton/file/26633-91-gost.pdf"
					},

			        new Specificity
			        {
				        Name = "ГОСТ 7473-2010 Смеси бетонные",
				        Description = "http://www.gvozdem.ru/stroim-dom/kalkulyatory/calc_beton/file/7473-2010-gost.pdf"
					}
				}
	        };

	        Theory test2 = new Theory
	        {
		        Name = "СНИП",
		        Specificity = new List<Specificity>
		        {
			        new Specificity
			        {
				        Name = "СНиП 82-02-95 Нормы расхода цемента при изготовление железобетонных изделий",
				        Description = "http://www.gvozdem.ru/stroim-dom/kalkulyatory/calc_beton/file/82-02-95-snip.pdf"
					}
		        }
			};
			Theory[] test = new Theory[] { test1, test2 };
			DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Theory[]));

	        using (FileStream fs = new FileStream("Test.json", FileMode.OpenOrCreate))
	        {
		        jsonFormatter.WriteObject(fs, test);
	        }

	        using (FileStream fs = new FileStream("Test.json", FileMode.OpenOrCreate))
	        {
		        Theory[] newtest = (Theory[])jsonFormatter.ReadObject(fs);

		        foreach (Theory p in newtest)
				{
					TreeViewItem item = new TreeViewItem {Header = p.Name};

					foreach (var specificity in p.Specificity)
					{
						TreeViewItem item2 = new TreeViewItem { Header = specificity.Name };
						item2.Items.Add(specificity.Description);
						item.Items.Add(item2);
					}
					Root.Items.Add(item);
				}
	        }
        }
    }

	[DataContract]
	public class Theory
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public List<Specificity> Specificity { get; set; }
	}

	public class Specificity
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
