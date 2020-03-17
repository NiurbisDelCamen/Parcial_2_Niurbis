using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Parcial2.Entidades
{
    public class LLamada
    {
        [Key]
        public int LlamadaId { get; set; }
        public String Descripcion { get; set; }

        [ForeignKey("LlamadaId")]
        public virtual List<LlamadaDetalle> Llamadas { get; set; }
        public LLamada()
        {
            LlamadaId = 0;
            Descripcion = string.Empty;
            Llamadas = new List<LlamadaDetalle>();
        }
    }
}
