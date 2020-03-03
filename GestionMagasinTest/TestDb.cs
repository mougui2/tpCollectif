using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using GestionMagasin;
using GestionMagasin.model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GestionMagasinTest
{
    public class TestDb
    {
        private Secteur _secteur;
        private Secteur _secteur1;

        private Etagere _etagere;
        private Etagere _etagere1;

        private Article _article;
        private Article _article1;
        private Article _article2;

        private PositionMagasin _position;
        private PositionMagasin _position1;
        private PositionMagasin _position2;

        private TpDbContext ctx;

        public void initVar()
        {
            _secteur = new Secteur(1, "secteur1");
            _secteur1 = new Secteur(2, "secteur2");
            _etagere = new Etagere(1, 255, _secteur);
            _etagere1 = new Etagere(2, 255, _secteur1);

            _article = new Article(1, "toto0", "??", DateTime.Today, 10, 10);
            _article1 = new Article(2, "toto1", "??", DateTime.Today, 10, 2);
            _article2 = new Article(3, "toto2", "??", DateTime.Today, 10, 5);
            //affectation position automatique
            _position = new PositionMagasin(1, _etagere, _article1);
            _position1 = new PositionMagasin(2, _etagere, _article2);

            _position2 = new PositionMagasin(3, _etagere1, _article);

        }

        private void InitTest()
        {
            if (File.Exists("test.db"))
            {
                File.Delete("test.db");
            }

            initVar();

            ctx = new TpDbContext(new DbContextOptionsBuilder<TpDbContext>().UseSqlite("Filename=test.db").Options);
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            ctx.Articles.Add(_article1);
            ctx.Articles.Add(_article2);
            ctx.Articles.Add(_article);
            ctx.Add(_etagere);
            ctx.Add(_etagere1);
            ctx.Add(_secteur);
            ctx.Add(_secteur1);
            ctx.SaveChanges();

        }

        [Fact]
        public void GetAllArticleTest()
        {
            InitTest();

            var repo = new ArticleRepository(ctx);
            var result = repo.GetAllArticles().ToList();
            result.Any(a => a == _article1).Should().BeTrue();
            result.Any(a => a == _article2).Should().BeTrue();
            result.Any(a => a == _article).Should().BeTrue();
        }

        [Fact]
        public void GetAllArticleByEtagereTest()
        {
            InitTest();
            var repo = new ArticleRepository(ctx);
            var result = repo.GetAllArticlesByEtagere(_etagere);
            result.Any(a => a == _article1).Should().BeTrue();
            result.Any(a => a == _article2).Should().BeTrue();
        }

        [Fact]
        public void GetAllArticlesBySecteurTest()
        {
            InitTest();
            var repo = new ArticleRepository(ctx);
            var result = repo.GetAllArticlesBySecteur(_secteur1);
            result.First().Should().BeSameAs(_article);
        }

        [Fact]
        public void FindArticleByIdTest()
        {
            InitTest();
            var repo = new ArticleRepository(ctx);
            var result = repo.FindArticleById(1);
            result.Should().BeSameAs(_article);
        }

        [Fact]
        public void InsertTest()
        {
            InitTest();
            var repo = new ArticleRepository(ctx);
            var temp = new Article(100, "articleTest", "??", new DateTime(), 50, 0.5f);
            repo.Insert(temp);
            repo.Save();
            ctx.Articles.Any(a => a == temp).Should().BeTrue();
        }

        [Fact]
        public void UpdateTest()
        {
            InitTest();
            var repo = new ArticleRepository(ctx);
            _article.SKU = "t";
            repo.Update(_article);
            repo.Save();
            ctx.Articles.Any(a => a == _article).Should().BeTrue();
        }

        [Fact]
        public void PrixMoyenBySecteurTest()
        {
            InitTest();
            var repo = new ArticleRepository(ctx);
            var result = repo.PrixMoyenBySecteur(_secteur);
            result.Should().Be((_article.PrixInitial + _article1.PrixInitial) / 2);
        }
    }
}
