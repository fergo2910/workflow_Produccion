using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lbl_Cotizado_Autos_Web.Estructuras
{
    public class CA_CERTIFICADO
    {
        public string IDEPROCESO { get; set; }
        public string LLAVE { get; set; }
        public string LLAVE_ALTERNA { get; set; }
        public string IDEPLANPOL { get; set; }
        public string PLANPOL { get; set; }
        public string CERTIFICADO { get; set; }
        public string CERTIFICADO_REF { get; set; }
        public string FECHA_INICIAL_CUENTA_CREDITO { get; set; }
        public string FECHA_FINAL_CUENTA_CREDITO { get; set; }
        public string VIGENCIA_INICIAL { get; set; }
        public string VIGENCIA_FINAL { get; set; }
        public string FECHA_INICIO_COBRO { get; set; }
        public string PRIMER_PAGO_REALIZADO { get; set; }
        public string FORMA_PAGO { get; set; }
        public string PAGOS { get; set; }
        public string MONTO_ASEGURADO { get; set; }
        public string PRIMA_COBRAR { get; set; }
        public string TIPO_CUENTA { get; set; }
        public string NUMERO_CUENTA { get; set; }
        public string ENTIDAD_CUENTA { get; set; }
        public string CODIGO_ENTIDAD_TARJETA { get; set; }
        public string IDEPOL { get; set; }
        public string NUMCERT { get; set; }
        public string USUARIO { get; set; }
        public string FECHA { get; set; }
        public string LINEA { get; set; }
        public string LAYOUT { get; set; }
        public string STSCA { get; set; }
        public string SUMA_VALIDA { get; set; }
        public string TIPOVEH { get; set; }
        public string CODMARCA { get; set; }
        public string CODMODELO { get; set; }
        public string CODVERSION { get; set; }
        public string TIPO_PLACA { get; set; }
        public string NUMPLACA { get; set; }
        public string ANOVEH { get; set; }
        public string COLOR { get; set; }
        public string SERIALCARROCERIA { get; set; }
        public string SERIALMOTOR { get; set; }
        public string USO { get; set; }
        public string TITULO { get; set; }
        public string EXCESO_RC { get; set; }
        public string SECCION_III { get; set; }
        public string NUMPUESTOS { get; set; }
        public string MOD_PAGOS { get; set; }
        public string SUMAASEGURADA { get; set; }
        public string VIGENCIA_FINAL_EMITIDA { get; set; }
        public string NUMERO_CUENTA_VENCE { get; set; }
        public string TIPO_CTA_PAGO { get; set; }
        public string NUMERO_CTA_PAGO { get; set; }
        public string ENTIDAD_CTA_PAGO { get; set; }
        public string CODIGO_ENTIDAD_TARJETA_PAGO { get; set; }
        public string NUMPOL { get; set; }
        public string ALARMA { get; set; }
        public string COD_AGENCIA { get; set; }
        public string COD_EJECUTIVO { get; set; }
        public string CODAGENCIADISTRIBUIDOR { get; set; }
        public string MONTO_CUOTA { get; set; }
        public string CORPORATIVO { get; set; }
        public string NOMBRE_EJECUTIVO { get; set; }
        public string AFILIADA { get; set; }
        public string CATEGORIA { get; set; }
        public string CLASEBIEN { get; set; }
        public string CODBIEN { get; set; }
        public string MARCA { get; set; }
        public string MODELO { get; set; }
        public string SERIE { get; set; }
        public string DESCRIP { get; set; }
        public string DIRECC { get; set; }
        public string PAIS_RIESGO { get; set; }
        public string DEPARTAMENTO_RIESGO { get; set; }
        public string MUNICIPIO_RIESGO { get; set; }
        public string ALDEA_LOCALIDAD_ZONA_RIESGO { get; set; }
        public string CODMOTVEXC { get; set; }
        public string TIPO_OPERACION { get; set; }
        public string CUOTAS_COBRADAS { get; set; }
        public string MONTO_COBRADO { get; set; }
        public List<string> COBERTURAS {get; set;}
        public string CODIGO { get; set; }
        public string PRIMA { get; set; }
        public string TASA { get; set; }
        public List<string> RECARGOS { get; set; }
        public string TIPO { get; set; }
        public string PORCENTAJE { get; set; }
        public string MONTO { get; set; }
        public string CODINTER { get; set; }
    }
}
