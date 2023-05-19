using System;
using System.Collections;
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

namespace WPFSzeleromu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Szeleromu> szeleromuLista = new List<Szeleromu>();
        public MainWindow()
        {
            InitializeComponent();
            AdatokBeolvasasa();
            dtgAdatok.ItemsSource = szeleromuLista;
        }

        private void AdatokBeolvasasa()
        {
            StreamReader streamReader = new StreamReader("szeleromu.txt");
            while (!streamReader.EndOfStream)
            {
                string[] szeleromuAdatai = streamReader.ReadLine().Split(";");
                szeleromuLista.Add(new Szeleromu(szeleromuAdatai[0], int.Parse(szeleromuAdatai[1]), int.Parse(szeleromuAdatai[2]), int.Parse(szeleromuAdatai[3])));
            }
            streamReader.Close();
        }

        private void btnSzeleromuvekSzama_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"{szeleromuLista.Count} szélerőmű adata van az állományban.");
        }

        private void btnAtlagTeljesitmeny_Click(object sender, RoutedEventArgs e)
        {
            List<int> eromuvekTeljesitmenye2010ben = new List<int>();
            foreach (Szeleromu szeleromu in szeleromuLista)
            {
                if (szeleromu.KezdetiEv == 2010)
                {
                    eromuvekTeljesitmenye2010ben.Add(szeleromu.Teljesitmeny);
                }
            }
            MessageBox.Show($"A 2010-ben indult szélerőművek átlagteljesítménye: {Math.Round(eromuvekTeljesitmenye2010ben.Average(), 2)} W");
        }

        private void btnAdatokTelepulesrol_Click(object sender, RoutedEventArgs e)
        {
            lbTelepulesSzeleromuvei.Items.Clear();
            string bekertTelepules = txtTelepules.Text;
            foreach (Szeleromu szeleromu in szeleromuLista)
            {
                if (szeleromu.EromuHelyszine.ToLower() == bekertTelepules.ToLower())
                {
                    lbTelepulesSzeleromuvei.Items.Add($"{szeleromu.EgysegekSzama};{szeleromu.Teljesitmeny};{szeleromu.KezdetiEv}");
                }
            }
            lblSzeleromuvekSzamaTelepulesen.Content = $"Szélerőművek száma a listában: {lbTelepulesSzeleromuvei.Items.Count}";
        }

        private void btnLegjobbEromu_Click(object sender, RoutedEventArgs e)
        {
            int legjobbTeljesitmeny = 0;
            int legjobbTeljesitmenyIndexe = 0;
            for (int index = 0; index < szeleromuLista.Count; index++)
            {
                if (szeleromuLista[index].Teljesitmeny > legjobbTeljesitmeny)
                {
                    legjobbTeljesitmeny = szeleromuLista[index].Teljesitmeny;
                    legjobbTeljesitmenyIndexe = index;
                }
            }

            MessageBox.Show($"Legjobb teljesítményű erőmű adatai: {szeleromuLista[legjobbTeljesitmenyIndexe].EromuHelyszine} helyszín, " +
                            $"{szeleromuLista[legjobbTeljesitmenyIndexe].Teljesitmeny} W teljesítmény, " +
                            $"{szeleromuLista[legjobbTeljesitmenyIndexe].KezdetiEv} kezdeti év");
        }

        private void btnJelentesKeszitese_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter streamWriter = new StreamWriter("jelentes.txt");
            for (int index = 0; index < szeleromuLista.Count; index++)
            {
                streamWriter.WriteLine($"{szeleromuLista[index].EromuHelyszine}," +
                                       $"{szeleromuLista[index].EgysegekSzama}," +
                                       $"{szeleromuLista[index].Teljesitmeny}," +
                                       $"{szeleromuLista[index].KezdetiEv}," +
                                       $"{szeleromuLista[index].SzeleromuvekKategoriai(szeleromuLista[index].Teljesitmeny)}");
            }
            streamWriter.Close();
        }
    }
}
