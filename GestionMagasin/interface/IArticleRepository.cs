using GestionMagasin.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionMagasin
{
    interface IArticleRepository
    {
        void Insert(Article article);
        void Update(Article article);
        void Remove(Article article);
        void Save();
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int id);
    }
}
