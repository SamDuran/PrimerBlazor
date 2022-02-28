using System.ComponentModel.DataAnnotations;
public partial class ProductosDetalle
{
    [Key]
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public string? Descripcion { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Precio { get; set; }

    public ProductosDetalle()
    {
        Id = ProductoId = 0;
        Cantidad = Precio = 0;
    }
    public ProductosDetalle(int id, int productoId, string desc, decimal cant, decimal precio)
    {
        Id = id;
        ProductoId = productoId;
        Descripcion= desc;
        Cantidad = cant;
        Precio = precio;
    }
}