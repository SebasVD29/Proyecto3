﻿namespace APIRutas_Movil.Modelo
{
    public class Chofer
    {
        public int IdentificadorChofer {  get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Contraseña { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? Estado { get; set; }
    }
}
