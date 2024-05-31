using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_incahuasi_worker.Domain.Entities
{
    public class Pedido
    {
        public Guid id { get; set; }
        public int? numeroDePedido { get; set; }
        public string cicloDelPedido { get; set; }
        public long codigoDeContratoInterno { get; set; }
        public int estadoDelPedido { get; set; }
        public string cuentaCorriente { get; set; }
        public DateTime cuando { get; set; }
    }
}
