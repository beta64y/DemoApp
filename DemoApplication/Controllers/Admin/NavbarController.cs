using DemoApplication.Database;
using DemoApplication.Database.Models;

using DemoApplication.ViewModels.Admin.Navbar;
using DemoApplication.ViewModels.Admin.Subnavbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DemoApplication.Controllers.Admin
{
    [Route("Navbar")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;
        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "navbar-list")]
        public IActionResult List()

        {
            var model = _dataContext.Navbars.Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter,n.Row)).ToList();

            return View("~/Views/Admin/Navbar/List.cshtml", model);
        }

        #region Add

        [HttpGet("add", Name = "navbar-add")]
        public IActionResult Add()
        {
            var model = new ViewModels.Admin.Subnavbar.AddviewModel
            {
                Navbars = _dataContext.Navbars
        .Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter, n.Row))
        .ToList(),
            };
            return View("~/Views/Admin/Navbar/Add.cshtml");
        }



        [HttpPost("add", Name = "navbar-add")]
        public IActionResult Add(ViewModels.Admin.Navbar.AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            AddNavbar();

            return RedirectToRoute("Navbar-list");

            IActionResult GetView(ViewModels.Admin.Navbar.AddViewModel model)
            {
                //model.SubNavbars = _dataContext.SubNavbars
                //    .Select(n => new SubListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter, n.Row))
                //    .ToList();
                //niye?
                //Ne niye?burda navbarlari gostermeli deyildik he? onda silekde vapshe. Subnavbarlari gostermiyecek? ok
                return View("~/Views/Admin/Navbar/Add.cshtml", model);
            }

            void AddNavbar()
            {
                var navbar = new Navbar
                {
                    Name = model.Name,
                    IsMain = model.IsMain,
                    IsHeader = model.IsHeader,
                    IsFooter = model.IsFooter,
                    Row = model.Row

                };

                _dataContext.Navbars.Add(navbar);

                _dataContext.SaveChanges();
            }
        }


        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            var model = new AddViewModel
            {
                Id = navbar.Id,
                 Name= navbar.Name,
                IsFooter = navbar.IsFooter,
                IsHeader = navbar.IsHeader,
                IsMain = navbar.IsMain,
                //SubNavbars =_dataContext.SubNavbars.Select(n => new SubNavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter, n.Row)).ToList()
            };

            return View("~/Views/Admin/Navbar/Update.cshtml", model);
        }

        [HttpPost("update/{id}", Name = "navbar-update")]
        public async Task<IActionResult> UpdateAsync(AddViewModel model)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (navbar is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            UpdateNavbarAsync();

            return RedirectToRoute("navbar-list");

            IActionResult GetView(AddViewModel model)
            {
                //model.Navbars = _dataContext.Navbars
                //    .Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter, n.Row))
                //    .ToList();



                return View("~/Views/Admin/navbar/Add.cshtml", model);
            }

            async Task UpdateNavbarAsync()
            {
                navbar.Name = model.Name;
                navbar.IsMain = model.IsMain;
                navbar.IsHeader = model.IsHeader;
                navbar.IsFooter = model.IsFooter;
                navbar.Row = model.Row;

                _dataContext.Navbars.Add(navbar);

                _dataContext.SaveChanges();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("navbar-list");
        }

        #endregion
    }
}
