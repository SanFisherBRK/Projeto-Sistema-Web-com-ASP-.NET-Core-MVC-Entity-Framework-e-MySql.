using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Services;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModel;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;

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

        public async Task<IActionResult> Index() //Vai chamar nossa operação FindAll() lá da nossa classe SellerService
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        //Tipo de retorno de todas as ações
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            //Testando se é valido fica execultando enquanto o usuario não preencher o formulario
            if (!ModelState.IsValid)
            {
                //Carregando os departamentos
                var departments = await _departmentService.FindAllAsync();

                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message }); ;
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            }
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
            }
            return View(obj);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id ==null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" }); ;
            }
            //Testando se esse id existe no banco de dados
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" }); ;
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel ViewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(ViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            //Testando se é valido fica execultando enquanto o usuario não preencher o formulario
            if (!ModelState.IsValid)
            {
                //Carregando os departamentos
                var departments = await _departmentService.FindAllAsync();

                var viewModel = new SellerFormViewModel { Seller = seller, Departments  = departments };

                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" }); ;
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message }); ;
            }
           
            
        }
        public IActionResult Error(string message)
        {
            var ViewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(ViewModel);
        }
    }
}
