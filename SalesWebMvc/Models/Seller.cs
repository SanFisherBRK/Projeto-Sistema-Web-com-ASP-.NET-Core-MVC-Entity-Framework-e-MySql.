using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }

        //Anotation
        [Required(ErrorMessage = "{0} required")] //Faz esse campo ser obrigatório
                                                          //"Name size should be between 3 and 60       //{1} pega o primeiro parametro que é 60     {2} pega o segundo parametro que é 3
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")] //Configura tamanho minimo e maximo do nome \\ErrorMessage personaliza uma mensagem caso o campo esteja vazio
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")] //Faz esse campo ser obrigatório
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required(ErrorMessage = "{0} required")] //Faz esse campo ser obrigatório
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "{0} required")] //Faz esse campo ser obrigatório
        [Range(1000.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:f2}")] //Coloca duas casas decimais Nosalario do vendedor que aparece na pagina.
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        //Construtor com argumentos
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        //Metodo para adicionar venda
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        //Metodo para remover venda
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
