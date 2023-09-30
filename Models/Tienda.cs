public class Tienda
{
    public string _id { get; set; }
    public string dueño { get; set; }
    public string d_email { get; set; }
    public List<Pedidos> pedidos { get; set; } // Agrega esta propiedad
}


public class Pedidos
{
    public string correo_cliente { get; set; }
    public string nombre_producto { get; set; }
    public string precio_producto { get; set; }
    public string cantidad { get; set; }
    // Agrega más propiedades según tu estructura de datos
}