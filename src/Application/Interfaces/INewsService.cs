using Application.Models.Request;
using Application.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INewsService
    {
        void Create(NewsCreateRequest request);
        void Update(int id, NewsCreateRequest request);
        void Delete(int id);
        List<NewsDtoResponse> GetAll();
        NewsDtoResponse GetById(int id);
        List<NewsDtoResponse> GetByDate(DateTime date);

    }
}
