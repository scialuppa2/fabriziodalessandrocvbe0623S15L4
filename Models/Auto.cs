using System;

namespace NomeProgetto.Models
{
    public class Auto
    {
        public int ID { get; set; }
        public string Marca { get; set; }
        public string Modello { get; set; }

        // Proprietà calcolata per visualizzare Marca e Modello insieme
        public string MarcaModello
        {
            get { return $"{Marca} {Modello}"; }
        }
    }
}
