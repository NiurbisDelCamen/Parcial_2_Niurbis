using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Parcial2.Entidades
{
    public class LlamadaDetalle
    {
        [Key]
        public int Id { get; set; }
        public int LlamadaId { get; set; }
        public string Problema { get; set; }
        public string Solucion { get; set; }

        public LlamadaDetalle()
        {
            Id = 0;
           
            Problema = string.Empty;
            Solucion = string.Empty;
        }
        public LlamadaDetalle(int lLamadaId, string problema, string solucion)
        {
            Id = 0;
            LlamadaId = lLamadaId;
            Problema = problema;
            Solucion = solucion;
        }
    }
}
