using Application.Interfaces;
using Application.Models.Request;
using Application.Models.Response;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class MonthlyFeeService : IMonthlyFeeService
    {
        private readonly IMonthlyFeeRepository _repository;
        private readonly IUserRepository _userRepository;

        public MonthlyFeeService(IMonthlyFeeRepository repository, IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public int Create(MonthlyFeeCreateRequest request)
        {
            if (_repository.GetByMonthYear(request.Month, request.Year) != null) 
            {
                throw new BadRequestException("Ya existe una cuota para ese mes y año.");
            }
                
            var dueDate = new DateTime(request.Year, request.Month, 10);

            var fee = new MonthlyFee
            {
                Price = request.Price,
                Month = request.Month,
                Year = request.Year,
                DueDate = dueDate
            };

            _repository.Add(fee);
            return fee.Id;
        }

        public void Update(MonthlyFeeUpdateRequest request, int monthlyFeeId)
        {
            var monthlyFee = _repository.GetById(monthlyFeeId);
            if (monthlyFee == null)
            {
                throw new NotFoundException("No se encontró la cuota");
            }

            monthlyFee.Price = request.Price;
            monthlyFee.Month = request.Month;  
            monthlyFee.Year = request.Year;

            _repository.Update(monthlyFee);
        }

        public void Delete(int monthlyFeeId)
        {
            var monthlyFee = _repository.GetById(monthlyFeeId);
            if (monthlyFee == null)
            {
                throw new NotFoundException("No se encontró la cuota");
            }

            _repository.Delete(monthlyFee);
        }
        public List<MonthlyFeeDtoResponse> GetAll()
        {
            return _repository.GetAll().Select(f => new MonthlyFeeDtoResponse
            {
                Id = f.Id,
                Price = f.Price,
                Month = f.Month,
                Year = f.Year,
                DueDate = f.DueDate
            }).ToList();
        }

        public MonthlyFeeDtoResponse? GetByMonthYear(int month, int year)
        {
            var fee = _repository.GetByMonthYear(month, year);
            if (fee == null) return null;

            return new MonthlyFeeDtoResponse
            {
                Id = fee.Id,
                Price = fee.Price,
                Month = fee.Month,
                Year = fee.Year,
                DueDate = fee.DueDate
            };
        }

        //public MonthlyFeeDtoResponse? GetByMonthYearUser(int month, int year, int id)
        //{
        //    var fee = _repository.GetByMonthYear(month, year);
        //    var user = _userRepository.GetById(id);
        //    if (fee == null) return null;

        //    return new MonthlyFeeUserResponse
        //    {
        //        User = user.Id,
        //        Id = fee.Id,
        //        Price = fee.Price,
        //        Month = fee.Month,
        //        Year = fee.Year,
        //        DueDate = fee.DueDate
        //    };
        //}
    }
}
