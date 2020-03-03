using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionMagasin;
using GestionMagasin.model;

namespace WebAppliRazor
{
    public class Etagere_EditModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public Etagere_EditModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Etagere Etagere { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Etagere = await _context.Etageres.FirstOrDefaultAsync(m => m.Id == id);

            if (Etagere == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Etagere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EtagereExists(Etagere.Id))
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

        private bool EtagereExists(int id)
        {
            return _context.Etageres.Any(e => e.Id == id);
        }
    }
}
