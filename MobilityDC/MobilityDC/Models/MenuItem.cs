using System;
using System.Collections.Generic;
using System.Text;

namespace MobilityDC.Models
{
    public enum MenuItemType
    {
        Home,
        Bulk,
        Fine,
        Store,
        Help,
        SignOut
    }
    public class MenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
