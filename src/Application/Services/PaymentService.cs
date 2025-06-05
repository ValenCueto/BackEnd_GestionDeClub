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
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMonthlyFeeRepository _monthlyFeeRepository;

        public PaymentService(IPaymentRepository paymentRepo, IUserRepository userRepo, IMonthlyFeeRepository feeRepo)
        {
            _paymentRepository = paymentRepo;
            _userRepository = userRepo;
            _monthlyFeeRepository = feeRepo;
        }

        public void AssignFeeToAllUsers(int monthlyFeeId)
        {
            var users = _userRepository.GetAll();
            foreach (var user in users)
            {
                AssignFeeToUser(user.Id, monthlyFeeId);
            }
        }

        public void AssignFeeToUser(int userId, int monthlyFeeId)
        {
            if (!_paymentRepository.Exists(userId, monthlyFeeId))
            {
                var payment = new Payment
                {
                    UserId = userId,
                    MonthlyFeeId = monthlyFeeId,
                    Paid = false
                };
                _paymentRepository.Add(payment);
            }
        }

        public void MarkAsPaid(MarkPaymentRequest request)
        {
            var payment = _paymentRepository.GetByUserAndFee(request.UserId, request.MonthlyFeeId);
            if (payment == null) throw new Exception("No se encontró el registro de pago.");

            payment.Paid = true;
            payment.PaymentDate = DateTime.Now;
            _paymentRepository.Update(payment);
        }

        public List<PaymentDtoResponse> GetByUserId(int userId)
        {
            var payments = _paymentRepository.GetByUserId(userId);
            return payments.Select(p => new PaymentDtoResponse
            {
                Id = p.Id,
                UserId = p.UserId,
                UserName = p.User.Name,
                MonthlyFeeId = p.MonthlyFeeId,
                Price = p.MonthlyFee.Price,
                Period = $"{MesNombre(p.MonthlyFee.Month)} {p.MonthlyFee.Year}",
                Month = p.MonthlyFee.Month,           
                Year = p.MonthlyFee.Year,             
                DueDate = p.MonthlyFee.DueDate,
                Paid = p.Paid,
                PaymentDate = p.PaymentDate
            }).ToList();
        }

        public decimal GetCurrentMonthRevenue()
        {
            return _paymentRepository.GetCurrentMonthRevenue();
        }

        private string MesNombre(int mes)
        {
            return new[] {
                "", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
            }[mes];
        }

    }
}
