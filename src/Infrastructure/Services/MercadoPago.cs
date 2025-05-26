using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using MercadoPago.Client.Preference;
using MercadoPago.Config;

namespace Infrastructure.Services
{
    public class MercadoPagoService : IMercadoPagoService
    {
        public MercadoPagoService()
        {
            MercadoPagoConfig.AccessToken = "APP_USR-203537482362331-052610-0e4bfe446d22345ac9132440c7b835ca-2462160676"; // Reemplaza con tu token real
        }

        // Método para crear preferencia
        public async Task<string> CrearPreferenciaAsync(string titulo, decimal precio, int cantidad)
        {
            try
            {
                var request = new PreferenceRequest
                {
                    Items = new List<PreferenceItemRequest>
                {
                    new PreferenceItemRequest
                    {
                        Title = titulo,
                        Quantity = cantidad,
                        CurrencyId = "ARS",
                        UnitPrice = precio
                    }
                },
                    BackUrls = new PreferenceBackUrlsRequest
                    {

                        Success = "https://localhost:5173/pago-exitoso",
                        Failure = "https://localhost:5173/pago-fallido",
                        Pending = "https://localhost:5173/pago-pendiente"
                    },
                    AutoReturn = "approved"
                };

                var client = new PreferenceClient();
                var preference = await client.CreateAsync(request);
                return preference.InitPoint!;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine("🔥 Error al crear preferencia: " + ex.Message);
                throw; 
            }
        }
            

    }

}
