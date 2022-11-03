using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCI_Project;
using HCI_Project.Models;

namespace HCI_Project.Pages.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly HCI_Project.registrationContext _context;

        public DeleteModel(HCI_Project.registrationContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Section Section { get; set; }

        public async Task<IActionResult> OnGetAsync(decimal? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section = await _context.Sections.FirstOrDefaultAsync(m => m.Crn == id);

            if (section == null)
            {
                return NotFound();
            }
            else 
            {
                Section = section;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(decimal? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }
            var section = await _context.Sections.FindAsync(id);

            if (section != null)
            {
                Section = section;
                _context.Sections.Remove(Section);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
