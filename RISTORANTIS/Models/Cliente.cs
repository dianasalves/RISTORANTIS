using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            SelecionarPFavoritos = new HashSet<SelecionarPFavorito>();
            SelecionarRFavoritos = new HashSet<SelecionarRFavorito>();
        }

        [Key]
        [Column("ID_Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Utilizador.Cliente))]
        public virtual Utilizador IdClienteNavigation { get; set; }
        [InverseProperty(nameof(SelecionarPFavorito.IdClienteNavigation))]
        public virtual ICollection<SelecionarPFavorito> SelecionarPFavoritos { get; set; }
        [InverseProperty(nameof(SelecionarRFavorito.IdClienteNavigation))]
        public virtual ICollection<SelecionarRFavorito> SelecionarRFavoritos { get; set; }
    }
}
