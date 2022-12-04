using DemoApplication.ViewModels.Admin.Book.Add;
using DemoApplication.ViewModels.Admin.Navbar;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication.ViewModels.Admin.Subnavbar
{
    public class AddviewModel
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string ToUrl { get; set; }

        public int NavbarId { get; set; }

        public int Row { get; set; }

        public List<NavbarListViewModel>? Navbars { get; set; }
    }
}
