using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Task1.Data;
using Task1.Models;
using Task1.ViewModel;

namespace Task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult List()
        {
            List<Customer> customers=_context.Customers.Include(c=>c.Country).Include(ct=>ct.City).ToList();
            return View(customers);
        }
        public IActionResult Create()
        {
            CustomerCreateModel customerCreateModel = new CustomerCreateModel();
            customerCreateModel.Customer=new Customer();
            List<SelectListItem> countries=_context.Countries.OrderBy(n=>n.CountryName).Select(n=>new SelectListItem
            {
                Value = n.CountryId.ToString(),
                Text=n.CountryName
            }).ToList();
            customerCreateModel.Countries = countries;
            customerCreateModel.Cities = new List<SelectListItem>();
            return View(customerCreateModel);
        }
        public IActionResult GetCities(int CountryId)
        {
            List<SelectListItem> citiesSel = _context.Cities.Where(c => c.CountryId == CountryId).OrderBy(n => n.CityName).Select(n => new SelectListItem
            {
                Value = n.CityId.ToString(),
                Text = n.CityName
            }).ToList();
            return Json(citiesSel);

        }
        public IActionResult SaveCustomer(CustomerCreateModel cust)
        {
            _context.Add(cust.Customer);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public List<SelectListItem> GetCountries()
        {
            var lstCountry= new List<SelectListItem>();
            List<Country> countries=_context.Countries.ToList();
            lstCountry=countries.Select(n => new SelectListItem
            {
                Value=n.CountryId.ToString(),
                Text=n.CountryName
            }).ToList() ;
            return lstCountry;
        }
        public List<SelectListItem>GetCity(int CountryId)
        {
            List<SelectListItem> lstCity= _context.Cities
                .Where(c=>c.CountryId == CountryId)
                .OrderBy(n=>n.CityName)
                .Select(n=>new SelectListItem { Value = n.CityId.ToString(), Text=n.CityName }).ToList();
            return lstCity;

        }
        public Customer GetCustomerById(int Id)
        {
            Customer customer=_context.Customers.Where(c=>c.Id == Id).FirstOrDefault();
            return customer;
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Customer customer = GetCustomerById(id);
            ViewBag.CountryId = GetCountries();
            ViewBag.CityId = GetCity(customer.CountryId);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            _context.Attach(customer);
            _context.Entry(customer).State=EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id)
        {
            Customer customer = GetCustomerById(id);
            ViewBag.CountryId = GetCountries();
            ViewBag.CityId = GetCity(customer.CountryId);
            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}