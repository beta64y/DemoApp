using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class Navbar : BaseEntity
    {
        public string Name { get; set; }
        public int Row { get; set; }
        public bool IsMain { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
        public List<SubNavbar> SubNavbars { get; set; }
    }
}
