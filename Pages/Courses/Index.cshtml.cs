﻿using System;
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

public static class Extension
{
    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
    }
}
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
        [BindProperty(SupportsGet = true)]
        public int? CRN { get; set; }
        public async Task OnGetAsync()
        {
            var classes = from m in _context.Takeableclasses
                         select m;
            var sections = from m in _context.Sections
                           select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                classes = classes.Where(x => x.Classname.ToLower().Contains(SearchString.ToLower()));
            }
            if (!string.IsNullOrEmpty(CRN.ToString()))
            {
                 sections = sections.Where(s => s.Crn ==CRN);
                 
            }
            Class = await classes.ToListAsync();
            Section = await sections.ToListAsync();
        }
    }
}
