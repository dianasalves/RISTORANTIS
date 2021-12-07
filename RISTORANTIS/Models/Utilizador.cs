using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Utilizador")]
    public partial class Utilizador
    {
        public Utilizador()
        {
            Bloquears = new HashSet<Bloquear>();
        }

        [Key]
        [Column("ID_Utilizador")]
        public int IdUtilizador { get; set; }
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required]
        [StringLength(100)]
        public virtual string Email { get; set; }
        [Required]
        [StringLength(100)]
        public virtual string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(10)]
        public string Estado { get; set; }

        //[Required]
        [StringLength(100)]
        public string ConfEmail { get; set; }

        [InverseProperty("IdClienteNavigation")]
        public virtual Cliente Cliente { get; set; }
        [InverseProperty("IdRestauranteNavigation")]
        public virtual Restaurante Restaurante { get; set; }
        [InverseProperty(nameof(Bloquear.IdUtilizadorNavigation))]
        public virtual ICollection<Bloquear> Bloquears { get; set; }
    }
}

//    public enum EstadosPossiveis 
//	{

//        Em_Espera, //value = 0
//        Registado, //value = 1
//        Rejeitado  //value = 2
//    }
//}
