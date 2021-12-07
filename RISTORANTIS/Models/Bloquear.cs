using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Bloquear")]
    public partial class Bloquear
    {
        [Key]
        [Column("Data_Bloquear", TypeName = "date")]
        public DateTime DataBloquear { get; set; }
        [Required]
        [Column("Motivo_Bloqueio")]
        [StringLength(100)]
        public string MotivoBloqueio { get; set; }
        [Column("Data_Desbloqueio", TypeName = "date")]
        public DateTime? DataDesbloqueio { get; set; }
        [Key]
        [Column("ID_Administrador")]
        public int IdAdministrador { get; set; }
        [Key]
        [Column("ID_Utilizador")]
        public int IdUtilizador { get; set; }

        [ForeignKey(nameof(IdAdministrador))]
        [InverseProperty(nameof(Administrador.Bloquears))]
        public virtual Administrador IdAdministradorNavigation { get; set; }
        [ForeignKey(nameof(IdUtilizador))]
        [InverseProperty(nameof(Utilizador.Bloquears))]
        public virtual Utilizador IdUtilizadorNavigation { get; set; }
    }
}
