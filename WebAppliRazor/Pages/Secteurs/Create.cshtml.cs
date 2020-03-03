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
    public class Secteur_CreateModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public Secteur_CreateModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Secteur Secteur { get; set; }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Secteurs.Add(Secteur);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
