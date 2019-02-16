using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sefazProcess.Business;
using sefazProcess.Models;

namespace sefazProcess.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductBusiness _productBus;

        public ProductController(IProductBusiness productBusiness)
        {
            _productBus = productBusiness;
        }

        #region HTTP GET
 
        /// // GET api/product/version/{id}
        [ProducesResponseType((200), Type = typeof(List<Product>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [HttpGet, Route("{id}")]
        public ActionResult FindById(long id)
        {
            try
            {
            var product = _productBus.FindById(id);
            if (product == null) return NotFound("não existe esse registro ou o banco ainda não foi importado!");
            return Ok(product);
            }
            catch
            {
                return NotFound("não existe esse registro ou o banco ainda não foi importado!");
            }
        }

        /// GET api/product/version/codGTIN/{sortDirection}/{pageSize}/{page}/?codigoGTIN={codigoGTIN}
        /// <remarks>
        /// Exemplo de requisição:
        /// https://localhost:44395/api/Product/v1/codGTIN/desc/5/1/?codigoGTIN=7893000394117
        /// </remarks>
        [ProducesResponseType((200), Type = typeof(List<Product>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [HttpGet, Route("produtos/{sortDirection}/{pageSize}/{page}")]
        public ActionResult FindByCodigoGTIN([FromQuery] long codigoGTIN, string sortDirection, int pageSize, int page)
        {
            try
            {
                if(codigoGTIN == 0)
                {
                    return NotFound("não existe esse registro ou o banco ainda não foi importado!");
                }
                else
                {
                var products = _productBus.FindByKeyWordPaged(codigoGTIN, sortDirection, pageSize, page);
                if (products.List.Count == 0) return NotFound("não existe esses registros ou o banco ainda não foi importado!");
                return Ok(products);
                }
            }
            catch
            {
                return NotFound("não existe esse registro ou o banco ainda não foi importado!");
            }
        }

        // GET api/product/import
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [HttpGet]
        [Route("import")]
        public IActionResult Import()
        {
            try
            {
            Process.Start("CreateImportDB.bat");
            return Ok("Importação realizada com sucesso!");
            }
            catch
            {
                return NotFound("Algo de errado aconteceu, verifique o arquivo .bat");
            }
        }


        #endregion

    }
}
