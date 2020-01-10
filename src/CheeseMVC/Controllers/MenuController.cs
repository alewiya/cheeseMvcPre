using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private readonly CheeseDbContext context;
        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }
        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View(addMenuViewModel);
        }
        public IActionResult ViewMenu(int id)
        {
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            Menu menu = context.Menus.Single(ni => ni.ID == id);
            ViewMenuViewModel ViewMenu = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items,
            };
            return View(ViewMenu);

        }
        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(p => p.ID == id);

            AddMenuItemViewModel addmenuItemView = new AddMenuItemViewModel(context.Cheeses.ToList())
            {
                menuId = menu.ID,
                Menu = menu

            };
            return View(addmenuItemView);
        }
        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemView)
        {
            if (ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context
                    .CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemView.CheeseId)
                    .Where(cm => cm.MenuID == addMenuItemView.menuId)
                    .ToList();

                if (!existingItems.Any())
                {
                    CheeseMenu cheeseMenu = new CheeseMenu
                    {
                        CheeseID = addMenuItemView.CheeseId,
                        MenuID = addMenuItemView.menuId
                    };
                    context.CheeseMenus.Add(cheeseMenu);
                    context.SaveChanges();

                    return Redirect("/Menu/ViewMenu/"+ cheeseMenu.MenuID);
                }
                }
                return View(addMenuItemView);

            

        }
    }
}

