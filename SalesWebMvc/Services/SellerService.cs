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
            
        public async Task<List<Seller>> FindAllAsync() 
        {
            //Acessa a minha fonte de dados relacionado a tabela vendedores e converte para uma lista.
            return await _context.Seller.ToListAsync();
        } 

        //Adiciona um novo vendedor ao banco de dados
        public async Task InsertAsync(Seller obj)
        {
            _context.Add(obj);
            //salvando no banco de dados
            await _context.SaveChangesAsync();
        }

        //Procurar o vendedor pelo id digitado
        public async Task<Seller> FindByIdAsync(int id)
        {
            //Faz o Join entre duas tabelas mescla dados para monstrar o nome do departamento
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id); 
        }

        //Remove o vendedor cujo id seja igual ao digitado
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
             catch(DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async Task UpdateAsync(Seller obj)
        {
            //Any verifica se existem algum registro no banco com a condição que você colocar aqui
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if(!hasAny)
            {
                throw new NotFoundException("Id not found");
            }

            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }

            
        }
    }
}
