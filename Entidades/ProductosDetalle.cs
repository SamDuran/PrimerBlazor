using System.ComponentModel.DataAnnotations;
public partial class ProductosDetalle
{
    [Key]
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string? Descripcion { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Precio { get; set; }

    public ProductosDetalle( string descripcion, decimal cantidad, decimal precio)
    {
        Descripcion= descripcion;
        Cantidad = cantidad;
        Precio = precio;
    }
}