using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;


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
        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
