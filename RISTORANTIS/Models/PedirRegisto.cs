using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Pedir_Registo")]
    public partial class PedirRegisto
    {
        [Key]
        [Column("ID_Pedir_Registo")]
        public int IdPedirRegisto { get; set; }
        [Column("Data_Pedido", TypeName = "date")]
        public DateTime DataPedido { get; set; }
        [Column("Data_Aceitacao", TypeName = "date")]
        public DateTime? DataAceitacao { get; set; }
        [StringLength(10)]
        public string? Resultado { get; set; }
        [Column("Motivo_Rejeicao")]
        [StringLength(100)]
        public string? MotivoRejeicao { get; set; }
        [Column("ID_Restaurante")]
        public int IdRestaurante { get; set; }
        [Column("ID_Administrador")]
        public int? IdAdministrador { get; set; }

        [ForeignKey(nameof(IdAdministrador))]
        [InverseProperty(nameof(Administrador.PedirRegistos))]
        public virtual Administrador IdAdministradorNavigation { get; set; }
        [ForeignKey(nameof(IdRestaurante))]
        [InverseProperty(nameof(Restaurante.PedirRegistos))]
        public virtual Restaurante IdRestauranteNavigation { get; set; }
    }
}
