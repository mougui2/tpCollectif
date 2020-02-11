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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=demos.db");
            base.OnConfiguring(optionsBuilder);
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

        public Article FindArticleById(int id)
        {
            return context.Articles.Where(a => a.Id == id).FirstOrDefault();
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return context.Articles;
        }
        public IEnumerable<Article> GetAllArticlesBySecteur(Secteur secteur)
        {
            List<Article> listArticle = new List<Article>();
            context.Secteurs.Where(s => s.Id == secteur.Id).FirstOrDefault()?.ListEtagere.ForEach(e => e.ListPositions.ForEach(pos =>
            {
                if (pos.Etagere.Id == e.Id)
                {
                    listArticle.Add(pos.Article);
                }
            }));

            return listArticle;
        }
        public IEnumerable<Article> GetAllArticlesByEtagere(Etagere etagere)
        {
            List<Article> listArticle = new List<Article>();

            context.Etageres.Where(e => e.Id == etagere.Id).FirstOrDefault()?.ListPositions.ForEach(p => {

                if (p.IdEtagere == etagere.Id)
                    listArticle.Add(FindArticleById(p.IdArticle));
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

        #endregion

        #region etagere

        #endregion

        #region secteur

        public float PrixMoyenBySecteur(Secteur secteur)
        {
            var lesArticles = GetAllArticlesBySecteur(secteur);
            float total = 0;
            lesArticles.ToList().ForEach(a => total += a.PrixInitial);
            return total / lesArticles.Count();
        }

        #endregion


        //TODO article prix moyen sur tous le magasin par article, qte totale de l'article
        //todo etagere prix moyen de l'etagere, qte article, poidsdes articles present, taux remplissage (% poids)
        //todo secteur qte article, nb etageres, taux remplissage moyen(% poids)
    }
}
