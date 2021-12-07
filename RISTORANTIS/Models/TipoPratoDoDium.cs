using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Tipo_PratoDoDia")]
    public partial class TipoPratoDoDium
    {
        public TipoPratoDoDium()
        {
            PratoDoDia = new HashSet<PratoDoDium>();
        }

        [Key]
        [Column("ID_Tipo_P")]
        public int IdTipoP { get; set; }
        [Required]
        [Column("Nome_tipo_P")]
        [StringLength(50)]
        public string NomeTipoP { get; set; }

        [InverseProperty(nameof(PratoDoDium.TipoNavigation))]
        public virtual ICollection<PratoDoDium> PratoDoDia { get; set; }
    }
}
