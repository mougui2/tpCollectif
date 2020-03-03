using System;
using System.Collections.Generic;
using System.Text;

namespace GestionMagasin.model
{
    public class Secteur
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<Etagere> ListEtagere { get; set; }

        public Secteur()
        {
            ListEtagere = new List<Etagere>();
        }

        public Secteur(int id, string nom)
        {
            Id = id;
            Nom = nom;
            ListEtagere = new List<Etagere>();
        }

        public Secteur(int id, string nom,List<Etagere> listEtagere)
        {
            Id = id;
            Nom = nom;
            ListEtagere = listEtagere;
        }

        public List<Article> GetAllArticle()
        {
            var result = new List<Article>();
            ListEtagere.ForEach(e => e.ListPositions.ForEach(pos => result.Add(pos.Article)));
            return result;
        }

        public int GetQteArticle()
        {
            int total = 0;
            ListEtagere.ForEach(e => e.ListPositions.ForEach(pos => total += pos.Quantite));
            return total;
        }

        public int GetNbEtagere()
        {
            return ListEtagere.Count;
        }

        public float GetPrixMoyen()
        {
            var lesArticles = GetAllArticle();
            float total = 0;
            lesArticles.ForEach(a => total += a.PrixInitial);
            return total / lesArticles.Count;
        }

        //public int GetTauxMoyenRemplissage()
        //{
        //    var tauxMoyen = 0;
        //    ListEtagere.ForEach(e => e.);
        //}
    }
}
