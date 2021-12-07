using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Administrador")]
    public partial class Administrador
    {
        public Administrador()
        {
            Bloquears = new HashSet<Bloquear>();
            InverseIdCriadorNavigation = new HashSet<Administrador>();
            PedirRegistos = new HashSet<PedirRegisto>();
        }

        [Key]
        [Column("ID_Administrador")]
        public int IdAdministrador { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Column("ID_CRIADOR")]
        public int? IdCriador { get; set; }

        [ForeignKey(nameof(IdCriador))]
        [InverseProperty(nameof(Administrador.InverseIdCriadorNavigation))]
        public virtual Administrador IdCriadorNavigation { get; set; }
        [InverseProperty(nameof(Bloquear.IdAdministradorNavigation))]
        public virtual ICollection<Bloquear> Bloquears { get; set; }
        [InverseProperty(nameof(Administrador.IdCriadorNavigation))]
        public virtual ICollection<Administrador> InverseIdCriadorNavigation { get; set; }
        [InverseProperty(nameof(PedirRegisto.IdAdministradorNavigation))]
        public virtual ICollection<PedirRegisto> PedirRegistos { get; set; }
    }
}
