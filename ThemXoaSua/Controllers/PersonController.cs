using Microsoft.AspNetCore.Mvc;
using ThemXoaSua.Models;

namespace ThemXoaSua.Controllers
{
  //public class PersonController : Controller
  //{
  //  private readonly ApplicationDbContext _context;

  //  public PersonController(ApplicationDbContext context)
  //  {
  //    _context = context;
  //  }

  //  public IActionResult Index()
  //  {
  //    return View();
  //  }

  //  public IActionResult AddPerson()
  //  {
  //    return View();
  //  }

  //  [HttpPost]
  //  public IActionResult AddPerson(Person person)
  //  {
  //    if (!ModelState.IsValid)
  //    {
  //      return View();
  //    }

  //    try
  //    {
  //      _context.Add(person);
  //      //_context.Person.Add(person);
  //      _context.SaveChanges();
  //      TempData["msg"] = "them thanh cong";
  //      return RedirectToAction("Index");
  //    }
  //    catch (Exception ex)
  //    {
  //      TempData["msg"] = "khong the them người dùng. kiem tra lai";
  //      return View();
  //    }
  //  }

  //  public IActionResult DisplayPersons()
  //  {
  //    var persons = _context.Person.ToList();
  //    return View();
  //  }
  //}
  public class PersonController : Controller
  {
    private readonly ApplicationDbContext _ctx;
    public PersonController(ApplicationDbContext ctx)
    {
      _ctx = ctx;
    }
    public IActionResult Index()
    {
      return View();
    }

    //it is get method
    public IActionResult AddPerson()
    {
      return View();
    }

    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      try
      {
        _ctx.Person.Add(person);
        _ctx.SaveChanges();
        TempData["msg"] = "Thêm thành công";
        return RedirectToAction("AddPerson");

      }
      catch (Exception ex)
      {
        TempData["msg"] = "Không thể thêm";
        return View();
      }

    }

    public IActionResult DisplayPersons()
    {
      var persons = _ctx.Person.ToList();
      return View(persons);
    }

    public IActionResult EditPerson(int id)
    {
      var person = _ctx.Person.Find(id);
      return View(person);
    }

    [HttpPost]
    public IActionResult EditPerson(Person person)
    {
      if (!ModelState.IsValid)
      {
        return View();
      }
      try
      {
        _ctx.Person.Update(person);
        _ctx.SaveChanges();
        return RedirectToAction("DisplayPersons");

      }
      catch (Exception ex)
      {
        TempData["msg"] = "Không thể update";
        return View();
      }

    }

    public IActionResult DeletePerson(int id)
    {
      try
      {
        var person = _ctx.Person.Find(id);
        if (person != null)
        {
          _ctx.Person.Remove(person);
          _ctx.SaveChanges();
        }
      }
      catch (Exception ex)
      {
      }
      return RedirectToAction("DisplayPersons");

    }
  }
}
