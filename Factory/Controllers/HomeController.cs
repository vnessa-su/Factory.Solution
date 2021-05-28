using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private readonly FactoryContext _db;

    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Engineer> allEngineers = _db.Engineers
        .OrderBy(engineer => engineer.LastName)
        .ThenBy(engineer => engineer.FirstName)
        .ToList();
      ViewBag.EngineerList = allEngineers;

      List<Machine> allMachines = _db.Machines
        .OrderBy(machine => machine.Manufacturer)
        .ThenBy(machine => machine.ProductModel)
        .ToList();
      ViewBag.MachineList = allMachines;
      return View();
    }

  }
}