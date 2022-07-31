using AspNet.Data;
using AspNet.Models;
using AspNet.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVCDemoDbContext mVCDemoDb;

        public EmployeeController(MVCDemoDbContext mVCDemoDb)
        {
            this.mVCDemoDb = mVCDemoDb;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await mVCDemoDb.entities.ToListAsync();
            return View(employees);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeViewModel)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeViewModel.Name,
                Email = addEmployeeViewModel.Email,
                Salary = addEmployeeViewModel.Salary,
                Department = addEmployeeViewModel.Department,
                DateOfBirth = addEmployeeViewModel.DateOfBirth
            };

            await mVCDemoDb.entities.AddAsync(employee);

            await mVCDemoDb.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await mVCDemoDb.entities.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {



                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth

                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDb.entities.FindAsync(model.Id);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mVCDemoDb.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mVCDemoDb.entities.FindAsync(model.Id);

            if(employee != null)
            {
                mVCDemoDb.entities.Remove(employee);
                await mVCDemoDb.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }


    }
}
