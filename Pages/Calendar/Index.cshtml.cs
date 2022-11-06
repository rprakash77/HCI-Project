using System;
using HCI_Project.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCI_Project;
using HCI_Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.AccessControl;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace HCI_Project.Pages.Calendar
{
    public class IndexModel : PageModel
    {
        public List<Section> cart { get; set; }
        public List<string> conflictslots { get; set; }
        public List<Takeableclass> takeableclasses { get; set; }
        private readonly HCI_Project.registrationContext _context;
        public IndexModel(HCI_Project.registrationContext context)
        {
            _context = context;
        }
        public async Task OnGet()
        {
            cart = SessionHelper.GetObjectFromJson<List<Section>>(HttpContext.Session, "cart");
            var classes = from m in _context.Takeableclasses select m;
            takeableclasses = await classes.ToListAsync();
            conflictslots = new List<string>();
        }

        public async Task<IActionResult> OnGetBuyNow(decimal id)
        {
            cart = SessionHelper.GetObjectFromJson<List<Section>>(HttpContext.Session, "cart");
            var sections = from m in _context.Sections
                           select m;
            int i = 0;
            Section? section = null;
            foreach (var item in await sections.ToListAsync())
            {
                Console.WriteLine("item.Crn:" + item.Crn + "\n");
                Console.WriteLine("Cart Crn:" + id + "\n");
                if (item.Crn == id)
                {
                    i++;
                    section = item;
                    break;
                }
            }
            if (i == 0)
            {
                return RedirectToPage("Details");
            }

            if (section == null)
            {
                return RedirectToPage("Courses");
            }
            if (cart == null)
            {
                cart = new List<Section>();
                cart.Add((section));
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int index = 0;
                foreach (Section item in cart)
                {
                    if (item.Crn == id)
                    {
                        index--;
                        break;
                    }
                }
                if (index == 0)
                {
                    cart.Add(section);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            return RedirectToPage("Cart");
        }
        public IActionResult OnGetDelete(decimal id)
        {
            cart = SessionHelper.GetObjectFromJson<List<Section>>(HttpContext.Session, "cart");
            foreach (var item in cart)
            {
                if (item.Crn == id)
                {
                    cart.Remove(item);
                    break;
                }
            }
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToPage("Cart");
        }
    }
}
