using System;
namespace MasterProject.ViewModels
{
	public class AddToDoViewModel
	{
		[Required(ErrorMessage = "Title is required! ")]
		public string Title { get; set; }

        [Required(ErrorMessage = "Description is required! ")]
        public string  Description { get; set; }

        [Required(ErrorMessage = "DueTime is required! ")]
        public DateTime DueTime { get; set; }
	}
}

