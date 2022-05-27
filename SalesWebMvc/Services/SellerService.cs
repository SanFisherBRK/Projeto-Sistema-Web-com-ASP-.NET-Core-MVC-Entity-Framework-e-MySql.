using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //Readonly faz com que essa dependencia não seja alterada.
        private readonly SalesWebMvcContext _context;

        //Criando um construtor para que a dependecia possa ocorrer
        public SellerService (SalesWebMvcContext context)
        {
            _context = context;
        }
            
        public List<Seller> FindAll() 
        {
            //Acessa a minha fonte de dados relacionado a tabela vendedores e converte para uma lista.
            return _context.Seller.ToList();
        } 
    }
}
