using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestionMagasin;
using GestionMagasin.model;

namespace WebAppliRazor
{
    public class Etagere_CreateModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public Etagere_CreateModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Etagere Etagere { get; set; }

        [BindProperty]
        public List<Secteur> SecteurList { get { return _context.Secteurs.ToList(); } }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Etageres.Add(Etagere);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
