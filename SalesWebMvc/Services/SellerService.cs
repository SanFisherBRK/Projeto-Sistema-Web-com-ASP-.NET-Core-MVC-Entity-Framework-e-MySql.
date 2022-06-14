using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;

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

        //Adiciona um novo vendedor ao banco de dados
        public void Insert(Seller obj)
        {
            _context.Add(obj);
            //salvando no banco de dados
            _context.SaveChanges();
        }

        //Procurar o vendedor pelo id digitado
        public Seller FindById(int id)
        {
            //Faz o Join entre duas tabelas mescla dados para monstrar o nome do departamento
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id); 
        }

        //Remove o vendedor cujo id seja igual ao digitado
        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }

        public void Update(Seller obj)
        {
            //Any verifica se existem algum registro no banco com a condição que você colocar aqui
            if(!_context.Seller.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            
        }
    }
}
