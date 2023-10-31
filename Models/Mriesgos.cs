
public class Procesos
{
     public string m_nombre { get; set; }
    public List<Mriesgos> m_riesgos { get; set; } // Agrega esta propiedad
}
public class Mriesgos
{
    public string nombre { get; set; }
    public string impacto_desc { get; set; }
    public string impacto_num { get; set; }
    public string impacto_porc { get; set; }
    public string probabilidad_desc { get; set; }
    public string probabilidad_num { get; set; }
    public string probabilidad_porc { get; set; }
    public string calificacion { get; set; }
    public string riesgo { get; set; }
    public string proceso_asignado { get; set; }
    public string fecha { get; set; }
    public string _id { get; set; }

}