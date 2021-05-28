using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }

    [Column(TypeName = "VARCHAR(255)")]
    public string Manufacturer { get; set; }

    [Display(Name = "Model")]
    [Column(TypeName = "VARCHAR(255)")]
    public string ProductModel { get; set; }

    [Display(Name = "Address")]
    [Column(TypeName = "VARCHAR(255)")]
    public string CompanyAddress { get; set; }

    [Display(Name = "Phone Number")]
    [Column(TypeName = "VARCHAR(20)")]
    public string CompanyPhoneNumber { get; set; }

    [Display(Name = "Email")]
    [Column(TypeName = "VARCHAR(255)")]
    public string CompanyEmail { get; set; }

    [Display(Name = "Website")]
    [Column(TypeName = "VARCHAR(255)")]
    public string CompanyWebsite { get; set; }

    public virtual ICollection<EngineerMachine> EngineerMachineJoinEntities { get; }

    public Machine()
    {
      this.EngineerMachineJoinEntities = new HashSet<EngineerMachine>();
    }
  }
}