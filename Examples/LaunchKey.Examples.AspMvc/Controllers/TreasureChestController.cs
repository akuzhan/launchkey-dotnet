using LaunchKey.Examples.AspMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaunchKey.Examples.AspMvc.Controllers
{
    public class TreasureChestController : Controller
    {
		static string[] Loot = {
			"a Goblin Poking Sword of Awesomeness",
			"an Ancient Essence of Overweight Ogres",
			"a Fleeting Bow of the Anxious Elf"
							   };
		[NoCache]
        public ActionResult Index()
        {
			var r = new Random();
			return View(new TreasureChestModel { ItemGained = Loot[r.Next() % Loot.Length], XpGained = r.Next(30, 90) });
        }

    }
}
