using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class DepartmentsService
    {
        //Readonly faz com que essa dependencia não seja alterada.
        private readonly SalesWebMvcContext _context;

        //Criando um construtor para que a dependecia possa ocorrer
        public DepartmentsService(SalesWebMvcContext context)
        {
            _context = context;
        }

        //Metodo para retornar os Departamentos ordenados por Nome
        public async Task<List<Department>> FindAllAsync() //A palavra "async Task "no começo do metodo faz ele virar asyncrono
        {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync(); //await avisa ao compilador que essa é uma chamada asyncrona
        }
    }
}
