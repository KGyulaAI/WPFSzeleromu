using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSzeleromu
{
    internal class Szeleromu
    {
        string eromuHelyszine;
        int egysegekSzama;
        int teljesitmeny;
        int kezdetiEv;

        public Szeleromu(string eromuHelyszine, int egysegekSzama, int teljesitmeny, int kezdetiEv)
        {
            this.eromuHelyszine = eromuHelyszine;
            this.egysegekSzama = egysegekSzama;
            this.teljesitmeny = teljesitmeny;
            this.kezdetiEv = kezdetiEv;
        }

        public string EromuHelyszine { get => eromuHelyszine; }
        public int EgysegekSzama { get => egysegekSzama; }
        public int Teljesitmeny { get => teljesitmeny; }
        public int KezdetiEv { get => kezdetiEv; }

        public char SzeleromuvekKategoriai(int teljesitmeny)
        {
            if (teljesitmeny >= 1000)
            {
                return 'A';
            }
            else if (teljesitmeny > 500 && teljesitmeny < 1000)
            {
                return 'B';
            }
            else
            {
                return 'C';
            }
        }
    }
}
