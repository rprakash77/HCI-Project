using HCI_Project.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HCI_Project.Models;
using Microsoft.EntityFrameworkCore;


namespace HCI_Project.Pages
{
    public class CartModel : PageModel
    {
        public List<Section> cart { get; set; }
        public List<Takeableclass> takeableclasses { get; set; }
        private readonly HCI_Project.registrationContext _context;
        public CartModel(HCI_Project.registrationContext context)
        {
            _context = context;
        }
        public async void OnGet()
        {
            cart = SessionHelper.GetObjectFromJson<List<Section>>(HttpContext.Session, "cart");
            var classes = from m in _context.Takeableclasses select m;
            takeableclasses = await classes.ToListAsync();
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