namespace MVC.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public int JobCategoryId { get; set; }
        public bool IsActive { get; set; }
    }
}
