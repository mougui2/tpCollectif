using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestionMagasin;
using GestionMagasin.model;

namespace WebAppliRazor
{
    public class Secteur_DeleteModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public Secteur_DeleteModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Secteur Secteur { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Secteur = await _context.Secteurs.FirstOrDefaultAsync(m => m.Id == id);

            if (Secteur == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Secteur = await _context.Secteurs.FindAsync(id);

            if (Secteur != null)
            {
                _context.Secteurs.Remove(Secteur);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
