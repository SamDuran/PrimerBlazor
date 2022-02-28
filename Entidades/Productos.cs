using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Samuel_Duran_Ap1_p1_.Entidades
{
    public partial class Productos
    {
        [Key]
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "Campo obligatorio. Se debe indicar la descripción.")]
        [MinLength(3, ErrorMessage ="La descripción debe tener al menos {1} caractéres.")]
        [MaxLength(35, ErrorMessage ="La descripción no debe pasar de {1} caractéres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio. Se debe indicar la existencia.")]
        [Range(0.1, int.MaxValue, ErrorMessage = "Se debe indicar la existencia del producto dentro de los rangos {1}/{2}.")]
        public double Existencia { get; set; }

        [Required(ErrorMessage = "El Campo \"Costo\"está vacío. Por favor indique un costo.")]
        [Range(1, int.MaxValue, ErrorMessage = "El costo debe estar dentro del rango permitido {1}/{2}.")]
        public double Costo { get; set; }

        public double ValorInventario { get; set; }
        [Required(ErrorMessage = "Campo obligatorio. Se debe indicar el precio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Se debe indicar el precio del producto dentro de los rangos {1}/{2}.")]
        public double Precio { get; set; }
        
        public int Ganancia { get; set; }

        [ForeignKey("ProductoId")]

        public virtual List<ProductosDetalle> ProductosDetalle {get;set;}

        public Productos()
        {
            ProductoId=0;
            ProductosDetalle = new List<ProductosDetalle>();
        }
    }
}