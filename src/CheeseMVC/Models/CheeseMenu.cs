using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    //intermidate class for menu and cheese
    public class CheeseMenu
    {
        public int MenuID { get; set; }
        public Menu Menus { get; set; }

        public int CheeseID { get; set; }
        public Cheese Cheese { get; set; }
    }
}
