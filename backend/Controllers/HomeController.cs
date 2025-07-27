using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Horizons.Connection;
using Horizons.Models;
using Horizons.Services;

namespace Horizons.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private ApplicationContext Context { get; set; }
        private ISaleService Service { get; set; }

        public HomeController(ILogger<HomeController> logger, ApplicationContext dbContext, ISaleService service)
        {
            Context = dbContext;
            _logger = logger;
            Service = service;
        }

 /// <summary>
        /// Получение списка контрагентов по имени. Учитывается вхождение без учета регистра.
        /// </summary>
        /// <param name="name">вводимое имя</param>
        /// <returns>Список подходящих по введенному имени контрагентов</returns>
        [HttpGet("GetContrpartnerByName")]
        public List<JsonResult> GetContrpartnerByName(String name)
        {
            _logger.LogInformation("GET GetContrpartnerByName, args="+ name);
            return Context.Contrpartners.Where(x => x.ContrpartnerName.ToLower().Contains(name.ToLower())).ToList()
                .ConvertAll(x => new JsonResult(x));
        }

        /// <summary>
        /// Получение списка контрагентов по ИНН. учитывается вхождение.
        /// </summary>
        /// <param name="inn">инн введенный</param>
        /// <returns>Список подходящих по введенному инн контрагентов</returns>
        [HttpGet("GetContrpartnerByInn")]
        public List<JsonResult> GetContrpartnerByInn(long inn)
        {
            _logger.LogInformation("GET GetContrpartnerByInn, args=" + inn);
            return Context.Contrpartners.Where(x => x.ContrpartnerInn.ToString().ToLower().Contains(inn.ToString().ToLower())).ToList()
                .ConvertAll(x => new JsonResult(x));
        }
        
        /// <summary>
        /// Получение списка контрагентов по региону. 
        /// </summary>
        /// <param name="division">регион</param>
        /// <returns>Список подходящих по введенному региону контрагентов</returns>
        [HttpGet("GetContrpartnerByDivision")]
        public List<JsonResult> GetContrpartnerByDivision(String division)
        {
            _logger.LogInformation("GET GetContrpartnerByDivision, args=" + division);
            return   Service.GetContrpartnersByDivision(Context, division)
                .ConvertAll(x => new JsonResult(x));
        }

        /// <summary>
        /// Получение списка контрагентов по складу. Учитывается вхождение строки.
        /// </summary>
        /// <param name="warehouse">склад</param>
        /// <returns>Список подходящих по введенному складу контрагентов</returns>
        [HttpGet("GetContrpartnerByWarehouse")]
        public List<JsonResult> GetContrpartnerByWarehouse(String warehouse)
        {
            _logger.LogInformation("GET GetContrpartnerByWarehouse, args=" + warehouse);
            var saleDocs = Context.SaleDocuments
                .Where(x => x.Warehouse.ToLower().Contains(warehouse.ToLower()))
                .Select(l => l.ContrpartnerId).Distinct().Take(200).ToList();

            return Context.Contrpartners.Where(j => saleDocs.Contains(j.Id))
                .ToList()
                .ConvertAll(x => new JsonResult(x));
        }

        /// <summary>
        /// Получение списка сортамента.
        /// </summary>
        /// <returns>Сортамент</returns>
        [HttpGet("GetAssortments")]
        public List<JsonResult> GetAssortments()
         {
             _logger.LogInformation("GET GetAssortments");
            return Context.Assortments
                .Take(500)//искусственное ограничение для демо
                .ToList().ConvertAll(x => new JsonResult(x));
         }

        /// <summary>
        /// Получение документов продаж по контрагенту.
        /// </summary>
        /// <param name="id">ИД контрагента</param>
        /// <returns>список документов-продаж с сортаментом по каждой позиции</returns>
        [HttpGet("GetSaleDocumentsByContrpartner")]
         public List<JsonResult> GetSaleDocumentsByContrpartner(long id)
         {
            _logger.LogInformation("GET GetSaleDocumentsByContrpartner, args=" );
            var listSales = Context.SaleDocuments
                 .Where(x => x.ContrpartnerId == id 
                             && Context.Sales.Any(u => u.SaleDocumentId == x.Id)).ToList();
             var assortment = listSales.Select(i => new
                     {SaleDocument = i, Assortments = Context.Sales.Where(s => s.SaleDocumentId == i.Id)})
                 .ToList().Select(o => new
                 {
                     document = o.SaleDocument,
                     assortments = Context.Assortments.Where(p => o.Assortments.Any(l => l.AssortmentId == p.Id))
                         .Distinct().ToList()
                 }).ToList();

             return assortment.ConvertAll(u => new JsonResult(u));
         }

        /// <summary>
        /// Объем продаж по контрагенту
        /// </summary>
        /// <param name="id">ИД контрагента</param>
        /// <returns>Объем продаж в тоннах</returns>
        [HttpGet("GetTnsByContrpartner")]
         public JsonResult GetTnsByContrpartner(long id)
         {
             _logger.LogInformation("GET GetTnsByContrpartner, args=" + id);
            var documentIds = Context.SaleDocuments.Where(x => x.ContrpartnerId == id).Select(y=>y.Id).ToList();
             return new JsonResult(Context.Sales.Where(x => documentIds.Contains(x.SaleDocumentId.GetValueOrDefault()))
                 .Select(y => y.Tns).Sum());
         }

        /// <summary>
        /// Объем продаж в разрезе месяцев документа-продаж
        /// </summary>
        /// <param name="id">ИД контрагента</param>
        /// <returns>Объем продаж в тоннах с группировкой по месяцам</returns>
        [HttpGet("GetTnsByMonths")]
         public List<JsonResult> GetTnsByMonths(long id)
         {
             _logger.LogInformation("GET GetTnsByMonths, args=" + id);
            var monthTns = Context.SaleDocuments.Where(x => x.ContrpartnerId == id)
                 .Select(x => new
                 {
                     Month = x.DocumentDate.Value.Month,
                     SalesSum = Context.Sales.Where(t => t.SaleDocumentId == x.Id).Select(t=>t.Tns).Sum()
                 })
                 .GroupBy(t => t.Month)
                 .Select(y => new {Month = y.Key, Tns = y.Sum(r=>r.SalesSum)}).ToList();

             return monthTns.ConvertAll(x => new JsonResult(x));
         }

        /// <summary>
        /// Объем продаж в разрезе поставщиков из документа-продаж
        /// </summary>
        /// <param name="id">ИД контрагента</param>
        /// <returns>Объем продаж в тоннах с группировкой по поставщикам</returns>
        [HttpGet("GetTnsBySuppliers")]
         public List<JsonResult> GetTnsBySuppliers(long id)
         {
             _logger.LogInformation("GET GetTnsBySuppliers, args=" + id);
            var supplierTns = Context.SaleDocuments.Where(x => x.ContrpartnerId == id)
                .Select(x => new
                {
                    Supplier = x.Supplier,
                    SalesSum = Context.Sales.Where(t => t.SaleDocumentId == x.Id).Select(t => t.Tns).Sum()
                })
                .GroupBy(t => t.Supplier)
                .Select(y => new { Supplier = y.Key, Tns = y.Sum(r => r.SalesSum) }).ToList();

            return supplierTns.ConvertAll(x => new JsonResult(x));
        }

        /// <summary>
        /// Получение рекомендаций для контрагента на основе алгоритма априори
        /// </summary>
        /// <param name="id">ИД контрагента</param>
        /// <returns>Список сортамента</returns>
        [HttpGet("GetAssortmentApriori")]
         public List<JsonResult> GetAssortmentApriori(long id)
         {
             _logger.LogInformation("GET GetAssortmentApriori, args=" + id);
            return Service.GetAssortmentApriori(id,  Context, 10, 2).ConvertAll(x=>new JsonResult(x));
         }

        /// <summary>
        /// Получение часто покупаемых сортаментов
        /// </summary>
        /// <returns>Список сортамента</returns>
        [HttpGet("GetFrequentlyAssortment")]
         public List<JsonResult> GetFrequentlyAssortment()
         {
             _logger.LogInformation("GET GetFrequentlyAssortment" );
            return Service.GetFrequentlyAssortment( Context).ConvertAll(x => new JsonResult(x));
         }

        /// <summary>
        /// Получение часто покупаемых сортаментов по характеристикам конкретного контрагента (регион)
        /// </summary>
        /// <param name="id">ИД контрагента</param>
        /// <returns>Список сортамента<</returns>
        [HttpGet("GetFrequentlyAssortmentByContrpartner")]
         public List<JsonResult> GetFrequentlyAssortmentByContrpartner(long id)
         {
             _logger.LogInformation("GET GetFrequentlyAssortmentByContrpartner, args=" + id);
            return Service.GetFrequentlyAssortmentByContrpartner(Context , id).ConvertAll(x => new JsonResult(x));
         }

    }
}
