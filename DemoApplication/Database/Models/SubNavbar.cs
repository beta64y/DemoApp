using DemoApplication.Database.Models.Common;

namespace DemoApplication.Database.Models
{
    public class SubNavbar : BaseEntity
    {
        public string Name { get; set; }
        public string ToUrl { get; set; }
        public int Row { get; set; }
        public int NavbarId { get; set; }
        public Navbar Navbar { get; set; }

    }
}
