﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModel;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Declarando uma dependencia para o SellerService
        private readonly SellerService _sellerService;
        private readonly DepartmentsService _departmentService;


        //Criando o construtor para injetar a dependencia.
        public SellersController(SellerService sellerService, DepartmentsService departmentsService)
        {
            _sellerService = sellerService;
            _departmentService = departmentsService;
        }

        public IActionResult Index() //Vai chamar nossa operação FindAll() lá da nossa classe SellerService
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        //Tipo de retorno de todas as ações
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            return View(ViewModel);
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
