namespace MiProyectoMVC.Models.Events;

public class Event
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
    public string? Ubicacion { get; set; }
    public string? ImagenUrl { get; set; }
    public DateTime FechaCreacion { get; set; }
    public string? Status { get; set; }
}