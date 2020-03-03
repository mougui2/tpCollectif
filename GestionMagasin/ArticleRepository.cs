using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GestionMagasin.model;

namespace GestionMagasin
{
    public class TpDbContext : DbContext, IDisposable
    {

        public TpDbContext(DbContextOptions<TpDbContext> options)
           : base(options)
        {        }

        public DbSet<Secteur> Secteurs { get; set; }
        public DbSet<Etagere> Etageres { get; set; }
        public DbSet<Article> Articles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var article = modelBuilder.Entity<Article>();
            article.Property(m => m.Libelle).IsRequired();
            article.Property(m => m.Poids).IsRequired();
            article.Property(m => m.PrixInitial).IsRequired();
            article.Property(m => m.SKU).IsRequired();
            article.Property(m => m.DateSortie).IsRequired();
            article.HasMany(m => m.ListPositions).WithOne(m => m.Article).HasForeignKey(m => m.IdArticle);

            var secteur = modelBuilder.Entity<Secteur>();
            secteur.Property(m => m.Nom);
            secteur.HasMany(m => m.ListEtagere).WithOne(m => m.Secteur).HasForeignKey(m => m.IdSecteur);

            var etagere = modelBuilder.Entity<Etagere>();
            etagere.Property(m => m.PoidsMaximum);
            etagere.HasOne(e => e.Secteur).WithMany(s => s.ListEtagere).HasForeignKey(e => e.IdSecteur);
            etagere.HasMany(m => m.ListPositions).WithOne(m => m.Etagere).HasForeignKey(m => m.IdEtagere);

            var positions = modelBuilder.Entity<PositionMagasin>();
            positions.HasKey(m => new { m.IdEtagere, m.IdArticle });

        }

    }

    public class ArticleRepository : IArticleRepository
    {
        private readonly TpDbContext context;

        public ArticleRepository(TpDbContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        #region Article

        public Article GetArticleById(int id)
        {
            return context.Articles.Where(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return context.Articles;
        }
        
        public IEnumerable<Article> GetAllArticlesByEtagere(Etagere etagere)
        {
            List<Article> listArticle = new List<Article>();

            context.Etageres.Where(e => e.Id == etagere.Id).FirstOrDefault()?.ListPositions.ForEach(p =>
            {

                if (p.IdEtagere == etagere.Id)
                    listArticle.Add(GetArticleById(p.IdArticle));
            });

            return listArticle;
        }

        public void Insert(Article article)
        {
            context.Articles.Add(article);
        }

        public void Remove(Article article)
        {
            context.Articles.Remove(article);
        }
        public void Update(Article article)
        {
            context.Articles.Update(article);
        }

        public int GetQuantiteArticleEnMagasin(int idArticle)
        {
            int total =0;
            var article = GetArticleById(idArticle);
            article.ListPositions.ForEach(pos => total += pos.Quantite);
            return total;
        }

        #endregion

        #region etagere

        #endregion

        #region secteur

        public Secteur GetSecteurById(int id)
        {
            return context.Secteurs.Where(s => s.Id == id).FirstOrDefault();
        }

        #endregion


        //todo etagere prix moyen de l'etagere, qte article, poidsdes articles present, taux remplissage (% poids)
        //todo secteur qte article, nb etageres, taux remplissage moyen(% poids)
    }
}
