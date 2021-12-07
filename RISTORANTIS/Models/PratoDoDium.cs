using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    public partial class PratoDoDium
    {

        public PratoDoDium()
        {
            Registars = new HashSet<Registar>();
            SelecionarPFavoritos = new HashSet<SelecionarPFavorito>();
        }

        [Key]
        [Column("ID_Prato")]
        public int IdPrato { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        public int Tipo { get; set; }

        [ForeignKey(nameof(Tipo))]
        [InverseProperty(nameof(TipoPratoDoDium.PratoDoDia))]
        public virtual TipoPratoDoDium TipoNavigation { get; set; }
        [InverseProperty(nameof(Registar.IdPratoDoDiaNavigation))]
        public virtual ICollection<Registar> Registars { get; set; }
        [InverseProperty(nameof(SelecionarPFavorito.IdPratoNavigation))]
        public virtual ICollection<SelecionarPFavorito> SelecionarPFavoritos { get; set; }
    }
}
