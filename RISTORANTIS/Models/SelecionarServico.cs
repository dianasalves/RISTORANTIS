using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Selecionar_Servico")]
    public partial class SelecionarServico
    {
        [Key]
        public int ID_SelectServico { get; set; }
        [Column("ID_Servico")]
        public int IdServico { get; set; }
        [Column("ID_Restaurante")]
        public int IdRestaurante { get; set; }

        [ForeignKey(nameof(IdRestaurante))]
        public virtual Restaurante IdRestauranteNavigation { get; set; }
        [ForeignKey(nameof(IdServico))]
        public virtual TipoServico IdServicoNavigation { get; set; }
    }
}
