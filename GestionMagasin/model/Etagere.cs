using System;
using System.Collections.Generic;
using System.Text;

namespace GestionMagasin.model
{
    public class Etagere
    {
        public int Id { get; set; }
        public float PoidsMaximum { get; set; }
        public Secteur Secteur { get; set; }
        public int IdSecteur { get; set; }
        public List<PositionMagasin> ListPositions { get; set; }

        public Etagere()
        {
            ListPositions = new List<PositionMagasin>();
        }

        public Etagere(int id, float poidsMaximum, Secteur secteur)
        {
            Id = id;
            PoidsMaximum = poidsMaximum;
            Secteur = secteur;
            ListPositions = new List<PositionMagasin>();

        }

        public float getPoidsArticles()
        {
            float poids = 0;
            ListPositions.ForEach(p => poids += p.Article.Poids * p.Quantite);
            return poids;
        }
    }
}
