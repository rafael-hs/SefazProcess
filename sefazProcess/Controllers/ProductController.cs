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
            if (product == null) return NotFound();
            return Ok(product);
            }
            catch
            {
                return NotFound("não existe esse registro");
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
                    return NotFound("não existe esse registro");
                }
                else
                {
                var products = _productBus.FindByKeyWordPaged(codigoGTIN, sortDirection, pageSize, page);
                if (products == null) return NotFound();
                return Ok(products);
                }
            }
            catch
            {
                return NotFound();
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


            //var a = FindById(3);
            
            //if(a.ToString() == "não existe esse registro")
            //{
            //    return NotFound("Já foi executado a criação de banco uma vez!!");
            //}
            //else
            //{
            Process.Start("CreateImportDB.bat");
                return Ok();
            //}
        }


        #endregion

    }
}
