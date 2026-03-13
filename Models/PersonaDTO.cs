namespace ReportesMVC.Models
{
    public class PersonaDTO
    {
        public int IdPersona { get; set; }
        public string Appaterno { get; set; }
        public string Apmaterno { get; set; }
        public int IdSexo { get; set; }
        public string NombreSexo { get; set; }
        public string Correo { get; set; }
        public string TelefonoCel1 { get; set; }
        public int IidTipoDocumento { get; set; }
        public string TipoDoc { get; set; }
        public string NumeroIdentificacion { get; set; }
    }
}
