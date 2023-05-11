using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Task1.Models
{
    public class Todo
    {
        [DisplayName("Todo Id")]
        public int Id { get; set; }

        [MinLength(2)]
        public string Title { get; set; }

        public bool completed { get; set; }

        public Todo()
        {
            
        }

        public Todo(int id, string title, bool complete)
        {
            Id = id;
            Title = title;
            completed = complete;
            
        }
    }
}
