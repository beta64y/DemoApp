using DemoApplication.Database;
using DemoApplication.Database.Models;

using DemoApplication.ViewModels.Admin.Navbar;
using DemoApplication.ViewModels.Admin.Subnavbar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace DemoApplication.Controllers.Admin
{
    [Route("Subnavbar")]
    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;
        public SubNavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("list", Name = "Subnavbar-list")]
        public IActionResult List()

        {
            var model = _dataContext.SubNavbars.Select(s => new SubNavbarListViewModel(s.Id, s.Navbar.Name, s.Name, s.ToUrl, s.Row)).ToList();

            return View("~/Views/Admin/Subnavbar/List.cshtml", model);
        }

        #region Add

        [HttpGet("add", Name = "subnavbar-add")]
        public IActionResult Add()
        {
            var model = new ViewModels.Admin.Subnavbar.AddviewModel
            {
                Navbars = _dataContext.Navbars
                    .Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter,n.Row))
                    .ToList(),
            };

            return View("~/Views/Admin/Subnavbar/Add.cshtml", model);
        }



        [HttpPost("add", Name = "subnavbar-add")]
        public IActionResult Add(ViewModels.Admin.Subnavbar.AddviewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            if (!_dataContext.Navbars.Any(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar is not found");
                return GetView(model);
            }




            AddSubNavbar();

            return RedirectToRoute("Subnavbar-list");




            IActionResult GetView(ViewModels.Admin.Subnavbar.AddviewModel model)
            {
                model.Navbars = _dataContext.Navbars
                    .Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter,n.Row))
                    .ToList();



                return View("~/Views/Admin/Subnavbar/Add.cshtml", model);
            }

            void AddSubNavbar()
            {
                var subnavbar = new SubNavbar
                {

                    Name = model.Name,
                    NavbarId = model.NavbarId,
                    ToUrl = model.ToUrl,
                    Row = model.Row
                };

                _dataContext.SubNavbars.Add(subnavbar);



                _dataContext.SaveChanges();
            }
        }


        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "subnavbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null)
            {
                return NotFound();
            }

            var model = new AddviewModel
            {
                Id = subnavbar.Id,
                Name = subnavbar.Name,
                ToUrl = subnavbar.ToUrl,
                NavbarId = subnavbar.NavbarId,
                Row = subnavbar.Row,
                Navbars =
                    _dataContext.Navbars
                        .Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter, n.Row)).ToList()
            };

            return View("~/Views/Admin/Subnavbar/Update.cshtml", model);
        }

        [HttpPost("update/{id}", Name = "subnavbar-update")]
        public async Task<IActionResult> UpdateAsync(AddviewModel model)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (subnavbar is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            if (!_dataContext.Navbars.Any(a => a.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar is not found");
                return GetView(model);
            }



            UpdateSubNavbarAsync();

            return RedirectToRoute("subnavbar-list");

            IActionResult GetView(AddviewModel model)
            {
                model.Navbars = _dataContext.Navbars
                    .Select(n => new NavbarListViewModel(n.Id, n.Name, n.IsMain, n.IsHeader, n.IsFooter, n.Row))
                    .ToList();



                return View("~/Views/Admin/Subnavbar/Add.cshtml", model);
            }

            async Task UpdateSubNavbarAsync()
            {
                subnavbar.Name = model.Name;
                subnavbar.NavbarId = model.NavbarId;
                subnavbar.ToUrl = model.ToUrl;
                subnavbar.Row = model.Row;

                _dataContext.SubNavbars.Add(subnavbar);

                _dataContext.SaveChanges();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "subnavbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(subnavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("subnavbar-list");
        }

        #endregion
    }
}
