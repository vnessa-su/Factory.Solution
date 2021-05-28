using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;

    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> allEngineers = _db.Engineers
        .OrderBy(engineer => engineer.LastName)
        .ThenBy(engineer => engineer.FirstName)
        .ToList();
      return View(allEngineers);
    }

    public ActionResult Create()
    {
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "ProductModel");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer, int[] MachineIds)
    {
      foreach (int machineId in MachineIds)
      {
        bool entryExists = _db.EngineerMachine
          .Any(entry => entry.EngineerId == engineer.EngineerId && entry.MachineId == machineId);
        if (!entryExists)
        {
          _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machineId, EngineerId = engineer.EngineerId });
        }
      }
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Engineer selectedEngineer = _db.Engineers
        .Include(engineer => engineer.EngineerMachineJoinEntities)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(selectedEngineer);
    }

    public ActionResult Edit(int id)
    {
      Engineer selectedEngineer = _db.Engineers
        .Include(engineer => engineer.EngineerMachineJoinEntities)
        .ThenInclude(join => join.Machine)
        .FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.CourseId = new SelectList(_db.Machines, "MachineId", "ProductModel");
      return View(selectedEngineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer, int[] MachineIds)
    {
      foreach (int machineId in MachineIds)
      {
        EngineerMachine joinEntry = _db.EngineerMachine
          .Where(entry => entry.EngineerId == engineer.EngineerId && entry.MachineId == machineId)
          .Single();
        _db.EngineerMachine.Remove(joinEntry);
      }

      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer selectedEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(selectedEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer selectedEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(selectedEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}