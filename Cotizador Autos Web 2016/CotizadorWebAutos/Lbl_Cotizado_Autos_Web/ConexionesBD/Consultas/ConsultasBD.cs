using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Lbl_Cotizador_Autos_Web.ConexionesBD;
using Lbl_Cotizado_Autos_Web.Estructuras;
using System.Xml.Linq;
using Lbl_Cotizado_Autos_Web.ConexionesBD.Updates;
using Lbl_Cotizado_Autos_Web.Clientes;

namespace Lbl_Cotizado_Autos_Web.ConexionesBD.Consultas
{
    public class ConsultasBD
    {
        #region MYSQL
        public DataTable ConsultarPolizasEmitidas(string pCodPol, string pNumPol, string pCodigoIntermediario, string pTipoUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            string query = "";
            if(pTipoUsuario=="1")
                query = "  SELECT DATE(c.fecha_emision) inicio,DATE_ADD(DATE(c.fecha_emision), INTERVAL 1 YEAR) fin,  " +
                                                            "  c.idepol_emi,c.codpol_emi,c.numpol_emi, p.nombre producto, CONCAT(cl.primer_nombre_individual,' ',cl.segundo_nombre_individual,' ',cl.primer_apellido_individual,' ',cl.segundo_apellido_individual) cliente  " +
                                                            " FROM cotizacion c, planes_web p, cliente cl " +
                                                            " where c.codpol_emi = '" + pCodPol + "' " +
                                                            " and c.numpol_emi = '" + pNumPol + "' " +
                                                            " and p.id_plan_web = c.id_plan_cotizado " +
                                                            "  and c.id_cotizacion = cl.numero_cotizacion " +
                                                            " order by fecha_emision ";
            else
                query = "  SELECT DATE(c.fecha_emision) inicio,DATE_ADD(DATE(c.fecha_emision), INTERVAL 1 YEAR) fin,  " +
                                                            "  c.idepol_emi,c.codpol_emi,c.numpol_emi, p.nombre producto, CONCAT(cl.primer_nombre_individual,' ',cl.segundo_nombre_individual,' ',cl.primer_apellido_individual,' ',cl.segundo_apellido_individual) cliente  " +
                                                            " FROM cotizacion c, planes_web p, cliente cl " +
                                                            " where c.codpol_emi = '" + pCodPol + "' " +
                                                            " and c.numpol_emi = '" + pNumPol + "' " +
                                                            " and c.codigo_intermediario = '" + pCodigoIntermediario + "' " +
                                                            " and p.id_plan_web = c.id_plan_cotizado " +
                                                            "  and c.id_cotizacion = cl.numero_cotizacion " +
                                                            " order by fecha_emision ";
            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public DataTable ObtenerNombreIntermediarioUsuario(string pIdUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" select concat(codigo_intermediario,' ','-',' ',nombres,' ',apellidos) usuario  " +
                                                            " from usuario " +
                                                            " where id_usuario = '" + pIdUsuario + "' ", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public DataTable obtenerCorreosPorRol(string pRolSolicitado)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT us.correo_electronico   " +
                                                            " FROM usuario_rol_acceso usrlac,rol_acceso rla,usuario us " +
                                                            " where usrlac.estado = TRUE " +
                                                            " and us.estado = TRUE " +
                                                            " and rla.estado = TRUE " +
                                                            " and rla.nombre in ('" + pRolSolicitado + "') " +
                                                            " and rla.id_rol = usrlac.id_rol " +
                                                            " and us.id_usuario = usrlac.id_usuario ", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public DataTable obtenerCorreoUsuario(int pNumeroCotizacion)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" select correo_electronico, ifnull(concat(primer_nombre_individual, ' ', segundo_nombre_individual, ' ',primer_apellido_individual, ' ', segundo_apellido_individual),concat(cl.primer_nombre_representante, ' ', cl.segundo_nombre_representante, ' ', cl.primer_apellido_representante, ' ',cl.segundo_apellido_representante)) nombre_cliente   " +
                                                            " from usuario us, cotizacion ct, cliente cl " +
                                                            " where ct.id_cotizacion = '" + pNumeroCotizacion + "'" +
                                                            " and ct.id_usuario_cotizo = us.id_usuario and cl.numero_cotizacion = ct.id_cotizacion ", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();

            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }

        public DataTable ObtenerDescuentos(string pIdUsuario, string pIdPlan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT porcentaje_descuento_maximo FROM usuario_rol_descuento us, rol_descuento rd  " +
                                                            " where id_usuario = '" + pIdUsuario + "' " +
                                                            " and us.id_rol = rd.id_rol and us.estado = 1 and us.id_plan_web = '" + pIdPlan + "' ", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public DataTable ObtenerInformacionCotizacionMYSQL(string pIdCotizacion)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" select c.id_cotizacion, c.fecha_cotizacion,UPPER(c.nombre_solicitante) nombre_solicitante ,UPPER(u.nombre_unico_usuario) nombre_unico_usuario, " +
                                                            " c.desc_tipo_vehiculo,c.desc_marca_vehiculo, c.desc_modelo_vehiculo, c.num_asientos_vehiculo, " +
                                                            " c.monto_asegurado, c.frac_dos_pagos,c.frac_cuatro_pagos, c.frac_seis_pagos, c.frac_ocho_pagos, c.frac_diez_pagos, " +
                                                            " c.frac_doce_pagos,c.cuotas_tres_pagos,c.cuotas_seis_pagos,c.cuotas_diez_pagos,c.cuotas_doce_pagos,c.anio_vehiculo, " +
                                                            " c.pago_contado, concat(i.codinter,'---',i.nomcomercial) intermediario, pw.nombre, c.codigo_intermediario " +
                                                            " from cotizacion c, usuario u, intermediarios i, planes_web pw " +
                                                            " where  c.id_cotizacion = '" + pIdCotizacion + "' and u.id_usuario = c.id_usuario_cotizo and c.codigo_intermediario = i.codinter and c.id_plan_cotizado = pw.id_plan_web", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public DataTable ObtenerInformacionCotizacionMYSQLHOGAR(string pIdCotizacion)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
           
            MySqlDataAdapter adapter = new MySqlDataAdapter(" select c.id_cotizacion, c.fecha_cotizacion,c.nombre_solicitante,u.nombre_unico_usuario, " +
                                                            " c.monto_asegurado, c.frac_dos_pagos,c.frac_cuatro_pagos, c.frac_seis_pagos, c.frac_ocho_pagos, " +
                                                            " c.frac_diez_pagos,  c.frac_doce_pagos,c.cuotas_tres_pagos,c.cuotas_seis_pagos,c.cuotas_diez_pagos, " +
                                                            " c.cuotas_doce_pagos,c.pago_contado , concat(i.codinter,'---',i.nomcomercial) intermediario, d.direccion_bien, d.descripcion_pais, d.descripcion_depto, d.descripcion_muni, p.nombre, p.codplan, " +


                                                            "CASE WHEN p.codplan = '011' THEN  250000 " +
                                                            "WHEN p.codplan = '012' THEN 500000 " +
                                                            "WHEN p.codplan = '013' THEN 1000000  " +
                                                            "WHEN p.codplan = '014' THEN  100000 " +
                                                            "WHEN p.codplan = '015' THEN 200000 " +
                                                            "WHEN p.codplan = '016' THEN 400000  end edificio, " +

                                                            "CASE WHEN p.codplan = '011' THEN  100000 " +
                                                            "when p.codplan = '012' THEN 200000 " +
                                                            "WHEN p.codplan = '013' THEN 400000  " +
                                                            "WHEN p.codplan = '014' THEN  100000 " +
                                                            "WHEN p.codplan = '015' THEN 200000 " +
                                                            "WHEN p.codplan = '016' THEN 400000  end contenidos, " +

                                                            "CASE WHEN p.codplan = '011' THEN 35000  " +
                                                            "when p.codplan = '012' THEN 70000  " +
                                                            "WHEN p.codplan = '013' THEN 140000   " +
                                                            "WHEN p.codplan = '014' THEN  10000 " +
                                                            "WHEN p.codplan = '015' THEN 20000  " +
                                                            "WHEN p.codplan = '016' THEN 40000  end gastos_extras_sec1, " +

                                                            "CASE WHEN p.codplan = '011' THEN 35000  " +
                                                            "when p.codplan = '012' THEN 70000  " +
                                                            "WHEN p.codplan = '013' THEN 140000   " +
                                                            "WHEN p.codplan = '014' THEN  10000 " +
                                                            "WHEN p.codplan = '015' THEN 20000  " +
                                                            "WHEN p.codplan = '016' THEN 40000  end remocion_escombros, " +     
     
                                                            "CASE WHEN p.codplan = '011' THEN 30000  " +
                                                            "when p.codplan = '012' THEN 60000 " +
                                                            "WHEN p.codplan = '013' THEN 120000   " +
                                                            "WHEN p.codplan = '014' THEN  30000 " +
                                                            "WHEN p.codplan = '015' THEN 60000 " +
                                                            "WHEN p.codplan = '016' THEN 120000  end robo_atraco_contenidos, " +
                                                            
                                                            "CASE WHEN p.codplan = '011' THEN 20000  " +
                                                            "when p.codplan = '012' THEN 40000  " +
                                                            "WHEN p.codplan = '013' THEN 80000   " +
                                                            "WHEN p.codplan = '014' THEN  20000 " +
                                                            "WHEN p.codplan = '015' THEN 40000  " +
                                                            "WHEN p.codplan = '016' THEN 80000  end rotura_cristales, " +
                                                            
                                                            "CASE WHEN p.codplan = '011' THEN 500  " +
                                                            "when p.codplan = '012' THEN 1000  " +
                                                            "WHEN p.codplan = '013' THEN 2000   " +
                                                            "WHEN p.codplan = '014' THEN  500 " +
                                                            "WHEN p.codplan = '015' THEN 1000  " +
                                                            "WHEN p.codplan = '016' THEN 2000  end objetos_personales, " +
                                                            
                                                            "CASE WHEN p.codplan = '011' THEN 200000  " +
                                                            "when p.codplan = '012' THEN 400000  " +
                                                            "WHEN p.codplan = '013' THEN 800000   " +
                                                            "WHEN p.codplan = '014' THEN   200000 " +
                                                            "WHEN p.codplan = '015' THEN 400000  " +
                                                            "WHEN p.codplan = '016' THEN 800000  end resp_civil_familiar, " +

                                                            "CASE WHEN p.codplan = '011' THEN 10000  " +
                                                            "when p.codplan = '012' THEN 20000  " +
                                                            "WHEN p.codplan = '013' THEN 40000   " +
                                                            "WHEN p.codplan = '014' THEN  10000 " +
                                                            "WHEN p.codplan = '015' THEN 20000  " +
                                                            " WHEN p.codplan = '016' THEN 40000  end trabajadores_domesticos, " + 
    
                                                            "CASE WHEN p.codplan = '011' THEN 1000  " +
                                                            "when p.codplan = '012' THEN 2000  " +
                                                            "WHEN p.codplan = '013' THEN 4000   " +
                                                            "WHEN p.codplan = '014' THEN  1000 " +
                                                            "WHEN p.codplan = '015' THEN 2000  " +
                                                            "WHEN p.codplan = '016' THEN 4000  end dinero_y_valores, " +

                                                            "CASE WHEN p.codplan = '011' THEN 20000  " +
                                                            "when p.codplan = '012' THEN 40000 " +
                                                            "WHEN p.codplan = '013' THEN 80000   " +
                                                            "WHEN p.codplan = '014' THEN  20000 " +
                                                            "WHEN p.codplan = '015' THEN 40000  " +
                                                            "WHEN p.codplan = '016' THEN 80000  end rotura_maquinaria " +


                                                            " from cotizacion c, usuario u , datos_bien d, planes_web p, intermediarios i " +
                                                            " where  c.id_cotizacion = '" + pIdCotizacion + "' and u.id_usuario = c.id_usuario_cotizo and u.codigo_intermediario = i.codinter and d.id_cotizacion = c.id_cotizacion and c.id_plan_cotizado = p.id_plan_web ", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public DataTable ObtenerInformacionCotizacionORACLE(string pIdCotizacion)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" select r.id_cotizacion,r.seccion_cobertura, r.descripcion_seccion_cobertura, r.deducible_minimo, " +
                                                            " r.deducible_maximo, r.base, r.suma_asegurada, r.cub_ex, r.codigo_moneda " +
                                                            " from cotizacion c,reporte_cotizacion_autos r " +
                                                            " where  c.id_cotizacion = '" + pIdCotizacion + "' and c.id_usuario_cotizo = c.id_usuario_cotizo and r.id_cotizacion = c.id_cotizacion", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        
        public DataTable ObtenerCodigoIntermediario(string pIdUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT codigo_intermediario, usuario_interno FROM usuario where id_usuario = '" +
                                                            pIdUsuario + "'", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return datos;
        }
        public string ObtenerCodigoIntermediario(int pIdUsuario)
        {
            DataTable datos = new DataTable();
            string codigoIntermediario = string.Empty;
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT codigo_intermediario FROM usuario where id_usuario = " +
                                                            pIdUsuario, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    codigoIntermediario = datos.Rows[0]["codigo_intermediario"].ToString();
                }
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return codigoIntermediario;
        }
        public DataTable ObtenerPlanesDeIntermediario(string pCODINTER, string nombreTab)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT plw.nombre, plw.id_plan_web " +
                            " FROM plan_intermediario pli, planes_web plw, plan_categoria_producto pcp, categoria_productos cp" +
                            " where pli.id_plan_web = plw.id_plan_web" +
                            " and cp.id_categoria_producto = pcp.id_categoria_producto" +
                            " and pli.id_plan_web = pcp.id_plan" +
                            " and pli.estado = 1 " +
                            " and pli.codinter = '" + pCODINTER + "'" +
                            " and cp.nombre = '" + nombreTab +"'" +
                            " order by plw.nombre";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerCategoriasDisponiblesXMostrar(string pCODINTER)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT distinct c.nombre " +
                            " FROM PLAN_INTERMEDIARIO PLI, planes_web PLW, plan_categoria_producto pcp, categoria_productos c" +
                            " WHERE PLI.ID_PLAN_WEB = PLW.ID_PLAN_WEB" +
                            " and pcp.id_plan = PLW.id_plan_web" +
                            " and c.id_categoria_producto = pcp.id_categoria_producto" +
                            " AND PLI.ESTADO = 1" +
                            " AND PLI.CODINTER = '" + pCODINTER + "'";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerCotizacionesAlmacenadas(string pIdUsuario, string pIdPlanWeb)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacionEli = 0;
            int idEstadoCotizacionEmi = 0;
            int idEstadoCotizacionDene = 0;

            idEstadoCotizacionEli = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
            idEstadoCotizacionEmi = objetoOperaciones.obtenerIdEstadoCotizacion("EMI");
            idEstadoCotizacionDene = objetoOperaciones.obtenerIdEstadoCotizacion("DEN");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *,es.nombre estado_coti FROM cotizacion cot, estado_cotizacion es"
                                + " where es.id_estado_cotizacion = cot.estado_cotizacion"
                                + " and cot.id_usuario_cotizo = " + pIdUsuario
                                + " and cot.id_plan_cotizado = " + pIdPlanWeb
                                + " and cot.estado_cotizacion not in (" + idEstadoCotizacionEli + ", " + idEstadoCotizacionEmi + ", " + idEstadoCotizacionDene + ")"
                                + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                                + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerCotizacionesAlmacenadasXPlan(string pIdUsuario,string idplan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacionEli = 0;
            int idEstadoCotizacionEmi = 0;
            int idEstadoCotizacionDene = 0;


            idEstadoCotizacionEli = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
            idEstadoCotizacionEmi = objetoOperaciones.obtenerIdEstadoCotizacion("EMI");
            idEstadoCotizacionDene = objetoOperaciones.obtenerIdEstadoCotizacion("DEN");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *,pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, estado_cotizacion es, planes_web pl"
                                + " where es.id_estado_cotizacion = cot.estado_cotizacion"
                                + " and cot.id_usuario_cotizo = " + pIdUsuario
                                + " and cot.estado_cotizacion not in (" + idEstadoCotizacionEli + ", " + idEstadoCotizacionEmi + ", " + idEstadoCotizacionDene + ")"
                                + " and cot.id_plan_cotizado = pl.id_plan_web "
                                + " and pl.id_plan_web = " + idplan 
                                + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                                + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                                + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerCotizacionesAlmacenadasXPlan(string pIdUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacionEli = 0;
            int idEstadoCotizacionEmi = 0;
            int idEstadoCotizacionDene = 0;


            idEstadoCotizacionEli = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
            idEstadoCotizacionEmi = objetoOperaciones.obtenerIdEstadoCotizacion("EMI");
            idEstadoCotizacionDene = objetoOperaciones.obtenerIdEstadoCotizacion("DEN");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *,pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, estado_cotizacion es, planes_web pl"
                                + " where es.id_estado_cotizacion = cot.estado_cotizacion"
                                + " and cot.id_usuario_cotizo = " + pIdUsuario
                                + " and cot.estado_cotizacion not in (" + idEstadoCotizacionEli + ", " + idEstadoCotizacionEmi + ", " + idEstadoCotizacionDene + ")"
                                + " and cot.id_plan_cotizado = pl.id_plan_web "
                                + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                                + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                                + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerTodasLasCotizaciones(string pIdUsuario,string idplan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacionEli = 0;
            int idEstadoCotizacionEmi = 0;
            int idEstadoCotizacionDene = 0;


            idEstadoCotizacionEli = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
            idEstadoCotizacionEmi = objetoOperaciones.obtenerIdEstadoCotizacion("EMI");
            idEstadoCotizacionDene = objetoOperaciones.obtenerIdEstadoCotizacion("DEN");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *, pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, planes_web pl, estado_cotizacion es"
                               + " where cot.estado_cotizacion not in (" + idEstadoCotizacionEli + ", " + idEstadoCotizacionEmi + ", " + idEstadoCotizacionDene + ")"                                
                               + " and cot.id_plan_cotizado = pl.id_plan_web"
                               + " and pl.id_plan_web = " + idplan
                               + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                               + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                               + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerTodasLasCotizaciones(string pIdUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacionEli = 0;
            int idEstadoCotizacionEmi = 0;
            int idEstadoCotizacionDene = 0;


            idEstadoCotizacionEli = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
            idEstadoCotizacionEmi = objetoOperaciones.obtenerIdEstadoCotizacion("EMI");
            idEstadoCotizacionDene = objetoOperaciones.obtenerIdEstadoCotizacion("DEN");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *, pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, planes_web pl, estado_cotizacion es"
                               + " where cot.estado_cotizacion not in (" + idEstadoCotizacionEli + ", " + idEstadoCotizacionEmi + ", " + idEstadoCotizacionDene + ")"
                               + " and cot.id_plan_cotizado = pl.id_plan_web"
                               + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                               + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                               + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerTodasLasCotizacionesXPlan(int idPlan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacionEli = 0;
            int idEstadoCotizacionEmi = 0;
            int idEstadoCotizacionDene = 0;


            idEstadoCotizacionEli = objetoOperaciones.obtenerIdEstadoCotizacion("ELI");
            idEstadoCotizacionEmi = objetoOperaciones.obtenerIdEstadoCotizacion("EMI");
            idEstadoCotizacionDene = objetoOperaciones.obtenerIdEstadoCotizacion("DEN");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *, pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, planes_web pl, estado_cotizacion es"
                               + " where cot.estado_cotizacion not in (" + idEstadoCotizacionEli + ", " + idEstadoCotizacionEmi + ", " + idEstadoCotizacionDene + ")"
                               + " and cot.id_plan_cotizado = pl.id_plan_web"
                               + " and cot.id_plan_cotizado = " + idPlan
                               + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                               + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                               + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerCotizacionesXAutorizar(string idplan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacion = 0;           

            idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion("INS");           

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *,pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, estado_cotizacion es, planes_web pl, usuario us"
                                + " where es.id_estado_cotizacion = cot.estado_cotizacion"
                                + " and cot.id_usuario_cotizo = us.id_usuario"
                                + " and cot.estado_cotizacion = " + idEstadoCotizacion
                                + " and cot.id_plan_cotizado = pl.id_plan_web "
                                + " and pl.id_plan_web = " + idplan
                                + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                                + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                                + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerCotizacionesXAutorizar()
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacion = 0;

            idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion("INS");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *,pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, estado_cotizacion es, planes_web pl, usuario us"
                                + " where es.id_estado_cotizacion = cot.estado_cotizacion"
                                + " and cot.id_usuario_cotizo = us.id_usuario"
                                + " and cot.estado_cotizacion = " + idEstadoCotizacion
                                + " and cot.id_plan_cotizado = pl.id_plan_web "
                                + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                                + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                                + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerCotizacionesXAutorizarXPlan(int idPlan)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            Operaciones objetoOperaciones = new Operaciones();
            int idEstadoCotizacion = 0;

            idEstadoCotizacion = objetoOperaciones.obtenerIdEstadoCotizacion("INS");

            conexion = clConexiones.abrirConexionMysql();

            string query = "SELECT *,pl.nombre nombre_plan_web, es.nombre estado_coti FROM cotizacion cot, estado_cotizacion es, planes_web pl, usuario us"
                                + " where es.id_estado_cotizacion = cot.estado_cotizacion"                                
                                + " and cot.estado_cotizacion = " + idEstadoCotizacion
                                + " and cot.id_plan_cotizado = " + idPlan
                                + " and cot.id_plan_cotizado = pl.id_plan_web "
                                + " and cot.estado_cotizacion = es.id_estado_cotizacion"
                                + " and DATE_ADD(cot.fecha_cotizacion, INTERVAL  15 day) > NOW()"
                                + " order by cot.id_cotizacion desc";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable obtenerDetalleCotizacion(string IdCotizacion)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT desc_forma_pago_cotizado,desc_numero_pagos_cotizado " + 
                                                            " FROM cotizacion where id_cotizacion = " + IdCotizacion, conexion);
            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerNombrePlan(string pIdPlanWeb)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT * FROM planes_web where id_plan_web = '" + pIdPlanWeb + "'", conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
        public DataTable ObtenerDatosVehiculoCotizado(int idCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT tipoveh_vehiculo,codmarca_vehiculo,codmodelo_vehiculo,anio_vehiculo " +
                                                            " FROM cotizacion WHERE id_cotizacion = " + idCotizacion, conexion);
            try
            {
                adapter.Fill(datosVehiculo);
                resultado = ObtenerDescripcionVehiculos(datosVehiculo.Rows[0]["tipoveh_vehiculo"].ToString(),
                                                        datosVehiculo.Rows[0]["codmarca_vehiculo"].ToString(),
                                                        datosVehiculo.Rows[0]["codmodelo_vehiculo"].ToString(),
                                                        datosVehiculo.Rows[0]["anio_vehiculo"].ToString());
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable ObtenerComentarioInspeccion(string pIdCotizacion)
        {
            DataTable resultado = new DataTable();
            DataTable datosVehiculo = new DataTable();

            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();

            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" SELECT comentarios_inspeccion, numero_inspeccion FROM inspeccion_vehiculo where id_cotizacion = '" + pIdCotizacion + "'", conexion);
            try
            {
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        #endregion

        #region Oracle
        public DataTable ObtenerInforomacionRC(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pSumaAsegurada)
        {            
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Distinct RowNum, T.SUMAASEG,NVL(D.PRIMA,T.PRIMA) PRIMA, NVL(D.TASA,T.TASA) TASA " +
                                                              " From TARIFA_RCV_TIPO_VEH T, PLAN_PROD P, TARIFA_RCV_TIPO_VEH_DET D " +
                                                              " Where P.CODPROD = '" + pCodProd + "' " +
                                                              " And P.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And P.REVPLAN = '" + pRevPlan + "' " +
				                                              " And T.CODPROD = P.CODPROD " + 
				                                              " And T.CODPLAN = P.CODPLAN " +
				                                              " And T.REVPLAN = P.REVPLAN " +
                                                              " and D.CODPROD (+)= T.CODPROD " +
                                                              " And D.CODPLAN  (+)= T.CODPLAN " +
                                                              " And D.REVPLAN (+)= T.REVPLAN " +
                                                              " and D.CODCOBERT (+)= T.CODCOBERT " +
                                                              " and D.CODTARIF (+)= T.CODTARIF " +
                                                              " And T.TIPOVEH = '" + pTipoVeh + "'  " +
                                                              " and D.TIPOVEH (+)= '" + pTipoVeh + "'  " +
                                                              " and '" + pSumaAsegurada + "' BETWEEN D.MTOSUMAMIN(+) and D.MTOSUMAMAX(+)" +
                                                              " Order By SUMAASEG desc", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInformacionRC(string cCodProd, string cCodPlan, string cCodRevPlan, string cCodRamo, string cTipoVeh, string cCodModelo, string cCodMarca)
        {            
           DataTable datosCliente = new DataTable();

           Conexiones clsConexion = new Conexiones();
           OracleConnection conexionOracle = new OracleConnection();

           OracleCommand cmd = new OracleCommand();
           conexionOracle = clsConexion.abrirConexionOracleAcsel();

           cmd.Connection = conexionOracle;           
           cmd.CommandText = "PK_COTIZADOR_WEB.FN_SUMAS_RC";
           cmd.CommandType = CommandType.StoredProcedure;
           OracleParameter p1 = new OracleParameter("cCodProd", OracleDbType.Varchar2);
           OracleParameter p2 = new OracleParameter("cCodPlan", OracleDbType.Varchar2);
           OracleParameter p3 = new OracleParameter("cCodRevPlan", OracleDbType.Varchar2);
           OracleParameter p4 = new OracleParameter("cCodRamo", OracleDbType.Varchar2);
           OracleParameter p5 = new OracleParameter("cTipoVeh", OracleDbType.Varchar2);
           OracleParameter p6 = new OracleParameter("cCodModelo", OracleDbType.Varchar2);
           OracleParameter p7 = new OracleParameter("cCodMarca", OracleDbType.Varchar2);
           OracleParameter p8 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

           p1.Value = cCodProd;
           p2.Value = cCodPlan;
           p3.Value = cCodRevPlan;
           p4.Value = cCodRamo;
           p5.Value = cTipoVeh;
           p6.Value = cCodModelo;
           p7.Value = cCodMarca;

           p8.Direction = ParameterDirection.ReturnValue;

           cmd.Parameters.Add(p8);
           cmd.Parameters.Add(p1);
           cmd.Parameters.Add(p2);
           cmd.Parameters.Add(p3);
           cmd.Parameters.Add(p4);
           cmd.Parameters.Add(p5);
           cmd.Parameters.Add(p6);
           cmd.Parameters.Add(p7);

           try
           {
               datosCliente.Load(cmd.ExecuteReader());
               cmd.Connection.Close();
           }
           catch (OracleException ex)
           {
               cmd.Connection.Close();
               throw ex; 
           }

           return datosCliente;
             
        }
        public DataTable ObtenerInforomacionRCParaCoberturas(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pMontoSeleccionado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Distinct RowNum, T.SUMAASEG,NVL(D.PRIMA,T.PRIMA) PRIMA, NVL(D.TASA,T.TASA) TASA " +
                                                              " From TARIFA_RCV_TIPO_VEH T, PLAN_PROD P, TARIFA_RCV_TIPO_VEH_DET D " +
                                                              " Where P.CODPROD = '" + pCodProd + "' " +
                                                              " And P.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And P.REVPLAN = '" + pRevPlan + "' " +
                                                              " And T.CODPROD = P.CODPROD " +
                                                              " And T.CODPLAN = P.CODPLAN " +
                                                              " And T.REVPLAN = P.REVPLAN " +
                                                              " and D.CODPROD (+)= T.CODPROD " +
                                                              " And D.CODPLAN  (+)= T.CODPLAN " +
                                                              " And D.REVPLAN (+)= T.REVPLAN " +
                                                              " and D.CODCOBERT (+)= T.CODCOBERT " +
                                                              " and D.CODTARIF (+)= T.CODTARIF " +
                                                              " And T.TIPOVEH = '" + pTipoVeh + "'  " +
                                                              " and D.TIPOVEH (+)= '" + pTipoVeh + "'  " +
                                                              "  and T.SUMAASEG = '" + pMontoSeleccionado + "' ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInforomacionMenores(string pCodProd, string pCodPlan, string pRevPlan)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" select INDRECMEN, TO_CHAR(RANGO)||' a '||TO_CHAR(RANGOMAX)||' años' as Edad " +
                                                              " FROM RECA_DCTO_PLAN_PROD RDPP " +
                                                              " WHERE RDPP.CODPROD ='" + pCodProd + "' " +
                                                              " And RDPP.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And RDPP.REVPLAN = '" + pRevPlan + "' " +
                                                              " AND RDPP.INDAPLIMP ='S' " +
                                                              " and NVL(RDPP.INDRECMEN,'*') NOT IN ('*','A','B') " +
                                                              " order by RANGO DESC ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInforomacionCoberturaOcupantes(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh,
                                                               string pCodModelo, string pCodMarca, string pMontoAsegurar)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_SUMAS_APOV";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCodProd", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("cCodPlan", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("cRevPlan", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cCodRamoCert", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("cTipoVeh", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cCodModelo", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodMarca", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("nValAuto", OracleDbType.Int32);
            OracleParameter p9 = new OracleParameter("cCodCobert", OracleDbType.Varchar2);
            OracleParameter p10 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = "AUR1";
            p5.Value = pTipoVeh;
            p6.Value = pCodModelo;
            p7.Value = pCodMarca;
            p8.Value = pMontoAsegurar;
            p9.Value = "AUS1";

            p10.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p10);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosCliente;
            //DataTable datos = new DataTable();

            //Conexiones conexionOracle = new Conexiones();
            //OracleConnection conexion = new OracleConnection();
            //conexion = conexionOracle.abrirConexionOracleAcsel();
            //OracleDataAdapter adapter = new OracleDataAdapter(" Select Distinct T.CODSUBPLAN, T.SUMAPORASEG,NVL(D.PRIMA,T.PRIMA) PRIMA, NVL(D.TASA,T.TASA) TASA, t.CODCOBERT " +
            //                                                  " From TARIFA_APOV_TIPO_VEH T, PLAN_PROD P, TARIFA_APOV_TIPO_VEH_DET D " +
            //                                                  " Where P.CODPROD ='" + pCodProd + "' " +
            //                                                  " And P.CODPLAN   = '" + pCodPlan + "' " +
            //                                                  " And P.REVPLAN = '" + pRevPlan + "' " +
            //                                                  " And T.CODPROD = P.CODPROD " +
            //                                                  " And T.CODPLAN = P.CODPLAN " +
            //                                                  " And T.REVPLAN = P.REVPLAN " +
            //                                                  " And D.TIPOVEH (+) = '" + pTipoVeh + "' " +
            //                                                  " and D.CODPROD (+)= T.CODPROD " +
            //                                                  " And D.CODPLAN  (+)= T.CODPLAN " +
            //                                                  " And D.REVPLAN (+)= T.REVPLAN " +
            //                                                  " and D.CODCOBERT (+)= T.CODCOBERT " +
            //                                                  " and D.CODSUBPLAN (+)= T.CODSUBPLAN " +
            //                                                  " And T.TIPOVEH = '" + pTipoVeh + "' " +
            //                                                  " AND T.CODCOBERT = (SELECT MIN(CODCOBERT) FROM TARIFA_APOV_TIPO_VEH TV WHERE TV.CODPROD = T.CODPROD AND TV.CODPLAN = T.CODPLAN AND TV.REVPLAN = T.REVPLAN AND TV.TIPOVEH = T.TIPOVEH AND TV.CODSUBPLAN = T.CODSUBPLAN) " +
            //                                                  " and '" + pMontoAsegurar + "' BETWEEN D.MTOSUMAMIN(+) and D.MTOSUMAMAX(+) ORDER BY T.SUMAPORASEG desc", conexion);

            //try
            //{
            //    adapter.Fill(datos);

            //    conexion.Close();
            //}
            //catch (OracleException ex)
            //{
            //    conexion.Close();
            //}

            //return datos;
        }
        public DataTable ObtenerInforomacionCoberturaOcupantesPorMontoCotizado(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pMontoCotizado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Distinct T.CODSUBPLAN, T.SUMAPORASEG,NVL(D.PRIMA,T.PRIMA) PRIMA, NVL(D.TASA,T.TASA) TASA, t.CODCOBERT " +
                                                              " From TARIFA_APOV_TIPO_VEH T, PLAN_PROD P, TARIFA_APOV_TIPO_VEH_DET D " +
                                                              " Where P.CODPROD ='" + pCodProd + "' " +
                                                              " And P.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And P.REVPLAN = '" + pRevPlan + "' " +
                                                              " And T.CODPROD = P.CODPROD " +
                                                              " And T.CODPLAN = P.CODPLAN " +
                                                              " And T.REVPLAN = P.REVPLAN " +
                                                              " And D.TIPOVEH = '" + pTipoVeh + "' " +
                                                              " and D.CODPROD (+)= T.CODPROD " +
                                                              " And D.CODPLAN  (+)= T.CODPLAN " +
                                                              " And D.REVPLAN (+)= T.REVPLAN " +
                                                              " and D.CODCOBERT (+)= T.CODCOBERT " +
                                                              " and D.CODSUBPLAN (+)= T.CODSUBPLAN " +
                                                              " And T.TIPOVEH = '" + pTipoVeh + "' " +
                                                              " and T.SUMAASEG = '" + pMontoCotizado + "' ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInforomacionMuerteAccidental(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT RowNum, SUMAASEG " +
                                                              " FROM TARIFA_OTROS_TIPO_VEH " +
                                                              " WHERE CODPROD ='" + pCodProd + "' " +
                                                              " And CODPLAN   = '" + pCodPlan + "' " +
                                                              " And REVPLAN = '" + pRevPlan + "' " +
                                                              " And TIPOVEH = '" + pTipoVeh + "' " +
                                                              " ORDER BY SUMAASEG DESC", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable obtenerNumeroPagosXPlan(string pCodProd, string pCodPlan, string pRevPlan, string pCodPlanFracc)
        {
            DataTable datos = new DataTable();
            DateTime fechaActual = DateTime.Now;

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select CUOTAS, NOMPLAN, CODPLANFINAN " +
                                                              " FROM plan_financiamiento_plan_prod " +
                                                              " WHERE CODPROD ='" + pCodProd + "' " +
                                                              " AND cotiza_Web = 'S' " +
                                                              " And CODPLAN   = '" + pCodPlan + "' " +
                                                              " AND (CODPLANFINAN,MODPLAN) IN ( SELECT PF.CODPLAN,PF.MODPLAN " +
                                                              " FROM PLAN_FINANCIAMIENTO PF " +
                                                              " WHERE  to_date('" + fechaActual.ToString("dd") + fechaActual.ToString("MM") + 
                                                              fechaActual.ToString("yyyy") +"','DDMMYYYY') BETWEEN TRUNC(PF.INIPLAN) " +
                                                              " AND TRUNC(FINPLAN)) " +
                                                              " And REVPLAN = '" + pRevPlan + "' " +
                                                              " And VISA_CUOTA = '" + pCodPlanFracc + "' ORDER BY CUOTAS", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerMODPLAN(string pCodProd, string pCodPlan, string pRevPlan, string pCodPlanFracc, string pCODPLANFINAN)
        {
            DataTable datos = new DataTable();
            DateTime fechaActual = DateTime.Now;

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select MODPLAN " +
                                                              " FROM plan_financiamiento_plan_prod " +
                                                              " WHERE CODPROD ='" + pCodProd + "' " +
                                                              " And CODPLAN   = '" + pCodPlan + "' " +
                                                              " AND (CODPLANFINAN,MODPLAN) IN ( SELECT PF.CODPLAN,PF.MODPLAN " +
                                                              " FROM PLAN_FINANCIAMIENTO PF " +
                                                              " WHERE  to_date('" + fechaActual.ToString("dd") + fechaActual.ToString("MM") +
                                                              fechaActual.ToString("yyyy") + "','DDMMYYYY') BETWEEN TRUNC(PF.INIPLAN) " +
                                                              " AND TRUNC(FINPLAN) and CODPLANFINAN = '" + pCODPLANFINAN + "') " +
                                                              " And REVPLAN = '" + pRevPlan + "' " +
                                                              " And VISA_CUOTA = '" + pCodPlanFracc + "' ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInforomacionMontoCristales(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT sumaaseg " +
                                                              " FROM TARIFA_OTROS_TIPO_VEH " +
                                                              " WHERE CODPROD ='" + pCodProd + "' " +
                                                              " And CODPLAN   = '" + pCodPlan + "' " +
                                                              " And REVPLAN = '" + pRevPlan + "' " +
                                                              " And TIPOVEH = '" + pTipoVeh + "' " +
                                                              " AND codcobert = 'CIRS' " +
                                                              " AND codramo = 'AUR1'", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerTipoVehiculo(string pCodProd, string pCodPlan, string pRevPlan)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Distinct P.ORDEN - 1 As Correlativo, P.TIPOVEH, L.DESCRIP, P.ORDEN " +
                                                              " From PLAN_TIPO_CLASE_VEH P, LVAL L " +
                                                              " WHERE P.CODPROD ='" + pCodProd + "' " +
                                                              " And CODPLAN   = '" + pCodPlan + "' " +
                                                              " And REVPLAN = '" + pRevPlan + "' " +
                                                              " And L.TIPOLVAL = 'TIPOVEH' " +
                                                              " And L.CODLVAL = P.TIPOVEH " +
                                                              " AND DESCRIP NOT IN ('MOTOCICLETAS','MOTOCICLETA DE LUJO','PANEL PARTICULAR','REMOLQUES','CAMION','BUS USO COMERCIAL (NO TRANS.COMERCIAL)','CABEZALES','PANEL','MICROBUS USO COMERCIAL') " +
                                                              " Order By P.ORDEN, L.DESCRIP", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerListadoMarcasVehiculo()
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODMARCA, DESCMARCA FROM MARCA_VEH WHERE INDTIPO= 'A' AND INDACTIVOWEB = 'S' ORDER BY DESCMARCA", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerListadoLineasVehiculo(string pCodigoMarca)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT CODMODELO, DESCMODELO FROM MODELO_VEH WHERE CODMARCA = '" + pCodigoMarca + "' AND INDACTIVOWEB = 'S' ORDER BY DESCMODELO", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public int GET_IDPROCESO(string pCodUsr)
        {
            int resultado = 0;

            OracleConnection conexion = new OracleConnection();
            Conexiones conexionOracle = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = conexionOracle.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "pr_oper_sas.GET_IDPROCESO";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 100);
                OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Int32);

                p1.Value = pCodUsr;
                p2.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public string generarXML_CA_ASEGURADO(List<CA_ASEGURADO> datosAsegurado)
        {
            var xmlfromLINQ = new XElement("PROCESO",
                       from c in datosAsegurado
                       select new XElement("ASEGURADO",
                           new XElement("IDEPROCESO", c.IDEPROCESO),
                             new XElement("LLAVE", c.LLAVE),
                             new XElement("LLAVE_ALTERNA", c.LLAVE_ALTERNA),
                             new XElement("CONTRATANTE", c.CONTRATANTE),
                             new XElement("ASEGURADO", c.ASEGURADO),
                             new XElement("RESP_PAGO", c.RESP_PAGO),
                             new XElement("BENEFICIARIO", c.BENEFICIARIO),
                             new XElement("DEPENDIENTE", c.DEPENDIENTE),
                             new XElement("CODIGO_CLIENTE", c.CODIGO_CLIENTE),
                             new XElement("NOMBRE", c.NOMBRE),
                             new XElement("PRIMER_APELLIDO", c.PRIMER_APELLIDO),
                             new XElement("SEGUNDO_APELLIDO", c.SEGUNDO_APELLIDO),
                             new XElement("APELLIDO_CASADA", c.APELLIDO_CASADA),
                             new XElement("GENERO", c.GENERO),
                             new XElement("FECHANAC", c.FECHANAC),
                             new XElement("NACIONAL_EXTRANJERO", c.NACIONAL_EXTRANJERO),
                             new XElement("PAIS_NACIMIENTO", c.PAIS_NACIMIENTO),
                             new XElement("DEPARTAMENTO_NACIMIENTO", c.DEPARTAMENTO_NACIMIENTO),
                             new XElement("MUNICIPIO_NACIMIENTO", c.MUNICIPIO_NACIMIENTO),
                             new XElement("CEDULA", c.CEDULA),
                             new XElement("PAIS_CEDULA", c.PAIS_CEDULA),
                             new XElement("DEPARTAMENTO_CEDULA", c.DEPARTAMENTO_CEDULA),
                             new XElement("MUNICIPIO_CEDULA", c.MUNICIPIO_CEDULA),
                             new XElement("PASAPORTE", c.PASAPORTE),
                             new XElement("NIT", c.NIT),
                             new XElement("TIPO_CLIENTE", c.TIPO_CLIENTE),
                             new XElement("ESTADO_CIVIL", c.ESTADO_CIVIL),
                             new XElement("ACTIVIDAD_ECONOMICA", c.ACTIVIDAD_ECONOMICA),
                             new XElement("OCUPACION", c.OCUPACION),
                             new XElement("PROFESION", c.PROFESION),
                             new XElement("DEPORTE", c.DEPORTE),
                             new XElement("PAIS_DIRECCION", c.PAIS_DIRECCION),
                             new XElement("DEPARTAMENTO_DIRECCION", c.DEPARTAMENTO_DIRECCION),
                             new XElement("MUNICIPIO_DIRECCION", c.MUNICIPIO_DIRECCION),
                             new XElement("ALDEA_LOCALIDAD_ZONA", c.ALDEA_LOCALIDAD_ZONA),
                             new XElement("DIRECCION", c.DIRECCION),
                             new XElement("COLONIA", c.COLONIA),
                             new XElement("ZONA_DIR", c.ZONA_DIR),
                             new XElement("TELEFONO1", c.TELEFONO1),
                             new XElement("TELEFONO2", c.TELEFONO2),
                             new XElement("TELEFONO3", c.TELEFONO3),
                             new XElement("TELEFONO4", c.TELEFONO4),
                             new XElement("CORREO_ELECTRONICO", c.CORREO_ELECTRONICO),
                             new XElement("PORCENTAJE", c.PORCENTAJE),
                             new XElement("PARENTESCO", c.PARENTESCO),
                             new XElement("TIPOID", c.TIPOID),
                             new XElement("NUMID", c.NUMID),
                             new XElement("DVID", c.DVID),
                             new XElement("CODCLI", c.CODCLI),
                             new XElement("NUEVACEDULA", c.NUEVACEDULA),
                             new XElement("RAMO", c.RAMO),
                             new XElement("USUARIO", c.USUARIO),
                             new XElement("FECHA", c.FECHA),
                             new XElement("REFERENCIA", c.REFERENCIA),
                             new XElement("LINEA", c.LINEA),
                             new XElement("LAYOUT", c.LAYOUT),
                             new XElement("STSCA", c.STSCA),
                             new XElement("DPI", c.DPI),
                             new XElement("TIPO_CTA", c.TIPO_CTA),
                             new XElement("IDBASE", c.IDBASE),
                             new XElement("SEGUNDO_NOMBRE", c.SEGUNDO_NOMBRE),
                             new XElement("CASA_NUMERO", c.CASA_NUMERO),
                             new XElement("APTO_SIMILAR", c.APTO_SIMILAR),
                             new XElement("AVENIDA", c.AVENIDA),
                             new XElement("CALLE", c.CALLE),
                             new XElement("LOTE", c.LOTE),
                             new XElement("MANZANA", c.MANZANA),
                             new XElement("AVENIDACLASIF", c.AVENIDACLASIF),
                             new XElement("CALLECLASIF", c.CALLECLASIF),
                             new XElement("SECTOR", c.SECTOR),
                             new XElement("ZONA", c.ZONA),
                             new XElement("RELACION_DEPENDENCIA", c.RELACION_DEPENDENCIA),
                             new XElement("NOMBRE_PATRONO", c.NOMBRE_PATRONO),
                             new XElement("FECHA_INGRESO", c.FECHA_INGRESO),
                             new XElement("TIPO_CUENTA", c.TIPO_CUENTA),
                             new XElement("NUMERO_CUENTA", c.NUMERO_CUENTA),
                             new XElement("ENTIDAD_CUENTA", c.ENTIDAD_CUENTA),
                             new XElement("CODIGO_ENTIDAD_TARJETA", c.CODIGO_ENTIDAD_TARJETA),
                             new XElement("AUTORIZACION", c.AUTORIZACION),
                             new XElement("USUARIO_AUTORIZACION", c.USUARIO_AUTORIZACION),
                             new XElement("NUMERO_LOTE", c.NUMERO_LOTE),
                             new XElement("MONTO_COBRADO", c.MONTO_COBRADO),
                             new XElement("NUMCHQ", c.NUMCHQ),
                             new XElement("FECSTS", c.FECSTS),
                             new XElement("MTOCHQ", c.MTOCHQ),
                             new XElement("DEDUCIBLE", c.DEDUCIBLE),
                             new XElement("TIMBRES", c.TIMBRES),
                             new XElement("IVA", c.IVA),
                             new XElement("ISR", c.ISR),
                             new XElement("CODCOBERT", c.CODCOBERT),
                             new XElement("MTOTOTRES", c.MTOTOTRES),
                             new XElement("CODPLANFRAC", c.CODPLANFRAC),
                             new XElement("MODPLANFRAC", c.MODPLANFRAC),
                             new XElement("FECVENC_CUENTA", c.FECVENC_CUENTA),
                             new XElement("INDPEP", c.INDPEP),
                             new XElement("ACTUA_NOMBRE_PROPIO", c.ACTUA_NOMBRE_PROPIO)
                           ));

            return xmlfromLINQ.ToString();
        }
        public string generarXML_CA_CERTIFICADO(List<CA_CERTIFICADO> datosCertificado, List<CA_COBERTURA> pCoberturas, List<CA_REGARGO> pRecargos)
        {
            var xmlfromLINQ = new XElement("PROCESO",
                        from c in datosCertificado
                        select new XElement("CERTIFICADO",
                                 new XElement("IDEPROCESO", c.IDEPROCESO),
                                 new XElement("LLAVE", c.LLAVE),
                                 new XElement("LLAVE_ALTERNA", c.LLAVE_ALTERNA),
                                 new XElement("IDEPLANPOL", c.IDEPLANPOL),
                                 new XElement("PLANPOL", c.PLANPOL),
                                 new XElement("CERTIFICADO", c.CERTIFICADO),
                                 new XElement("CERTIFICADO_REF", c.CERTIFICADO_REF),
                                 new XElement("FECHA_INICIAL_CUENTA_CREDITO", c.FECHA_INICIAL_CUENTA_CREDITO),
                                 new XElement("FECHA_FINAL_CUENTA_CREDITO", c.FECHA_FINAL_CUENTA_CREDITO),
                                 new XElement("VIGENCIA_INICIAL", c.VIGENCIA_INICIAL),
                                 new XElement("VIGENCIA_FINAL", c.VIGENCIA_FINAL),
                                 new XElement("FECHA_INICIO_COBRO", c.FECHA_INICIO_COBRO),
                                 new XElement("PRIMER_PAGO_REALIZADO", c.PRIMER_PAGO_REALIZADO),
                                 new XElement("FORMA_PAGO", c.FORMA_PAGO),
                                 new XElement("PAGOS", c.PAGOS),
                                 new XElement("MONTO_ASEGURADO", c.MONTO_ASEGURADO),
                                 new XElement("PRIMA_COBRAR", c.PRIMA_COBRAR),
                                 new XElement("TIPO_CUENTA", c.TIPO_CUENTA),
                                 new XElement("NUMERO_CUENTA", c.NUMERO_CUENTA),
                                 new XElement("ENTIDAD_CUENTA", c.ENTIDAD_CUENTA),
                                 new XElement("CODIGO_ENTIDAD_TARJETA", c.CODIGO_ENTIDAD_TARJETA),
                                 new XElement("IDEPOL", c.IDEPOL),
                                 new XElement("NUMCERT", c.NUMCERT),
                                 new XElement("USUARIO", c.USUARIO),
                                 new XElement("FECHA", c.FECHA),
                                 new XElement("LINEA", c.LINEA),
                                 new XElement("LAYOUT", c.LAYOUT),
                                 new XElement("STSCA", c.STSCA),
                                 new XElement("SUMA_VALIDA", c.SUMA_VALIDA),
                                 new XElement("TIPOVEH", c.TIPOVEH),
                                 new XElement("CODMARCA", c.CODMARCA),
                                 new XElement("CODMODELO", c.CODMODELO),
                                 new XElement("CODVERSION", c.CODVERSION),
                                 new XElement("TIPO_PLACA", c.TIPO_PLACA),
                                 new XElement("NUMPLACA", c.NUMPLACA),
                                 new XElement("ANOVEH", c.ANOVEH),
                                 new XElement("COLOR", c.COLOR),
                                 new XElement("SERIALCARROCERIA", c.SERIALCARROCERIA),
                                 new XElement("SERIALMOTOR", c.SERIALMOTOR),
                                 new XElement("USO", c.USO),
                                 new XElement("TITULO", c.TITULO),
                                 new XElement("EXCESO_RC", c.EXCESO_RC),
                                 new XElement("SECCION_III", c.SECCION_III),
                                 new XElement("NUMPUESTOS", c.NUMPUESTOS),
                                 new XElement("MOD_PAGOS", c.MOD_PAGOS),
                                 new XElement("SUMAASEGURADA", c.SUMAASEGURADA),
                                 new XElement("VIGENCIA_FINAL_EMITIDA", c.VIGENCIA_FINAL_EMITIDA),
                                 new XElement("NUMERO_CUENTA_VENCE", c.NUMERO_CUENTA_VENCE),
                                 new XElement("TIPO_CTA_PAGO", c.TIPO_CTA_PAGO),
                                 new XElement("NUMERO_CTA_PAGO", c.NUMERO_CTA_PAGO),
                                 new XElement("ENTIDAD_CTA_PAGO", c.ENTIDAD_CTA_PAGO),
                                 new XElement("CODIGO_ENTIDAD_TARJETA_PAGO", c.CODIGO_ENTIDAD_TARJETA_PAGO),
                                 new XElement("NUMPOL", c.NUMPOL),
                                 new XElement("ALARMA", c.ALARMA),
                                 new XElement("COD_AGENCIA", c.COD_AGENCIA),
                                 new XElement("COD_EJECUTIVO", c.COD_EJECUTIVO),
                                 new XElement("CODAGENCIADISTRIBUIDOR", c.CODAGENCIADISTRIBUIDOR),
                                 new XElement("MONTO_CUOTA", c.MONTO_CUOTA),
                                 new XElement("CORPORATIVO", c.CORPORATIVO),
                                 new XElement("NOMBRE_EJECUTIVO", c.NOMBRE_EJECUTIVO),
                                 new XElement("AFILIADA", c.AFILIADA),
                                 new XElement("CATEGORIA", c.CATEGORIA),
                                 new XElement("CLASEBIEN", c.CLASEBIEN),
                                 new XElement("CODBIEN", c.CODBIEN),
                                 new XElement("MARCA", c.MARCA),
                                 new XElement("MODELO", c.MODELO),
                                 new XElement("SERIE", c.SERIE),
                                 new XElement("DESCRIP", c.DESCRIP),
                                 new XElement("DIRECC", c.DIRECC),
                                 new XElement("PAIS_RIESGO", c.PAIS_RIESGO),
                                 new XElement("DEPARTAMENTO_RIESGO", c.DEPARTAMENTO_RIESGO),
                                 new XElement("MUNICIPIO_RIESGO", c.MUNICIPIO_RIESGO),
                                 new XElement("ALDEA_LOCALIDAD_ZONA_RIESGO", c.ALDEA_LOCALIDAD_ZONA_RIESGO),
                                 new XElement("CODMOTVEXC", c.CODMOTVEXC),
                                 new XElement("TIPO_OPERACION", c.TIPO_OPERACION),
                                 new XElement("CUOTAS_COBRADAS", c.CUOTAS_COBRADAS),
                                 new XElement("MONTO_COBRADO", c.MONTO_COBRADO),
                                 new XElement("CODINTER", c.CODINTER),
                                 new XElement("COBERTURAS",                                                         
                                                            from x in pCoberturas
                                                            select new XElement("COBERTURA",
                                                            new XElement("RAMO", x.RAMO),
                                                            new XElement("CODIGO", x.CODIGO),
                                                            new XElement("SUMAASEGURADA", x.SUMAASEGURADA),
                                                            new XElement("PRIMA", x.PRIMA),
                                                            new XElement("IDEPROCESO", x.IDEPROCESO),
                                                            new XElement("LLAVE", x.LLAVE),
                                                            new XElement("LLAVE_ALTERNA", x.LLAVE_ALTERNA),
                                                            new XElement("LINEA", x.LINEA),
                                                            new XElement("TASA", x.TASA))
                                                            ),
                                new XElement("RECARGOS",
                                                            from y in pRecargos
                                                            select new XElement("RECARGO",
                                                            new XElement("CODIGO", y.CODIGO),
                                                            new XElement("TIPO", y.TIPO),
                                                            new XElement("PORCENTAJE", y.PORCENTAJE),
                                                            new XElement("IDEPROCESO", y.IDEPROCESO),
                                                            new XElement("LLAVE", y.LLAVE),
                                                            new XElement("LLAVE_ALTERNA", y.LLAVE_ALTERNA),
                                                            new XElement("LINEA", y.LINEA),
                                                            new XElement("MONTO", y.MONTO))
                                                            )
                            ));

            return xmlfromLINQ.ToString();
        }
        public string generarXML_CA_BIEN(List<CA_BIENES_CERTIFICADO> datosBienes)
        {
            var xmlfromLINQ = new XElement("PROCESO",
                        from c in datosBienes
                        select new XElement("BIENES",
                            new XElement("IDEPROCESO", c.IDEPROCESO),
                            new XElement("LLAVE", c.LLAVE),
                            new XElement("LLAVE_ALTERNA", c.LLAVE_ALTERNA),
                            new XElement("LINEA", c.LINEA),
                            new XElement("LAYOUT", c.LAYOUT),
                            new XElement("CLASEBIEN", c.CLASEBIEN),
                            new XElement("CODBIEN", c.CODBIEN),
                            new XElement("MARCA", c.MARCA),
                            new XElement("MODELO", c.MODELO),
                            new XElement("SERIE", c.SERIE),
                            new XElement("DESCRIP", c.DESCRIP),
                            new XElement("DIRECC", c.DIRECC),
                            new XElement("PAIS_RIESGO", c.PAIS_RIESGO),
                            new XElement("DEPARTAMENTO_RIESGO", c.DEPARTAMENTO_RIESGO),
                            new XElement("MUNICIPIO_RIESGO", c.MUNICIPIO_RIESGO),
                            new XElement("ALDEA_LOCALIDAD_ZONA_RIESGO", c.ALDEA_LOCALIDAD_ZONA_RIESGO),
                            new XElement("MONTO_ASEGURADO", c.MONTO_ASEGURADO),
                            new XElement("PRIMA_COBRAR", c.PRIMA_COBRAR),
                            new XElement("STSCA", c.STSCA)
                            ));

            return xmlfromLINQ.ToString();
        }
        public string CARGA_LOTE(int pIdProceso, string pCodUsr, string pXML_Cert, string pXML_Aseg, string pXML_Bien, string pWebLogic = "S")
        {
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;


                if (pXML_Bien != string.Empty)
                {
                    cmd.CommandText = "pr_oper_sas.CARGA_LOTE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p1 = new OracleParameter("pIdProceso", OracleDbType.Int32, 100);
                    OracleParameter p2 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 100);
                    OracleParameter p3 = new OracleParameter("pXML_Cert", OracleDbType.Clob);
                    OracleParameter p4 = new OracleParameter("pXML_Aseg", OracleDbType.Clob);
                    OracleParameter p5 = new OracleParameter("pXML_Bien", OracleDbType.Clob);
                    OracleParameter p6 = new OracleParameter("pWebLogic", OracleDbType.Varchar2, 100);
                    OracleParameter p7 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                    p1.Value = pIdProceso;
                    p2.Value = pCodUsr;
                    p3.Value = pXML_Cert;
                    p4.Value = pXML_Aseg;
                    p5.Value = pXML_Bien;
                    p6.Value = pWebLogic;
                    p7.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p7);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                }
                else
                {
                    cmd.CommandText = "pr_oper_sas.CARGA_LOTE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    OracleParameter p1 = new OracleParameter("pIdProceso", OracleDbType.Int32, 100);
                    OracleParameter p2 = new OracleParameter("pCodUsr", OracleDbType.Varchar2, 100);
                    OracleParameter p3 = new OracleParameter("pXML_Cert", OracleDbType.Clob);
                    OracleParameter p4 = new OracleParameter("pXML_Aseg", OracleDbType.Clob);
                    OracleParameter p5 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                    p1.Value = pIdProceso;
                    p2.Value = pCodUsr;
                    p3.Value = pXML_Cert;
                    p4.Value = pXML_Aseg;
                    p5.Direction = ParameterDirection.ReturnValue;

                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                }


                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["Return_Value"].Value.ToString();

                conexion.Close();
            }
            catch (Exception ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public void CARGA_ARCHXML(int IDEPROCESO)
        {
            //int resultado = 0;

            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_CARGA_AUTOMATICA.CARGA_ARCHXML";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdProceso", OracleDbType.Int32);
                //OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Int32);

                p1.Value = IDEPROCESO;
                //p2.Direction = ParameterDirection.ReturnValue;

                // cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                conexion.Close();

                //resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            //return resultado;
        }
        public bool esPagoSalteado(int requerimiento)
        {
            int resultado = 0;
            bool pagoSalteado = false;

            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_CONVENIO_BANCARIO.Val_Pago_Salteado";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdeFact", OracleDbType.Int32);
                OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.Int32);

                p1.Value = requerimiento;
                p2.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                resultado = int.Parse(cmd.Parameters["Return_Value"].Value.ToString());

                conexion.Close();

                if (resultado == 1)
                {
                    pagoSalteado = true;
                }
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return pagoSalteado;
        }
        public string CREAR(int IDEPROCESO)
        {
            string error = string.Empty;
            OracleConnection conexion = new OracleConnection();
            Conexiones clsConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clsConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_CARGA_AUTOMATICA.CREAR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nProceso", OracleDbType.Int32);

                p1.Value = IDEPROCESO;

                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                conexion.Close();

                error = verificarErrorCARGA(IDEPROCESO);

            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }
            return error;
        }
        private string verificarErrorCARGA(int IDPROCESO)
        {
            string descripcionERROR = string.Empty;

            string query = "select * from ca_bitacora where ideproceso = " + IDPROCESO;

            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            try
            {
                adapter.Fill(datos);

                if (datos.Rows.Count == 1)
                {
                    descripcionERROR = datos.Rows[0]["ERROR"].ToString();
                }

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                descripcionERROR = ex.Message;
            }

            return descripcionERROR;
        }
        public DataTable ObtenerInformacionCoberturasAdicionales(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pSumaAsegurada)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();

            string query = " SELECT distinct t.codramo, INITCAP(pr_cobert_ramo.descripcion(t.codramo, " +
                                                              " t.codcobert,'N')) NOMBRE,T.CODCOBERT, count(*) FROM TARIFA_OTROS_TIPO_VEH_DET t " +
                                                              " WHERE t.CODPROD ='" + pCodProd + "' " +
                                                              " And t.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And t.REVPLAN = '" + pRevPlan + "' " +
                                                              " And t.TIPOVEH = '" + pTipoVeh + "' " +
                                                              " AND t.CODCOBERT NOT IN ('AUEA') " +
                                                              " and '" + pSumaAsegurada + "' BETWEEN T.MTOSUMAMIN(+) and T.MTOSUMAMAX(+) " +
                                                              "  group by t.codramo, INITCAP(pr_cobert_ramo.descripcion(t.codramo, " +
                                                              " t.codcobert,'N')),T.CODCOBERT  having count(*) > 1 " +
                                                              " UNION " +
                                                              " SELECT distinct t.codramo, INITCAP(pr_cobert_ramo.descripcion(t.codramo, " +
                                                              " t.codcobert,'N')) NOMBRE,T.CODCOBERT, count(*) FROM TARIFA_OTROS_TIPO_VEH t " +
                                                              " WHERE t.CODPROD ='" + pCodProd + "' " +
                                                              " And t.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And t.REVPLAN = '" + pRevPlan + "' " +
                                                              " And t.TIPOVEH = '" + pTipoVeh + "' " +
                                                              " AND t.CODCOBERT NOT IN ('AUEA') " +
                                                              "  group by t.codramo, INITCAP(pr_cobert_ramo.descripcion(t.codramo, " +
                                                              " t.codcobert,'N')),T.CODCOBERT  having count(*) > 1 " +
                                                              " UNION " +
                                                              " SELECT distinct cr.codramoplan, " +
                                                              " INITCAP(pr_cobert_ramo.descripcion(cr.codramoplan, cr.codcobert,'N')), " +
                                                              " cr.CodCobert, 1 " +
                                                              " FROM cobert_plan_prod cr " +
                                                              " WHERE cr.codprod = '" + pCodProd + "' " +
                                                              " AND cr.codplan = '" + pCodPlan + "' " +
                                                              " AND cr.revplan = '" + pRevPlan + "' " +
                                                              " AND nvl(cr.indcobertoblig,'N') = 'N' " +
                                                              " AND cr.CODCOBERT NOT IN ('AUEA') " +
                                                              " AND not exists (SELECT 1 " +
                                                              " FROM TARIFA_OTROS_TIPO_VEH " +
                                                              " WHERE codprod = cr.codprod " +
                                                              " AND codplan = cr.codplan " +
                                                              " AND revplan = cr.revplan " +
                                                              " AND codcobert = cr.codcobert) ";

            string query2 = "SELECT DISTINCT t.codramo, INITCAP(pr_cobert_ramo.descripcion(t.codramo, " +
           "t.codcobert,'N')) NOMBRE,  T.CODCOBERT,  COUNT(*) FROM TARIFA_OTROS_TIPO_VEH_DET t " +
           "WHERE t.CODPROD = '" + pCodProd + "' " +
           "AND t.CODPLAN        = '" + pCodPlan + "' " +
           "AND t.REVPLAN        = '" + pRevPlan + "' " +
           "AND t.TIPOVEH        = '" + pTipoVeh + "' " +
           "AND t.CODCOBERT NOT IN ('AUEA') " +
           "AND " + pSumaAsegurada + " BETWEEN T.MTOSUMAMIN(+) AND T.MTOSUMAMAX(+) " +
           "GROUP BY t.codramo,  INITCAP(pr_cobert_ramo.descripcion(t.codramo, " +
           "t.codcobert,'N')),  T.CODCOBERT HAVING COUNT(*) > 1 " +
           "UNION " +
           "SELECT DISTINCT t.codramo, " +
           "INITCAP(pr_cobert_ramo.descripcion(t.codramo, t.codcobert,'N')) NOMBRE,  T.CODCOBERT,  COUNT(*) " +
           " FROM TARIFA_OTROS_TIPO_VEH t, cobert_plan_prod cpp " +
           "WHERE t.CODPROD      = '" + pCodProd + "' " +
           "AND t.CODPLAN        = '" + pCodPlan + "' " +
           "AND t.REVPLAN        = '" + pRevPlan + "' " +
           "AND t.TIPOVEH        = '" + pTipoVeh + "' " +
           "AND t.CODCOBERT NOT IN ('AUEA') " +
           "AND t.codprod = cpp.codprod " +
           "AND t.codplan = cpp.codplan " +
           "AND t.revplan = cpp.revplan " +
           "AND t.codcobert = cpp.codcobert " +
           "GROUP BY t.codramo, INITCAP(pr_cobert_ramo.descripcion(t.codramo, t.codcobert,'N')), " +
           "T.CODCOBERT,  nvl(cpp.indcobertoblig,'N') " +
           "HAVING COUNT(*) > 1 OR nvl(cpp.indcobertoblig,'N') = 'N' " +
           "UNION " +
           "SELECT DISTINCT cr.codramoplan, " +
           "INITCAP(pr_cobert_ramo.descripcion(cr.codramoplan, cr.codcobert,'N')), " +
           "cr.CodCobert, 1 " +
           "FROM cobert_plan_prod cr " +
           "WHERE cr.codprod               = '" + pCodProd + "' " +
           "AND cr.codplan                 = '" + pCodPlan + "' " +
           "AND cr.revplan                 = '" + pRevPlan + "' " +
           "AND NVL(cr.indcobertoblig,'N') = 'N' " +
           "AND cr.CODCOBERT NOT          IN ('AUEA') " +
           "AND NOT EXISTS " +
           "(SELECT 1  FROM TARIFA_OTROS_TIPO_VEH  " +
           "WHERE codprod = cr.codprod " +
           "AND codplan   = cr.codplan " +
           "AND revplan   = cr.revplan " +
           "AND codcobert = cr.codcobert  )";

            OracleDataAdapter adapter = new OracleDataAdapter(query2, conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInformacionRecargosAdicionales(string pCodProd, string pCodPlan, string pRevPlan)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT rec.CODRECADCTO,r.DESCRECADCTO,r.MTOLIMRECADCTO,rec.PORCRECADCTO,NVL(rec.INDOBLIG,'N') INDOBLIG  " +
                                                              " FROM reca_dcto_plan_prod rec, reca_dcto r " +
                                                              " WHERE rec.CODPROD ='" + pCodProd + "' " +
                                                              " And rec.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And rec.REVPLAN = '" + pRevPlan + "' " +
                                                              " AND rec.TIPORECADCTO = 'R' " +
                                                              " and rec.CODRECADCTO = r.CODRECADCTO " +
                                                              " and rec.TIPORECADCTO = r.TIPORECADCTO " +
                                                              " and NVL(rec.INDOBLIG,'N') != 'S' ", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }
        public DataTable ObtenerInforomacionRCPorROW(string pCodProd, string pCodPlan, string pRevPlan, 
            string pTipoVeh, string pCodModelo, string pCodMarca,string pSumaAsegurada, string pSumaCobert)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_PRIMAS_RC";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCodProd", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("cCodPlan", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("cRevPlan", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cCodRamo", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("cTipoVeh", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cCodModelo", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodMarca", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("nSumaCob", OracleDbType.Int32);
            OracleParameter p9 = new OracleParameter("nValAuto", OracleDbType.Int32);
            OracleParameter p10 = new OracleParameter("cCodCobert", OracleDbType.Varchar2);
            OracleParameter p11 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = "AUR1";
            p5.Value = pTipoVeh;
            p6.Value = pCodModelo;
            p7.Value = pCodMarca;
            p8.Value = pSumaCobert;
            p9.Value = float.Parse(pSumaAsegurada);
            p10.Value = "AUR4";

            p11.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p11);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p10);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosCliente;
        }
        /*public DataTable ObtenerInforomacionRCPorROW(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pSumaAsegurada,string pRow)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Distinct RowNum, T.SUMAASEG,NVL(D.PRIMA,T.PRIMA) PRIMA, NVL(D.TASA,T.TASA) TASA,T.CODCOBERT,T.CODRAMO " +
                                                              " From TARIFA_RCV_TIPO_VEH T, PLAN_PROD P, TARIFA_RCV_TIPO_VEH_DET D " +
                                                              " Where P.CODPROD = '" + pCodProd + "' " +
                                                              " And P.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And P.REVPLAN = '" + pRevPlan + "' " +
                                                              " And T.CODPROD = P.CODPROD " +
                                                              " And T.CODPLAN = P.CODPLAN " +
                                                              " And T.REVPLAN = P.REVPLAN " +
                                                              " and D.CODPROD (+)= T.CODPROD " +
                                                              " And D.CODPLAN  (+)= T.CODPLAN " +
                                                              " And D.REVPLAN (+)= T.REVPLAN " +
                                                              " and D.CODCOBERT (+)= T.CODCOBERT " +
                                                              " and D.CODTARIF (+)= T.CODTARIF " +
                                                              " And T.TIPOVEH = '" + pTipoVeh + "'  " +
                                                              " and D.TIPOVEH (+)= '" + pTipoVeh + "'  " +
                                                              " and '" + pSumaAsegurada + "' BETWEEN D.MTOSUMAMIN(+) and D.MTOSUMAMAX(+) " +
                                                              " and T.SUMAASEG ='" + pRow + "'" +
                                                              " Order By SUMAASEG", conexion);

            try
            {
                adapter.Fill(datos);

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }

            return datos;
        }*/
        public DataTable ObtenerInformacionOcupanteAUSUNO(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pSumaAsegurada,
                                                          string pCodSubPlan, string pCodModelo, string pCodMarca)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_PRIMAS_APOV";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCodProd", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("cCodPlan", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("cRevPlan", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cCodRamoCert", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("cTipoVeh", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cCodModelo", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodMarca", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("nValAuto", OracleDbType.BinaryFloat);
            OracleParameter p9 = new OracleParameter("cCodCobert", OracleDbType.Varchar2);
            OracleParameter p10 = new OracleParameter("cCodPlanApov", OracleDbType.Varchar2);
            OracleParameter p11 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = "AUR1";
            p5.Value = pTipoVeh;
            p6.Value = pCodModelo;
            p7.Value = pCodMarca;
            p8.Value = pSumaAsegurada;
            p9.Value = "AUS1";
            p10.Value = pCodSubPlan;

            p11.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p11);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p10);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosCliente;
        }
        public DataTable ObtenerInformacionOcupanteAUSDOS(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pSumaAsegurada,
                                                          string pCodSubPlan, string pCodModelo, string pCodMarca)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_PRIMAS_APOV";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("cCodProd", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("cCodPlan", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("cRevPlan", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("cCodRamoCert", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("cTipoVeh", OracleDbType.Varchar2);
            OracleParameter p6 = new OracleParameter("cCodModelo", OracleDbType.Varchar2);
            OracleParameter p7 = new OracleParameter("cCodMarca", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("nValAuto", OracleDbType.BinaryFloat);
            OracleParameter p9 = new OracleParameter("cCodCobert", OracleDbType.Varchar2);
            OracleParameter p10 = new OracleParameter("cCodPlanApov", OracleDbType.Varchar2);
            OracleParameter p11 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = "AUR1";
            p5.Value = pTipoVeh;
            p6.Value = pCodModelo;
            p7.Value = pCodMarca;
            p8.Value = float.Parse(pSumaAsegurada);
            p9.Value = "AUS2";
            p10.Value = pCodSubPlan;

            p11.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p11);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p10);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
                cmd.Connection.Close();
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return datosCliente;
        }
        public DataTable ObtenerIdepol(string pIdeProceso)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter("SELECT IDEPOL FROM CA_CERTIFICADO WHERE IDEPROCESO ='" + pIdeProceso + "'", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public int ObtenerIdepol(int ideProceso)
        {
            DataTable datos = new DataTable();
            int idepol = 0;

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();

            string query = "SELECT IDEPOL FROM CA_CERTIFICADO WHERE IDEPROCESO =" + ideProceso;

            OracleDataAdapter adapter = new OracleDataAdapter(query, conexion);

            adapter.Fill(datos);

            if (datos.Rows.Count == 1 )
            {
                idepol = int.Parse(datos.Rows[0]["IDEPOL"].ToString());
            }

            conexion.Close();

            return idepol;
        }
        public bool ActivarPolizaCotizacion(int pIdepol)
        {
            bool respuesta = false;
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_POLIZA.ACTIVAR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nidepol", OracleDbType.Int32);
                OracleParameter p2 = new OracleParameter("ctipomov", OracleDbType.Varchar2, 32767);
                OracleParameter p3 = new OracleParameter("ctipoemi", OracleDbType.Varchar2, 32767);
                OracleParameter p4 = new OracleParameter("Return_Value", OracleDbType.Varchar2, 32767);

                p1.Value = pIdepol;
                p2.Value = "T";
                p3.Value = "E";
                p4.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);


                cmd.ExecuteNonQuery();

                resultado = cmd.Parameters["Return_Value"].Value.ToString();

                conexion.Close();

                respuesta = true;

            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return respuesta;
        }
        /// <summary>
        /// Esta funcion sirve para generar financiamiento de la cotizacion
        /// Julio Luna
        /// 30/06/2016
        /// </summary>
        /// <param name="pIdepol"></param>
        /// <returns></returns>
        public bool GenerarCondFinanciamientoTCotizacion(int pIdepol)
        {
            bool respuesta = false;
            string resultado = string.Empty;

            OracleConnection conexion = new OracleConnection();
            Conexiones clConexion = new Conexiones();

            try
            {
                OracleCommand cmd = new OracleCommand();
                conexion = clConexion.abrirConexionOracleAcsel();
                cmd.Connection = conexion;
                cmd.CommandText = "PR_COND_FINANCIAMIENTO_T.GENERAR";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter p1 = new OracleParameter("nIdePol", OracleDbType.Int32);

                p1.Value = pIdepol;

                cmd.Parameters.Add(p1);

                cmd.ExecuteNonQuery();

                conexion.Close();

                respuesta = true;
            }
            catch (OracleException ex)
            {
                conexion.Close();
            }
            return respuesta;
        }
        public DataTable DevolverPrimeraCuota(string pIdepol)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT MTOGIROLOCAL FROM GIROS_FINANCIAMIENTO_T GIR, COND_FINANCIAMIENTO_T CON " +
                                                              " WHERE GIR.NUMFINANC = CON.NUMFINANC  " +
                                                              " AND GIR.NUMGIRO = 1  " +
                                                              " AND CON.IDEPOL = " + pIdepol, conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerGastosEmision(string pIdepol, string pNumeroCertificado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Sum(MTODETGIROLOCAL) As VALOR " +
                                                              " From DET_GIRO_FIN_T D, COND_FINANCIAMIENTO_T C " +
                                                              " Where C.IDEPOL = ' " + pIdepol + "'" +
                                                              " And C.NUMCERT = ' " + pNumeroCertificado + "'" +
                                                              " And D.NUMFINANC = C.NUMFINANC " +
                                                              " And D.CODCPTOACRE = 'GTOEMI' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable obtenerDocumentosPoliza(string pIdepol)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT * FROM DOCS_GENERADOS_POLIZA " +
                                                              " WHERE IDEPOL = '" + pIdepol + "' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerRecargoPorFinanciamiento(string pIdepol, string pNumeroCertificado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Sum(MTODETGIROLOCAL) As VALOR " +
                                                              " From DET_GIRO_FIN_T D, COND_FINANCIAMIENTO_T C " +
                                                              " Where C.IDEPOL = ' " + pIdepol + "'" +
                                                              " And C.NUMCERT = ' " + pNumeroCertificado + "'" +
                                                              " And D.NUMFINANC = C.NUMFINANC " +
                                                              " And D.CODCPTOACRE = 'RECFIN' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerIVA(string pIdepol, string pNumeroCertificado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Sum(MTODETGIROLOCAL) As VALOR " +
                                                              " From DET_GIRO_FIN_T D, COND_FINANCIAMIENTO_T C " +
                                                              " Where C.IDEPOL = ' " + pIdepol + "'" +
                                                              " And C.NUMCERT = ' " + pNumeroCertificado + "'" +
                                                              " And D.NUMFINANC = C.NUMFINANC " +
                                                              " And D.CODCPTOACRE = 'IMPIVA' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerImpuestoBomberos(string pIdepol, string pNumeroCertificado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Sum(MTODETGIROLOCAL) As VALOR " +
                                                              " From DET_GIRO_FIN_T D, COND_FINANCIAMIENTO_T C " +
                                                              " Where C.IDEPOL = ' " + pIdepol + "'" +
                                                              " And C.NUMCERT = ' " + pNumeroCertificado + "'" +
                                                              " And D.NUMFINANC = C.NUMFINANC " +
                                                              " And D.CODCPTOACRE = 'IMPBOM' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerOtrosIngresos(string pIdepol, string pNumeroCertificado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Sum(MTODETGIROLOCAL) As VALOR " +
                                                              " From DET_GIRO_FIN_T D, COND_FINANCIAMIENTO_T C " +
                                                              " Where C.IDEPOL = ' " + pIdepol + "'" +
                                                              " And C.NUMCERT = ' " + pNumeroCertificado + "'" +
                                                              " And D.NUMFINANC = C.NUMFINANC " +
                                                              " And D.CODCPTOACRE = 'OTRING' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerPrimaNeta(string pIdepol, string pNumeroCertificado)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" Select Sum(MTODETGIROLOCAL) As VALOR " +
                                                              " From DET_GIRO_FIN_T D, COND_FINANCIAMIENTO_T C " +
                                                              " Where C.IDEPOL = ' " + pIdepol + "'" +
                                                              " And C.NUMCERT = ' " + pNumeroCertificado + "'" +
                                                              " And D.NUMFINANC = C.NUMFINANC " +
                                                              " And D.CODCPTOACRE = 'RECIBO' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable DevolverInformacionPorcentajesCuotasPlanAuto(string pCodProd, string pCodPlan, string pRevPlan)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT CUOTAS, cf.PORCCPTOF PORCENTAJE " +
                                                              " FROM 	CONC_FINANCIAMIENTO cf, plan_financiamiento_plan_prod  pf   " +
                                                              " WHERE cf.CODCPTOACRE = 'RECFIN'  " +
                                                              " And pf.CodProd='" + pCodProd + "'  " +
                                                              " AND pf.CodPlan ='" + pCodPlan + "' " +
                                                              " AND pf.RevPlan ='" + pRevPlan + "' " +
                                                              " and cf.CODPLAN = pf.CODPLANFINAN  " +
                                                              " AND cf.MODPLAN = pf.MODPLAN " +
                                                              " ORDER BY 1 ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable DevolverPrimaTotal(string pIdeProceso, string pNumeroCertificado)
        {
            DataTable primaNeta = new DataTable();
            DataTable gastosEmision = new DataTable();
            DataTable recargosPorFinanciamiento = new DataTable();
            DataTable iVA = new DataTable();
            DataTable impuestoBomberos = new DataTable();
            DataTable otrosIngresos = new DataTable();
            DataTable resultado = new DataTable();
            DataTable idePol = new DataTable();

            double primaNetaValor = 0.0;
            double gastosEmisionValor = 0.0;
            double recargosPorFinanciamientoValor = 0.0;
            double iVAValor = 0.0;
            double impuestoBomberosValor = 0.0;
            double otrosIngresosValor = 0.0;

            idePol = ObtenerIdepol(pIdeProceso);

            primaNeta = ObtenerPrimaNeta(idePol.Rows[0]["IDEPOL"].ToString(), pNumeroCertificado);
            gastosEmision = ObtenerGastosEmision(idePol.Rows[0]["IDEPOL"].ToString(), pNumeroCertificado);
            recargosPorFinanciamiento = ObtenerRecargoPorFinanciamiento(idePol.Rows[0]["IDEPOL"].ToString(), pNumeroCertificado);
            iVA = ObtenerIVA(idePol.Rows[0]["IDEPOL"].ToString(), pNumeroCertificado);
            impuestoBomberos = ObtenerImpuestoBomberos(idePol.Rows[0]["IDEPOL"].ToString(), pNumeroCertificado);
            otrosIngresos = ObtenerOtrosIngresos(idePol.Rows[0]["IDEPOL"].ToString(), pNumeroCertificado);

            resultado.Columns.Add("PRIMA_NETA", typeof(double));
            resultado.Columns.Add("GASTOS_EMISION", typeof(double));
            resultado.Columns.Add("RECARGOS_FINANCIAMIENTO", typeof(double));
            resultado.Columns.Add("IVA", typeof(double));
            resultado.Columns.Add("IMPUESTO_BOMBEROS", typeof(double));
            resultado.Columns.Add("OTROS_INGRESOS", typeof(double));
            resultado.Columns.Add("TOTAL_COTIZACION", typeof(double));

            if (primaNeta.Rows[0]["VALOR"].ToString() == string.Empty)
            {
                primaNetaValor = 0;
            }
            else
            {
                primaNetaValor = Convert.ToDouble(primaNeta.Rows[0]["VALOR"]) + 125;
            }

            if (gastosEmision.Rows[0]["VALOR"].ToString() == string.Empty)
            {
                gastosEmisionValor = 0;
            }
            else
            {
                gastosEmisionValor = Convert.ToDouble(gastosEmision.Rows[0]["VALOR"]);
            }

            if (recargosPorFinanciamiento.Rows[0]["VALOR"].ToString() == string.Empty)
            {
                recargosPorFinanciamientoValor = 0;
            }
            else
            {
                recargosPorFinanciamientoValor = Convert.ToDouble(recargosPorFinanciamiento.Rows[0]["VALOR"]);
            }

            if (iVA.Rows[0]["VALOR"].ToString() == string.Empty)
            {
                iVAValor = 0;
            }
            else
            {
                iVAValor = Convert.ToDouble(iVA.Rows[0]["VALOR"]);
            }

            if (impuestoBomberos.Rows[0]["VALOR"].ToString() == string.Empty)
            {
                impuestoBomberosValor = 0;
            }
            else
            {
                impuestoBomberosValor = Convert.ToDouble(impuestoBomberos.Rows[0]["VALOR"]);
            }

            if (otrosIngresos.Rows[0]["VALOR"].ToString() == string.Empty)
            {
                otrosIngresosValor = 0;
            }
            else
            {
                otrosIngresosValor = Convert.ToDouble(otrosIngresos.Rows[0]["VALOR"]);
            }

            double sumaCotizacion = primaNetaValor + gastosEmisionValor + recargosPorFinanciamientoValor + iVAValor + impuestoBomberosValor + otrosIngresosValor;

            resultado.Rows.Add(primaNetaValor, gastosEmisionValor, recargosPorFinanciamientoValor, iVAValor, impuestoBomberosValor, otrosIngresosValor, sumaCotizacion);
            return resultado;
        }
        public DataTable obtenerMontoPrimaTotalPorTipoPago(string pCodProd, string pCodPlan, string pRevPlan, string pTipoPago, 
                                                                string pCodPlanFracc, string pCodMoneda, string idepol)
        {
            DataTable datosCliente = new DataTable();            
            DataTable primaNeta = new DataTable();
            //DataTable Asistencia = new DataTable();
            int asistencia = 0;
            double primNeta = 0;

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            
            primaNeta = ObtenerPrimaNeta(idepol, "1");            
            
            //Asistencia = ObtenerInformacionAsistencia(pCodProd, pCodPlan, pRevPlan, pCodPlanFracc);
            //Segun comentario de Marlene Mora la asistencia se debe enviar 0 10/11/2016
            //Convert.ToInt32(Asistencia.Rows[0]["monto"].ToString());

            asistencia = 0;
            primNeta = Convert.ToDouble(primaNeta.Rows[0]["VALOR"].ToString());

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;            
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_MONTOS_PRIMA_TOTAL";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pCODPROD", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("pCODPLAN", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("pREVPLAN", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("pTIPOCUOTA", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("pPRIMANETA", OracleDbType.Double);
            OracleParameter p6 = new OracleParameter("pASISTENCIA", OracleDbType.Int32);
            OracleParameter p7 = new OracleParameter("cCODMONEDA", OracleDbType.Varchar2);
            OracleParameter p8 = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter p9 = new OracleParameter("pNUMCERT", OracleDbType.Int32);
            OracleParameter p10 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = pTipoPago;
            p5.Value = primNeta;
            p6.Value = asistencia;
            p7.Value = pCodMoneda;
            p8.Value = idepol;
            p9.Value = "1";

            p10.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p10);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);
            cmd.Parameters.Add(p9);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex; 
            }

            cmd.Connection.Close();

            return datosCliente;
        }
        public DataTable obtenerMontoPrimaGenerales(string pCodProd, string pCodPlan, 
                                                    string pRevPlan, string pTipoPago, int pIdePol, 
                                                    string pCodPlanFracc, string pCodMoneda)
        {
            DataTable datosCliente = new DataTable();            
            DataTable datosPrimaNeta = new DataTable();
            double primaNeta = 0;

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            
            datosPrimaNeta = ObtenerPrimaNeta(pIdePol.ToString(), "1");
            
            primaNeta = Convert.ToDouble(datosPrimaNeta.Rows[0]["VALOR"].ToString());

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_MONTOS_PRIMA_TOTGEN";
            cmd.CommandType = CommandType.StoredProcedure;

            OracleParameter p1 = new OracleParameter("pCODPROD", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("pCODPLAN", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("pREVPLAN", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("pTIPOCUOTA", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("pPRIMANETA", OracleDbType.Double);
            OracleParameter p6 = new OracleParameter("pIDEPOL", OracleDbType.Int32);
            OracleParameter p7 = new OracleParameter("pNUMCERT", OracleDbType.Int32);
            OracleParameter p8 = new OracleParameter("cCODMONEDA", OracleDbType.Varchar2);
            OracleParameter p9 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = pTipoPago;
            p5.Value = primaNeta;
            p6.Value = pIdePol;
            
            //Julio Luna 27/07/2016
            //Se dejo quemado el numero de certificado por el momento
            p7.Value = "1";
            p8.Value = pCodMoneda;

            p9.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p9);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p7);
            cmd.Parameters.Add(p8);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());

            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            cmd.Connection.Close();

            return datosCliente;
        }
        public DataTable ObtenerInformacionAsistencia(string pCodProd, string pCodPlan, string pRevPlan, string pFormaPago)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT NVL(cf.mtocptof,0) monto " +
                                                              " FROM plan_financiamiento_plan_prod pf,   " +
                                                              " conc_financiamiento cf  " +
                                                              " where pf.CodProd='" + pCodProd + "'  " +
                                                              " AND pf.CodPlan ='" + pCodPlan + "' " +
                                                              " AND pf.RevPlan ='" + pRevPlan + "' " +
                                                              " AND pf.codplanfinan  = '" + pFormaPago + "' " +
                                                              " and cf.CODPLAN = pf.CODPLANFINAN  " +
                                                              " AND cf.MODPLAN = pf.MODPLAN " +
                                                              " AND cf.codgrupoacre  = 'OTRING'  " +
                                                              " AND cf.codcptoacre   = 'ASICAM' ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerInformacionFormaPago(string pCodProd, string pCodPlan, string pRevPlan)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" select distinct 'FRACCIONADO' DESCRIP, 'N' CODIGO from plan_financiamiento_plan_prod " +
                                                                " WHERE CodProd    ='" + pCodProd + "' " +
                                                                " AND CodPlan      ='" + pCodPlan + "' " +
                                                                " AND RevPlan      ='" + pRevPlan + "' " +
                                                                " AND VISA_CUOTA = 'N' " +
                                                                " UNION " +
                                                                " select distinct 'VISA-CUOTAS' DESCRIP, 'SV' CODIGO from plan_financiamiento_plan_prod " +
                                                                " WHERE CodProd    ='" + pCodProd + "' " +
                                                                " AND CodPlan      ='" + pCodPlan + "' " +
                                                                " AND RevPlan      ='" + pRevPlan + "' " +
                                                                " AND VISA_CUOTA = 'S' " +
                                                                " UNION " +
                                                                " select distinct 'CREDI-CUOTAS' DESCRIP, 'SC' CODIGO from plan_financiamiento_plan_prod " +
                                                                " WHERE CodProd    ='" + pCodProd + "' " +
                                                                " AND CodPlan      ='" + pCodPlan + "' " +
                                                                " AND RevPlan      ='" + pRevPlan + "' " +
                                                                " AND VISA_CUOTA = 'S'", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerTasaEquipoEspecial(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pSumaAsegurada)
        {
            DataTable datos = new DataTable();

            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();
            conexion = conexionOracle.abrirConexionOracleAcsel();
            OracleDataAdapter adapter = new OracleDataAdapter(" SELECT t.CODRAMO, t.SUMAASEG,MAXPORCSUM, INITCAP(r.DESCCOBERT) NOMBRE,NVL(D.PRIMA,T.PRIMA) PRIMA, NVL(D.TASA,T.TASA) TASA,T.CODCOBERT " +
                                                              " FROM TARIFA_OTROS_TIPO_VEH t, COBERT_RAMO r, TARIFA_OTROS_TIPO_VEH_DET D " +
                                                              " WHERE t.CODPROD ='" + pCodProd + "' " +
                                                              " And t.CODPLAN   = '" + pCodPlan + "' " +
                                                              " And t.REVPLAN = '" + pRevPlan + "' " +
                                                              " And t.TIPOVEH = '" + pTipoVeh + "' " +
                                                              " AND t.CODCOBERT = 'AUEA' " +
                                                              " and   t.CODCOBERT = r.CODCOBERT " +
                                                              " and t.CODRAMO = r.CODRAMO " +
                                                              " and D.CODPROD (+)= T.CODPROD  " +
                                                              " And D.CODPLAN  (+)= T.CODPLAN " +
                                                              " And D.REVPLAN (+)= T.REVPLAN " +
                                                              " and D.CODCOBERT (+)= T.CODCOBERT " +
                                                              " and D.CODTARIF (+)= T.CODTARIF " +
                                                              " and D.TIPOVEH (+)= '" + pTipoVeh + "' " +
                                                              " and '" + pSumaAsegurada + "' BETWEEN D.MTOSUMAMIN(+) and D.MTOSUMAMAX(+) " +
                                                              " ORDER BY 1,2,3 ", conexion);



            adapter.Fill(datos);

            conexion.Close();

            return datos;
        }
        public DataTable ObtenerDescripcionVehiculos(string pTipoVehiculo, string pCodigoMarca, string pCodigoModelo, string pAnio)
        {
            DataTable resultado = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter adapter = new OracleDataAdapter(" SELECT LV.DESCRIP,MAR.DESCMARCA,M.DESCMODELO, " + pAnio + " ANIO" +
                                                                  " FROM MODELO_VEH M, MARCA_VEH MAR, LVAL LV " +
                                                                  " WHERE MAR.CODMARCA = '" + pCodigoMarca + "' " +
                                                                  " AND M.CODMODELO = '" + pCodigoModelo + "' " +
                                                                  " AND M.CODMARCA = MAR.CODMARCA " +
                                                                  " AND LV.TIPOLVAL = 'TIPOVEH' " +
                                                                  " AND LV.CODLVAL = '" + pTipoVehiculo + "'", conexion);
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable obtenerInformacionDatosCotizacionOracle(int IdeProceso)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();

            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            //cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DATOS_CLI";
            cmd.CommandText = "PK_COTIZADOR_WEB.REP_COTIZACION_AUTO";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("nIdeProceso", OracleDbType.Int32);
            OracleParameter p2 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = IdeProceso;

            p2.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p1);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());
            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex; 
            }


            cmd.Connection.Close();

            return datosCliente;
        }
        public DataTable ObtenerBeneficios(string pIdPlan)
        {
            DataTable resultado = new DataTable();
            DataTable informacionPlan = new DataTable();
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {
                informacionPlan = ObtenerNombrePlan(pIdPlan);
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter adapter = new OracleDataAdapter(" SELECT * FROM COT_BENEFICIOS " +
                                                                  " WHERE CODPROD = '" + informacionPlan.Rows[0]["codprod"].ToString() + "' " +
                                                                  " AND CODPLAN = '" + informacionPlan.Rows[0]["codplan"].ToString() + "' " +
                                                                  " AND REVPLAN = '" + informacionPlan.Rows[0]["revplan"].ToString() + "' " +
                                                                  " ORDER BY IDBENEFICIO ", conexion);
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }

        public int obtenerCuotaXNumeroPago(string codProd, string codPlan, string revPlan, string codPlanFinan, string visaCuota)
        {
            DataTable resultado = new DataTable();
            int cuota = 0;
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter adapter = new OracleDataAdapter(" SELECT CUOTAS FROM plan_financiamiento_plan_prod " +
                                                                  " WHERE CODPROD = '" + codProd + "' " +
                                                                  " AND CODPLAN = '" + codPlan + "' " +
                                                                  " AND REVPLAN = '" + revPlan + "' " +
                                                                  " AND CODPLANFINAN = '" + codPlanFinan + "'"+ 
                                                                  " AND VISA_CUOTA = '" + visaCuota + "'", conexion);
                adapter.Fill(resultado);

                if (resultado.Rows.Count == 1)
                {
                    cuota = int.Parse(resultado.Rows[0]["CUOTAS"].ToString());
                }

                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return cuota;
        }
        public DataTable ObtenerDescuentos(string codProd, string codPlan, string revPlan)
        {
            DataTable resultado = new DataTable();            
            Conexiones conexionOracle = new Conexiones();
            OracleConnection conexion = new OracleConnection();

            try
            {                
                conexion = conexionOracle.abrirConexionOracleAcsel();
                OracleDataAdapter adapter = new OracleDataAdapter(" SELECT TIPOUSORECADCTO,CODRECADCTO FROM RECA_DCTO_PLAN_PROD " +
                                                                  " WHERE CODPROD = '" + codProd + "' " +
                                                                  " AND CODPLAN = '" + codPlan + "' " +
                                                                  " AND REVPLAN = '" + revPlan + "' " +
                                                                  " AND TIPORECADCTO = 'D' ", conexion);
                adapter.Fill(resultado);
                conexion.Close();
            }
            catch (OracleException ex)
            {
                conexion.Close();
                throw ex;
            }

            return resultado;
        }
        public DataTable ObtenerCoberturasAdicionalesPorCombo(string pCodProd, string pCodPlan, string pRevPlan, string pTipoVeh, string pValorVehiculo)
        {
            DataTable datosCliente = new DataTable();

            Conexiones clsConexion = new Conexiones();
            OracleConnection conexionOracle = new OracleConnection();
            OracleCommand cmd = new OracleCommand();
            conexionOracle = clsConexion.abrirConexionOracleAcsel();

            cmd.Connection = conexionOracle;
            //cmd.CommandText = "PK_COTIZADOR_WEB.FN_OBTENER_DATOS_CLI";
            cmd.CommandText = "PK_COTIZADOR_WEB.FN_REGRESAR_COBERTURAS_ADI";
            cmd.CommandType = CommandType.StoredProcedure;
            OracleParameter p1 = new OracleParameter("pCODPROD", OracleDbType.Varchar2);
            OracleParameter p2 = new OracleParameter("pCODPLAN", OracleDbType.Varchar2);
            OracleParameter p3 = new OracleParameter("pREVPLAN", OracleDbType.Varchar2);
            OracleParameter p4 = new OracleParameter("nTIPOVEH", OracleDbType.Varchar2);
            OracleParameter p5 = new OracleParameter("nVALORVEH", OracleDbType.Double);
            OracleParameter p6 = new OracleParameter("Return_Value", OracleDbType.RefCursor);

            p1.Value = pCodProd;
            p2.Value = pCodPlan;
            p3.Value = pRevPlan;
            p4.Value = pTipoVeh;
            p5.Value = pValorVehiculo;

            p6.Direction = ParameterDirection.ReturnValue;

            cmd.Parameters.Add(p6);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);

            try
            {
                datosCliente.Load(cmd.ExecuteReader());

            }
            catch (OracleException ex)
            {
                cmd.Connection.Close();
                throw ex;
            }


            cmd.Connection.Close();

            return datosCliente;
        }

        // MMora 11082016: Se agrega función que devuelve los intermediarios asignados al usuario para 
        //                  que pueda seleccionar para cotizacion
        public DataTable ObtenerIntermediariosXUsuario(string IdUsuario)
        {
            DataTable datos = new DataTable();
            Conexiones clConexiones = new Conexiones();
            MySqlConnection conexion = new MySqlConnection();
            
            conexion = clConexiones.abrirConexionMysql();
            MySqlDataAdapter adapter = new MySqlDataAdapter(" select i.codinter, concat(i.codinter, ' ','-',' ', i.nomcomercial) NomComercial" +
                                                            "   from intermediarios i, intermediario_x_usuario ixu " +
                                                            "   where i.codinter = ixu.codinter " +
                                                            "     and ixu.estado = TRUE" +
                                                            "     and ixu.id_usuario = " + IdUsuario +
                                                            "  UNION  " +
                                                            "select i.codinter, concat(i.codinter, ' ','-',' ', i.nomcomercial) " +
                                                            "   from intermediarios i, usuario u " +
                                                            "   where i.codinter = u.codigo_intermediario " +
                                                            "     and u.id_usuario = " + IdUsuario, conexion);

            try
            {
                adapter.Fill(datos);
                conexion.Close();
            }

            catch (MySqlException ex)
            {
                conexion.Close();
                throw ex;
            }
            return datos;
        }
                
        #endregion
    }

    
}
