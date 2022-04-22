using System.ComponentModel.DataAnnotations;

namespace peliculaAPI.Models
{
    public class CreatePelicula
    {
        [Required(ErrorMessage = "El nombre debe estar especificado")]
        public string Pel_Nombre { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "El campo debe ser numerico")]
        public int Pel_Duracion { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "El campo debe ser numerico")]
        [Required(ErrorMessage = "El director debe estar especificado")]
        public int Pel_Genero { get; set; }
        [Required(ErrorMessage = "El director debe estar especificado")]
        public string Pel_Director { get; set; }
        [Required(ErrorMessage = "El resumen debe estar especificado")]
        public string Pel_Resumen { get; set; }
    }
}
