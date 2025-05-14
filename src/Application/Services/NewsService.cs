using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public void Create(NewsCreateRequest request)
        {
            var news = new News
            {
                Title = request.Title,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Date = request.Date
            };

            _newsRepository.Add(news);
        }

        public void Update(int id, NewsCreateRequest request)
        {
            var news = _newsRepository.GetById(id) ?? throw new Exception("Noticia no encontrada");

            news.Title = request.Title;
            news.Description = request.Description;
            news.ImageUrl = request.ImageUrl;
            news.Date = request.Date;

            _newsRepository.Update(news);
        }

        public void Delete(int id)
        {
            var news = _newsRepository.GetById(id) ?? throw new Exception("Noticia no encontrada");
            _newsRepository.Delete(news);
        }

        public List<NewsDtoResponse> GetAll()
        {
            var newsList = _newsRepository.GetAll() ?? throw new Exception("No hay noticias");

            var response = new List<NewsDtoResponse>();
            foreach (var news in newsList)
            {
                var dto = new NewsDtoResponse
                {
                    Id = news.Id,
                    Title = news.Title,
                    Description = news.Description,
                    ImageUrl = news.ImageUrl,
                    Date = news.Date
                };

                response.Add(dto);
            }

            return response;
        }


        public NewsDtoResponse GetById(int id)
        {
            var news = _newsRepository.GetById(id) ?? throw new Exception("Noticia no encontrada");

            return new NewsDtoResponse
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                ImageUrl = news.ImageUrl,
                Date = news.Date
            };
        }


        //Filtrar por fecha
        public List<NewsDtoResponse> GetByDate(DateTime date)
        {
            var newsList = _newsRepository.GetByDate(date) ?? throw new Exception("No hay noticias para esa fecha");

            var response = new List<NewsDtoResponse>();
            foreach (var news in newsList)
            {
                var dto = new NewsDtoResponse
                {
                    Id = news.Id,
                    Title = news.Title,
                    Description = news.Description,
                    ImageUrl = news.ImageUrl,
                    Date = news.Date
                };

                response.Add(dto);
            }

            return response;
        }



    }
}
