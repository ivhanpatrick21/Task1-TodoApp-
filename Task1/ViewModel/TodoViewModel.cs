using System.ComponentModel.DataAnnotations;

namespace Task1.ViewModel
{
    public class TodoViewModel
    {
        [Required(ErrorMessage = "Todo Title is required")]
        [MinLength(2)] // where title should be minimum of 2 characters
        public string Title { get; set; }
      
        public bool completed { get; set; }

       

       
        public TodoViewModel()
        {
        }
        public TodoViewModel(string title,  bool completed_)
        {
            Title = title;
            completed = completed_;
        }
    }
}
