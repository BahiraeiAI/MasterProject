using System;
namespace MasterProject.Models
{
	public class ToDoModel
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime DueTime { get; set; }
		public TimeSpan RemainingTimeSpan => DueTime - CreatedAt;
		public ICollection<ImageModel>? Images { get; set; }
		public AppUser AppUser { get; set; }
		public string? AppUserId { get; set; }
	}
}

