using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace sefazProcess.Models
{
    public class Product
    {
        public long id { get; set; }
        public long codigoGTIN { get; set; }
        public DateTime dataEmissao { get; set; }
        public long codigoTipoPagamento { get; set; }
        public long codigoProduto { get; set; }
        public long codigoNCM { get; set; }
        public string codigoUnidade { get; set; }

        public string descricaoProduto { get; set; }
        public decimal valorUnitario { get; set; }
        public long idEstabelecimento { get; set; }
        public string nomeEstabelecimento { get; set; }
        public string nomeLogradouro { get; set; }
        public long codigoNumLogradouro { get; set; }
        public string complemento { get; set; }
        public string nomeBairro { get; set; }
        public long codigoMunicipioIBGE { get; set; }
        public string nomeMunicipio { get; set; }
        public string nomeSiglaUF { get; set; }
        public long codCep { get; set; }
        public string numeroLatitude { get; set; }
        public string numeroLongitude { get; set; }

        [NotMapped]
        public string urlCoordinator {get;set;}

       

    }
}
