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
    public class EtagereModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public EtagereModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        public IList<Etagere> Etagere { get;set; }

        public async Task OnGetAsync()
        {
            Etagere = await _context.Etageres.ToListAsync();
        }
    }
}
