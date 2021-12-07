using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Registar")]
    public partial class Registar
    {
        [Key]
        public int ID_Registo { get; set; }


        [Column("Data_Disponibilidade", TypeName = "datetime")]
        public DateTime DataDisponibilidade { get; set; }

        [StringLength(1000)]
        public string Fotografia { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Preco { get; set; }
        
        [Column("ID_Restaurante")]
        public int IdRestaurante { get; set; }
        
        [Column("ID_PratoDoDia")]
        public int IdPratoDoDia { get; set; }

        [ForeignKey(nameof(IdPratoDoDia))]
        [InverseProperty(nameof(PratoDoDium.Registars))]
        public virtual PratoDoDium IdPratoDoDiaNavigation { get; set; }

        [ForeignKey(nameof(IdRestaurante))]
        [InverseProperty(nameof(Restaurante.Registars))]
        public virtual Restaurante IdRestauranteNavigation { get; set; }

        public virtual ICollection<SelecionarPFavorito> SelecionarPFavoritos { get; set; }
    }
}
