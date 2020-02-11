using System;
using System.Collections.Generic;
using System.Text;

namespace tpIndividuel.model
{
    public class PositionMagasin
    {
        public int Quantite { get; set; }
        public Etagere Etagere { get; set; }
        public int IdEtagere { get; set; }
        public int IdArticle { get; set; }
        public Article Article { get; set; }

        public PositionMagasin(int quantite, Etagere etagere, Article article)
        {
            if(etagere.PoidsMaximum< etagere.getPoidsArticles() + article.Poids * quantite)
            {
                throw new Exception("L'etagere ne peux pas supporter autant de poids !");
            }
            Quantite = quantite;
            Etagere = etagere;
            Article = article;
            article.ListPositions.Add(this);
            etagere.ListPositions.Add(this);
        }
        public PositionMagasin(int quantite, int idEtagere, int idArticle)
        {
            
            Quantite = quantite;
            IdEtagere = idEtagere;
            IdArticle = IdArticle;
        }
        public PositionMagasin()
        {

        }
    }
}
