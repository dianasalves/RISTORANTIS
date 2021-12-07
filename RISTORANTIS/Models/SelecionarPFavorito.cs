using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Selecionar_P_Favoritos")]
    public partial class SelecionarPFavorito
    {
        [Key]
        [Column("ID_Cliente")]
        public int IdCliente { get; set; }
        [Key]
        [Column("ID_Prato")]
        public int IdPrato { get; set; }
        [Column("Notificacao_P")]
        public bool NotificacaoP { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.SelecionarPFavoritos))]
        public virtual Cliente IdClienteNavigation { get; set; }
        [ForeignKey(nameof(IdPrato))]
        [InverseProperty(nameof(PratoDoDium.SelecionarPFavoritos))]
        public virtual PratoDoDium IdPratoNavigation { get; set; }
    }
}
