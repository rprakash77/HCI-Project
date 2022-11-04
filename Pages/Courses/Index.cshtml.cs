using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HCI_Project;
using HCI_Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCI_Project.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly HCI_Project.registrationContext _context;

        public IndexModel(HCI_Project.registrationContext context)
        {
            _context = context;
        }

        public IList<Section> Section { get;set; } = default!;
        public IList<Takeableclass> Class { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }
        public async Task OnGetAsync()
        {
            if (_context.Sections != null)
            {
                Section = await _context.Sections.Include(s => s.Class).ToListAsync();
            }
            if (_context.Takeableclasses != null)
            {
                Class = await _context.Takeableclasses.ToListAsync();
            }
        }
    }
}
