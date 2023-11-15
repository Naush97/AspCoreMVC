
using Microsoft.AspNetCore.Mvc.Rendering;
using Task1.Data;
using Task1.Models;

namespace Task1.ViewModel
{
    public class CustomerCreateModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<SelectListItem>Countries { get; set; }
        public IEnumerable<SelectListItem>Cities { get; set; }
    }
}
