using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMercadoPagoService
    {
        Task<string> CrearPreferenciaAsync(string titulo, decimal precio, int cantidad, int cuotaId, int userId);
    }
}
