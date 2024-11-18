using Microsoft.AspNetCore.Identity;
using TrackerApp.DataAccess.Entities;

namespace TrackerApp.DataAccess;

public class UserEntity : IdentityUser<Guid>
{
    public List<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();
}
