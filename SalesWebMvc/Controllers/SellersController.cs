using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Declarando uma dependencia para o SellerService
        private readonly SellerService _sellerService;

        //Criando o construtor para injetar a dependencia.
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index() //Vai chamar nossa operação FindAll() lá da nossa classe SellerService
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Tipo de retorno de todas as ações
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
