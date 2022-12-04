using DemoApplication.Database.Models;
using DemoApplication.ViewModels.Admin.Book.Add;
using DemoApplication.ViewModels.Admin.Navbar;
using System.ComponentModel.DataAnnotations;

namespace DemoApplication.ViewModels.Admin.Navbar
{
    public class AddViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Row { get; set; }
        public bool IsMain { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
        public List<SubNavbar> SubNavbars { get; set; }

    }
}
