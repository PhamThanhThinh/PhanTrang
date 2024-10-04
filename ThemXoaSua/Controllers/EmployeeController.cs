using Microsoft.AspNetCore.Mvc;
using ThemXoaSua.Models;

namespace ThemXoaSua.Controllers
{
  public class EmployeeController : Controller
  {
    private readonly ApplicationDbContext _ctx;

    public EmployeeController(ApplicationDbContext ctx)
    {
      _ctx = ctx;
    }

    public IActionResult AddEmployees()
    {
      var employees = new List<Employee>
      {
        new Employee{Name="nhanvien1", Email="nhanvien1@test.xyz"},
        new Employee{Name="nhanvien2", Email="nhanvien2@test.xyz"},
        new Employee{Name="nhanvien3", Email="nhanvien3@test.xyz"},
        new Employee{Name="nhanvien4", Email="nhanvien4@test.xyz"},
        new Employee{Name="nhanvien5", Email="nhanvien5@test.xyz"},
        new Employee{Name="nhanvien6", Email="nhanvien6@test.xyz"},
        new Employee{Name="nhanvien7", Email="nhanvien7@test.xyz"},
        new Employee{Name="nhanvien8", Email="nhanvien8@test.xyz"},
        new Employee{Name="nhanvien9", Email="nhanvien9@test.xyz"},
        new Employee{Name="nhanvien10", Email="nhanvien10@test.xyz"},
        new Employee{Name="nhanvien11", Email="nhanvien11@test.xyz"},
        new Employee{Name="nhanvien12", Email="nhanvien12@test.xyz"}
      };
      _ctx.AddRange(employees);
      try
      {
        _ctx.SaveChanges();
        return Content("hoàn thành");
      }
      catch (Exception ex)
      {
        return Content("lỗi");
      }
    }

    public IActionResult DisplayEmployees(string keySearch = "", string orderBy = "", int currentPage = 1)
    {
      keySearch = string.IsNullOrEmpty(keySearch) ? "" : keySearch.ToLower();
      var employeData = new EmployeeViewModel();
      var employees = (from e in _ctx.Employees
                       where keySearch == "" || e.Name.ToLower().StartsWith(keySearch)
                       select new Employee
                       {
                         Id = e.Id,
                         Name = e.Name,
                         Email = e.Email
                       });
      switch (orderBy)
      {
        case "name_desc":
          employees = employees.OrderByDescending(a => a.Name);
          break;
        case "email_desc":
          employees = employees.OrderByDescending(a => a.Email);
          break;
        case "email":
          employees = employees.OrderBy(a => a.Email);
          break;
        default:
          employees = employees.OrderBy(a => a.Name);
          break;
      }
      int totalRecords = employees.Count();
      int pageSize = 5;
      int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
      employees = employees.Skip((currentPage - 1) * pageSize).Take(pageSize);
      employeData.Employees = employees;
      employeData.CurrentPage = currentPage;
      employeData.TotalPages = totalPages;
      employeData.KeySearch = keySearch;
      employeData.PageSize = pageSize;
      employeData.OrderBy = orderBy;
      return View(employeData);
    }
  }
}
