using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Web;

#nullable disable

namespace RISTORANTIS.Models
{
    [Table("Restaurante")]
    public partial class Restaurante
    {

        public Restaurante()
        {
            PedirRegistos = new HashSet<PedirRegisto>();
            Registars = new HashSet<Registar>();
            SelecionarRFavoritos = new HashSet<SelecionarRFavorito>();
        }

        [Key]
        [Column("ID_Restaurante")]
        public int IdRestaurante { get; set; }
        [Required]
        [StringLength(9)]
        public string Telefone { get; set; }
        [Required]
        [Column("Localizacao_GPS")]
        [Display(Name = "Localização GPS")]
        [StringLength(100)]
        public string LocalizacaoGps { get; set; }
        [Required]
        [Column("Endereco_Codigo_Postal")]
        [Display(Name = "Código Postal")]
        [StringLength(8)]
        public string EnderecoCodigoPostal { get; set; }
        [Required]
        [Column("Endereco_Morada")]
        [Display(Name = "Morada")]
        [StringLength(50)]
        public string EnderecoMorada { get; set; }
        [Required]
        [Column("Endereco_Localidade")]
        [Display(Name = "Localidade")]
        [StringLength(50)]
        public string EnderecoLocalidade { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Horário")]
        public string Horario { get; set; }

        //[StringLength(100)]
        [RegularExpression(@"^.+\.([jJ][pP][gG])$", ErrorMessage = "Aceita apenas as seguintes extensões: .jpg")]
        public string Fotografia { get; set; }

        [Required]
        [Column("Dia_Descanso")]
        [Display(Name = "Dia de Descanso")]
        [StringLength(50)]
        public string DiaDescanso { get; set; }

        [Required]
        [StringLength(10)]
        public string EstadoR { get; set; }

        [ForeignKey(nameof(IdRestaurante))]
        [InverseProperty(nameof(Utilizador.Restaurante))]
        public virtual Utilizador IdRestauranteNavigation { get; set; }
        [InverseProperty(nameof(PedirRegisto.IdRestauranteNavigation))]
        public virtual ICollection<PedirRegisto> PedirRegistos { get; set; }
        [InverseProperty(nameof(Registar.IdRestauranteNavigation))]
        public virtual ICollection<Registar> Registars { get; set; }
        [InverseProperty(nameof(SelecionarRFavorito.IdRestauranteNavigation))]
        public virtual ICollection<SelecionarRFavorito> SelecionarRFavoritos { get; set; }
    }
}
