using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFHaberinDBService
{
    
    public class Service1 : IService1
    {
        NewsDBEntities db = new NewsDBEntities();

        public List<MansetDTO> ListMansetler()
        {
            List<News> MansetList = db.News.Where(n => n.isBigNew == true).ToList();
            return MansetList.Select(n => new MansetDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<SporDTO> ListSpor()
        {
            List<News> SporList = db.News.Where(n => n.CategoryID == 1).ToList();
            return SporList.Select(n => new SporDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<EkonomiDTO> ListEkonomi()
        {
            List<News> EkonomiList = db.News.Where(n => n.CategoryID == 2).ToList();
            return EkonomiList.Select(n => new EkonomiDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<MagazinDTO> ListMagazin()
        {
            List<News> MagazinList = db.News.Where(n => n.CategoryID == 4).ToList();
            return MagazinList.Select(n => new MagazinDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<EmlakDTO> ListEmlak()
        {
            List<News> ListEmlak = db.News.Where(n => n.CategoryID == 5).ToList();
            return ListEmlak.Select(n => new EmlakDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<SaglikDTO> ListSaglik()
        {
            List<News> ListSaglik = db.News.Where(n => n.CategoryID == 6).ToList();
            return ListSaglik.Select(n => new SaglikDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<TeknolojiDTO> ListTeknoloji()
        {
            List<News> ListTeknoloji = db.News.Where(n => n.CategoryID == 7).ToList();
            return ListTeknoloji.Select(n => new TeknolojiDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<PolitikaDTO> ListPolitika()
        {
            List<News> ListPolitika = db.News.Where(n => n.CategoryID == 8).ToList();
            return ListPolitika.Select(n => new PolitikaDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }

        public List<DunyaDTO> ListDunya()
        {
            List<News> ListDunya = db.News.Where(n => n.CategoryID == 9).ToList();
            return ListDunya.Select(n => new DunyaDTO
            {
                HaberID = n.NewsID,
                HaberBaslik = n.NewsTitle,
                HaberAciklama = n.NewsDescription,
                HaberIcerik = n.NewsContent,
                YayinlanmaTarihi = n.PublishDate
            }).ToList();
        }
    }
}
