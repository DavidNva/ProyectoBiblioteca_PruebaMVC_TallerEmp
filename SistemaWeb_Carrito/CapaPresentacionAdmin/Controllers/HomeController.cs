﻿    using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;

namespace CapaPresentacionAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();/*Retorna la vista con el nombre de Index (Dentro de la carpeta vista, hay un Index*/
        }
        public ActionResult Usuarios()
        {
            return View();/*Retorna la vista con el nombre de Usuarios(Dentro de la carpeta vista, hay un home, dentro usuarios*/
        }
        [HttpGet] /*Una URL que devuelve datos, un httpost se le pasan los valores y despues devuelve los datos  */
        public JsonResult ListarUsuarios() /*D este json se puede controlar que mas ver, igualar elementos, etc*/
        {
            List<EN_Usuario> oLista = new List<EN_Usuario>();
            oLista = new RN_Usuarios().Listar();/*Esta declarado en RN_Usuarios, capa negocio*/

            return Json(new {data=oLista }, JsonRequestBehavior.AllowGet);
            /*El json da los datos, jala los datos de esa lista, en data*/

        }

        [HttpPost]
        public JsonResult GuardarUsuario(EN_Usuario objeto) /*De este json se puede controlar que mas ver, igualar elementos, etc*/
        {
            object resultado;/*Va a permitir almacenar cualquier tipo de resultado (en este caso int o booelan, dependiendi si es creacion o edicion)*/
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0)/*Es decir, si el id es 0 en inicio (el valor es 0 inicialmente) significa que es
             un usuario nuevo, por lo que se ha dado dando clic con el boton de crear*/
            {
                resultado = new RN_Usuarios().Registrar(objeto, out mensaje);/*El metodo registrar
                 de tipo int, devuelve el id registrado*/
            }else
            {/*Pero si el id es diferente de 0, es decir ya existe, entonces se esta editando
                 a un usuario, por lo que indica que se ha dado clic en el boton de editar, eso lo comprobamos
                 con los alert comentados*/
                resultado = new RN_Usuarios().Editar(objeto, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new RN_Usuarios().Eliminar(id, out mensaje);
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }
        /*La consulta de busqueda por fecha o id transaccion*/
        [HttpGet]
        public JsonResult ListaReporte(string fechaInicio, string fechaFin, string idLibro) 
        {
            List<EN_Reporte> oLista = new List<EN_Reporte>();
            oLista = new RN_Reporte().Ventas(fechaInicio, fechaFin, idLibro);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet); /*Obtenemos el objeto del reporte*/
        }

        [HttpGet]
        public JsonResult VistaDashBoard()
        {
            EN_DashBoard objeto = new RN_Reporte().VerDashBoard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet); /*Obtenemos el objeto del reporte*/
        }



        [HttpPost]
        public FileResult ExportarVenta(string fechaInicio, string fechaFin, string idTransaccion)
        {
            List<EN_Reporte> oLista = new List<EN_Reporte>();
            oLista = new RN_Reporte().Ventas(fechaInicio, fechaFin, idTransaccion);

            DataTable dt = new DataTable();
            dt.Locale = new System.Globalization.CultureInfo("es-MX"); /*Configuracion con mexico*/
            dt.Columns.Add("Fecha Registro", typeof(string));
            dt.Columns.Add("Libro", typeof(string));
            dt.Columns.Add("Autor", typeof(string));
            dt.Columns.Add("Categoria", typeof(string));
            dt.Columns.Add("Paginas", typeof(int));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("IdLibro", typeof(string));

            foreach (EN_Reporte rp in oLista)
            {
                dt.Rows.Add(new object[]{
                    rp.FechaRegistro,
                    rp.Libro,
                    rp.Autor,
                    rp.Categoria,
                    rp.Paginas,
                    rp.Stock,
                    rp.IdLibro
                });
            }
            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteVenta" + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

        //public ActionResult About() 
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();/*Retorna la vista con el nombre de About (Dentro de la carpeta vista, hay un About*/
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
        //public ActionResult PaginaTest() /*Este metodo lleva a una vista
        //                                  *Para Esta vista Se agrega una nueva llamada PaginaTest */
        //{
        //    return View();
        //}

    }
}