using DemoApplication.Database.Models;
using DemoApplication.ViewModels.Admin.Navbar;

namespace DemoApplication.ViewModels.Admin.Subnavbar
{
    public class SubNavbarListViewModel
    {
        public int Id { get; set; }
        public string Navbar { get; set; }
        public string Name { get; set; }
        public string ToUrl { get; set; }
        public int Row { get; set; }

        public NavbarListViewModel NavbarListViewModel { get; set; }



        public SubNavbarListViewModel(int id,string navbar, string name, string toUrl, int row)
        {
            Id = id;
            Navbar = navbar;
            Name = name;
            ToUrl = toUrl;
            Row = row;
        }
    }
}
