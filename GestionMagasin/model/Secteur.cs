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
    }
}
