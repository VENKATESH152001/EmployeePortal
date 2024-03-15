using EmployeePortal.web.data;
using EmployeePortal.web.Models;
using EmployeePortal.web.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortal.web.Controllers
{
    public class EmployeesController(ApplicationDbContext dbContext) : Controller
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel viewModel)
        {
            var employee = new Employee
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                IsActive = viewModel.IsActive
            };
            await dbContext.Employees.AddAsync(employee);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
           var employees = await dbContext.Employees.ToListAsync();

            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
         var employee =  await dbContext.Employees.FindAsync(id);

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee viewModel)
        {
            var employee = await dbContext.Employees.FindAsync(viewModel.Id);

             if (employee is not null)
            {
                employee.Name = viewModel.Name;
                employee.Email = viewModel.Email;
                employee.Phone = viewModel.Phone;
                employee.IsActive = viewModel.IsActive;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Employees");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Employee viewModel)
        {
            var employee = await dbContext.Employees
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id==viewModel.Id);

            if(employee is not null)
            {
                dbContext.Employees.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Employees");
        }
    }
}
