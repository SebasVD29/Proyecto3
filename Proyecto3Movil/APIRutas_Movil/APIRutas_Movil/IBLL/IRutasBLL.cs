﻿using APIRutas_Movil.Modelo;

namespace APIRutas_Movil.IBLL
{
    public interface IRutasBLL
    {
        public Task<ResponseListaRutas> ListarRutasPorChofer(int idChofer);
        public Task<ResponseRutas> CambioEstado(Rutas rutas);


    }
}
