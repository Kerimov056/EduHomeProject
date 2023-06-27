using EduHome.Core.Interface;
using System.ComponentModel.DataAnnotations;

namespace EduHome.Core.Entities;

public class EvSpeakers : IEntity
{
    public int Id { get ; set ; }
    [Required,MaxLength(300)]
    public string ImagePath { get ; set ; }
    [Required, MaxLength(50)]
    public string Name { get ; set ; }
    public List<Event_Spkears> Event_Spkears { get ; set ; }
    public List<EvCompanySpeakers> EvCompanySpeakers { get ; set ; }

}
