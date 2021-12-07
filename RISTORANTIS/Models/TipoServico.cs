using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Tipo_Servico")]
    public partial class TipoServico
    {
        [Key]
        [Column("ID_Servico")]
        public int IdServico { get; set; }
        [Required]
        [Column("Nome_Tipo_S")]
        [StringLength(50)]
        public string NomeTipoS { get; set; }

        public bool Selecionado { get; set; }

        public List<TipoServico> ts { get; set; }

    }
}
