namespace DemoApplication.ViewModels.Admin.Navbar
{
    public class NavbarListViewModel
    {
        public NavbarListViewModel(int id, string name, bool isMain, bool isHeader, bool isFooter, int row)
        {
            Id = id;
            Name = name;
            IsMain = isMain;
            IsHeader = isHeader;
            IsFooter = isFooter;
            Row = row;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
        public bool IsHeader { get; set; }
        public bool IsFooter { get; set; }
        public int Row { get; set; }

    }
}
