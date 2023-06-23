using System.ComponentModel.DataAnnotations;

namespace CoolApi.Models
{
    public class Student
    {
        public Student(int id, string name) 
        {
            this.id = id;
    this.name = name;
   
        }
                public int id{get; set;}
        [Required]
        public string name { get; set; }

        
    }
    
}