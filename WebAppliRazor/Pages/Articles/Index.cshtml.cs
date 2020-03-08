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
    public class ArticleModel : PageModel
    {
        private readonly GestionMagasin.TpDbContext _context;

        public ArticleModel(GestionMagasin.TpDbContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }
        public int nbArticles { get; set; }

        public async Task OnGetAsync()
        {
            Article = await _context.Articles.ToListAsync();
        }
    }
}
