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
    public class Etagere_DetailsModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public Etagere_DetailsModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

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
    }
}
