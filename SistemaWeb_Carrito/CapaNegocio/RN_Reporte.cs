﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class RN_Reporte
    {
        private BD_Reporte objCapaDato = new BD_Reporte();

        public List<EN_Reporte> Ventas(string fechaInicio, string fechaFin, string idTransaccion)
        {
            return objCapaDato.Ventas(fechaInicio, fechaFin, idTransaccion);
        }

        public EN_DashBoard VerDashBoard() /*Usa una clase de la capa entidad*/
        {
            return objCapaDato.VerDashBoard();/*Retorna el metodo listar de la instancia de la capa Datos*/
        }
    }//VerDashBoard
}
