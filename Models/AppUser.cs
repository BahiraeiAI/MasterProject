using System;
namespace MasterProject.Models
{
	public class AppUser:IdentityUser
	{

		public ICollection<ToDoModel>? toDoModels { get; set; }
	}
}

