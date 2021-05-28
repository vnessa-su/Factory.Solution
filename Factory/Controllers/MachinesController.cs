using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Collections.Generic;
using System.Linq;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;

    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> allMachines = _db.Machines
        .OrderBy(machine => machine.Manufacturer)
        .ThenBy(machine => machine.ProductModel)
        .ToList();
      return View(allMachines);
    }

    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "ProductModel");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine, int[] engineerIds)
    {
      foreach (int engineerId in engineerIds)
      {
        bool entryExists = _db.EngineerMachine
          .Any(entry => entry.MachineId == machine.MachineId && entry.EngineerId == engineerId);
        if (!entryExists)
        {
          _db.EngineerMachine.Add(new EngineerMachine() { MachineId = machine.MachineId, EngineerId = engineerId });
        }
      }
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Machine selectedMachine = _db.Machines
        .Include(machine => machine.EngineerMachineJoinEntities)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(machine => machine.MachineId == id);
      return View(selectedMachine);
    }

    public ActionResult Edit(int id)
    {
      Machine selectedMachine = _db.Machines
        .Include(machine => machine.EngineerMachineJoinEntities)
        .ThenInclude(join => join.Engineer)
        .FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.CourseId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(selectedMachine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine, int[] engineerIds)
    {
      foreach (int engineerId in engineerIds)
      {
        EngineerMachine joinEntry = _db.EngineerMachine
          .Where(entry => entry.EngineerId == engineerId && entry.MachineId == machine.MachineId)
          .Single();
        _db.EngineerMachine.Remove(joinEntry);
      }

      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine selectedMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(selectedMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine selectedMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(selectedMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}