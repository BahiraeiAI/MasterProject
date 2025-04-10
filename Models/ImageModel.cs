using System;
namespace MasterProject.Models
{
	public class ImageModel
	{
		[Key]
		public int Id { get; set; }
		public string ImageName { get; set; }
		public string ContentType { get; set; }
		public byte[] ImageData { get; set; }
		public int ToDoId { get; set; }
		public ToDoModel ToDo { get; set; }
	}
}

