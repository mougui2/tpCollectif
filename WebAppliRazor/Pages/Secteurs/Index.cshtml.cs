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
    public class SecteurModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public SecteurModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        public IList<Secteur> Secteur { get;set; }

        public async Task OnGetAsync()
        {
            Secteur = await _context.Secteurs.ToListAsync();
        }
    }
}
