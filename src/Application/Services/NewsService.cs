using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;
        private readonly IImageService _imageService;

        public NewsService(INewsRepository newsRepository, IImageService imageService)
        {
            _newsRepository = newsRepository;
            _imageService = imageService;
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
            var news = _newsRepository.GetById(id) ?? throw new NotFoundException("Noticia no encontrada");

            news.Title = request.Title;
            news.Description = request.Description;
            news.ImageUrl = request.ImageUrl;
            news.Date = request.Date;

            _newsRepository.Update(news);
        }

        public void Delete(int id)
        {
            var news = _newsRepository.GetById(id) ?? throw new NotFoundException("Noticia no encontrada");

            if (!string.IsNullOrEmpty(news.ImageUrl))
            {
                var publicId = ObtenerPublicIdDesdeUrl(news.ImageUrl);
                _imageService.DeleteImageAsync(publicId).Wait();
            }

            _newsRepository.Delete(news);
        }

        public List<NewsDtoResponse> GetAll()
        {
            var newsList = _newsRepository.GetAll() ?? throw new NotFoundException("No hay noticias");

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
            var news = _newsRepository.GetById(id) ?? throw new NotFoundException("Noticia no encontrada");

            return new NewsDtoResponse
            {
                Id = news.Id,
                Title = news.Title,
                Description = news.Description,
                ImageUrl = news.ImageUrl,
                Date = news.Date
            };
        }

        public List<NewsDtoResponse> GetByDate(DateTime date)
        {
            var newsList = _newsRepository.GetByDate(date) ?? throw new NotFoundException("No hay noticias para esa fecha");

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

        private string ObtenerPublicIdDesdeUrl(string imageUrl)
        {
            var uri = new Uri(imageUrl);
            var path = uri.AbsolutePath; // /club-news/nombre.jpg
            var parts = path.Split('/');
            var filename = Path.GetFileNameWithoutExtension(parts.Last());

            return parts.Length >= 2
                ? $"{parts[^2]}/{filename}"
                : filename;
        }
    }
}
