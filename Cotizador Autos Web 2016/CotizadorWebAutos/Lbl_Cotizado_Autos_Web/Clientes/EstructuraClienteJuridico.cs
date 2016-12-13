using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Clientes
{
    public class EstructuraClienteJuridico
    {
        public string nombre_persona_juridica { get; set; }
        public string nit_persona_juridica { get; set; }
        public string primer_nombre_representante { get; set; }
        public string segundo_nombre_representante { get; set; }
        public string primer_apellido_representante { get; set; }
        public string segundo_apellido_representante { get; set; }
        public string tipo_identificacion_representante { get; set; }
        public string numero_identificacion_representante { get; set; }
        public string pais_emision_representante { get; set; }
        public string depto_emision_representante { get; set; }
        public string muni_emision_representante { get; set; }
        public string fecha_constitucion_empresa { get; set; }
        public string pais_origen_empresa { get; set; }
        public string actividad_economica_empresa { get; set; }
        public string direccion_empresa { get; set; }
        public string pais_direccion_empresa { get; set; }
        public string depto_direccion_empresa { get; set; }
        public string muni_direccion_empresa { get; set; }
        public string zona_direccion_empresa { get; set; }
        public string calle_direccion_empresa { get; set; }
        public string avenida_direccion_empresa { get; set; }
        public string numero_casa_direccion_empresa { get; set; }
        public string colonia_direccion_empresa { get; set; }
        public string edificio_direccion_empresa { get; set; }
        public string lote_direccion_empresa { get; set; }
        public string manzana_direccion_empresa { get; set; }
        public string sector_direccion_empresa { get; set; }
        public string esPep_juridico { get; set; }
        public string relacionPep_juridico { get; set; }
        public string asociadoPep_juridico { get; set; }
        public string correo_electronico_juridico { get; set; } 
    }
}
