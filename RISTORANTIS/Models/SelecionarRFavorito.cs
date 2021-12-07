using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Selecionar_R_Favoritos")]
    public partial class SelecionarRFavorito
    {
        [Key]
        [Column("ID_Cliente")]
        public int IdCliente { get; set; }

        [Key]
        [Column("ID_Restaurante")]
        public int IdRestaurante { get; set; }
        [Column("Notificacao_R")]
        public bool NotificacaoR { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.SelecionarRFavoritos))]
        public virtual Cliente IdClienteNavigation { get; set; }
        [ForeignKey(nameof(IdRestaurante))]
        [InverseProperty(nameof(Restaurante.SelecionarRFavoritos))]
        public virtual Restaurante IdRestauranteNavigation { get; set; }
    }
}
