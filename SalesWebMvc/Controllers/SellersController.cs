using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Declarando uma dependencia para o SellerService
        private readonly SellerService _sellerService;

        //Criando o contrutor para injetar a dependencia.
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index() //Vai chamar nossa operação FindAll() lá da nossa classe SellerService
        {
            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
