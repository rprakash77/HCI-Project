using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HCI_Project;
using HCI_Project.Models;

namespace HCI_Project.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly HCI_Project.registrationContext _context;

        public EditModel(HCI_Project.registrationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Section Section { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(decimal? id)
        {
            if (id == null || _context.Sections == null)
            {
                return NotFound();
            }

            var section =  await _context.Sections.FirstOrDefaultAsync(m => m.Crn == id);
            if (section == null)
            {
                return NotFound();
            }
            Section = section;
           ViewData["Classid"] = new SelectList(_context.Takeableclasses, "Classid", "Classid");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(Section.Crn))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SectionExists(decimal id)
        {
          return _context.Sections.Any(e => e.Crn == id);
        }
    }
}
