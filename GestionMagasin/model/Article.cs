using System;
using System.Collections.Generic;

namespace GestionMagasin.model
{
    public class Article
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
        public string SKU { get; set; }
        public DateTime DateSortie { get; set; }
        public float PrixInitial { get; set; }
        public float Poids { get; set; }
        public List<PositionMagasin> ListPositions { get; set; }

        public Article()
        {
            ListPositions = new List<PositionMagasin>();
        }

        public Article(int id, string libelle, string sKU, DateTime dateSortie, float prixInitial, float poids, List<PositionMagasin> positions)
        {
            Id = id;
            Libelle = libelle;
            SKU = sKU;
            DateSortie = dateSortie;
            PrixInitial = prixInitial;
            Poids = poids;
            ListPositions = positions;
        }
        public Article(int id, string libelle, string sKU, DateTime dateSortie, float prixInitial, float poids)
        {
            Id = id;
            Libelle = libelle;
            SKU = sKU;
            DateSortie = dateSortie;
            PrixInitial = prixInitial;
            Poids = poids;
            ListPositions = new List<PositionMagasin>();
        }

    }
}
