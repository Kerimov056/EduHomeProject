using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class EvCompany : IEntity
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Name { get; set; }
    public List<EvCompanySpeakers> EvCompanySpeakers { get; set; }
}
