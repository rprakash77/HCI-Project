using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using HCI_Project;
using HCI_Project.Models;

namespace HCI_Project.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly HCI_Project.registrationContext _context;

        public CreateModel(HCI_Project.registrationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["Classid"] = new SelectList(_context.Takeableclasses, "Classid", "Classid");
            return Page();
        }

        [BindProperty]
        public Section Section { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Sections.Add(Section);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
