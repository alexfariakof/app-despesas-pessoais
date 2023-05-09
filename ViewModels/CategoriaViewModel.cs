using despesas_backend_api_net_core.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace despesas_backend_api_net_core.ViewModels
{
    public class CategoriaViewModel
    {
        [Key]
        [Display(Name = "Id")]
        [Required(ErrorMessage = "O código é requerido.")]
        public int Id { get; set; }

        [Display(Name = "Descricao")]
        [Required(ErrorMessage = "Uma descrição é requerida.")]
        public string Descricao { get; set; }


        [Display(Name = "IdUsuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Usuario")]
        public Usuario Usuario { get; set; }


        [Display(Name = "Categoria")]
        public Categoria Categoria { get; set; }

        [Display(Name = "IdTipoCategoria")]
        public int IdTipoCategoria { get; set; }
        [Required(ErrorMessage = "Um tipo de categoria é requerida.")]

        [Display(Name = "TipoCategoria")]
        public TipoCategoria TipoCategoria { get; set; }


    }
}
