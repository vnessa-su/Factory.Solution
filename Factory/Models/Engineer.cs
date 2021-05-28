using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.Models
{
  public class Engineer
  {
    public int EngineerId { get; set; }

    [Display(Name = "First Name")]
    [Column(TypeName = "VARCHAR(50)")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Column(TypeName = "VARCHAR(50)")]
    public string LastName { get; set; }

    [Column(TypeName = "VARCHAR(255)")]
    public string Address { get; set; }

    [Display(Name = "Phone Number")]
    [Column(TypeName = "VARCHAR(20)")]
    public string PhoneNumber { get; set; }

    [Column(TypeName = "VARCHAR(255)")]
    public string Email { get; set; }

    [Display(Name = "Hire Date")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime HireDate { get; set; } = DateTime.Now;

    public virtual ICollection<EngineerMachine> EngineerMachineJoinEntities { get; }

    public Engineer()
    {
      this.EngineerMachineJoinEntities = new HashSet<EngineerMachine>();
    }
  }
}