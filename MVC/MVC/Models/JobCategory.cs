using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models
{
    public class JobCategory
    {
        public Job Job { get; set; }
        public SelectList Categories { get; set; }
    }
}
