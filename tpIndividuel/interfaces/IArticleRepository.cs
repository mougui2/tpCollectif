using System;
using System.Collections.Generic;
using System.Text;
using tpIndividuel.model;

namespace tpIndividuel
{
    interface IArticleRepository
    {
        void Insert(Article article);
        void Update(Article article);
        void Remove(Article article);
        void Save();
        IEnumerable<Article> GetAllArticles();
        Article FindArticleById(int id);
    }
}
