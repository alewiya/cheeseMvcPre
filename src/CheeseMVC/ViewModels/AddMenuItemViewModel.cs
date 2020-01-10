﻿using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int menuId { get; set; }
        public int CheeseId { get; set; }
        public List<SelectListItem> Cheeses { get; set; }
        public Menu Menu {get; set; }
        public AddMenuItemViewModel() { 
        }
        public AddMenuItemViewModel(IEnumerable<Cheese> cheeses)
        {
            Cheeses = new List<SelectListItem>();
            foreach(var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });

            }
        }
    }
}
