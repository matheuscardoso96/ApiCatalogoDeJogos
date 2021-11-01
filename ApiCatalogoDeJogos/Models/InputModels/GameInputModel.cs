using System.ComponentModel.DataAnnotations;

namespace ApiCatalogoDeJogos.Models.InputModels
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength =3,ErrorMessage = "The game name must be between 6 and 100 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The developer name must be between 6 and 100 characters.")]
        public string Developer { get; set; }
        public double Price { get; set; }
    }
}
