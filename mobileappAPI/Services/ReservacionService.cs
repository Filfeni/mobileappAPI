using mobileappAPI.Authentication;
using mobileappAPI.Models;
using mobileappAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mobileappAPI.Services
{
    public interface IReservacionService
    {
        List<Reservacion> GetReservationByCarID(int Idcarro);
        void CreateReservation(Reservacion reservacion);
        //List<ReservationViewModel> GetReservationById(int id);
        List<Reservacion> SearchReservations(string modelo);
    }
    public class ReservacionService : IReservacionService
    {
        private readonly AuthContext _context;

        public ReservacionService(AuthContext dbContext)
        {
            _context = dbContext;
        }

        //Metodo para obtener las reservaciones por el idcarro o nombre
        public List<Reservacion> GetReservationByCarID(int Idcarro)
        {
            var reservacion = (from r in _context.Reservacions
                               where r.Idcarro == Idcarro
                               select new Reservacion {
                                   Idreservacion = r.Idreservacion,
                                   FechaSalida = r.FechaSalida,
                                   FechaEntrega = r.FechaEntrega,
                                   Idcarro = r.Idcarro,
                                   Idcliente = r.Idcliente,
                                   IdtipoReservacion = r.IdtipoReservacion

                               }).ToList();

            return reservacion;
        }

        public void CreateReservation(Reservacion reservacion)
        {
            Reservacion reservacionNew = new Reservacion();
            reservacionNew.Idreservacion = reservacion.Idreservacion;
            reservacionNew.Idcarro = reservacion.Idcarro;
            reservacionNew.Idcliente = reservacion.Idcliente;
            reservacionNew.IdtipoReservacion = reservacion.IdtipoReservacion;
            reservacionNew.FechaSalida = reservacion.FechaSalida;
            reservacionNew.FechaEntrega = reservacion.FechaEntrega;

            _context.Reservacions.Add(reservacionNew);
            _context.SaveChanges();

        }

        /*Este metodo devolvera un listado mediante Xamarin "Buscar metodo del calendario"
         para mostrar todas las reservaciones*/

        //public List<ReservationViewModel> GetReservationById(int id)
        //{
        //    var reservacion = (from r in _context.Reservacions
        //                       join f in _context.Carros on r.Idcarro equals f.Idcarro
        //                       join s in _context.Usuarios on )
        //}


        //Para la busqueda de la reservacion
        public List<Reservacion> SearchReservations(string modelo)
        {
            var reservacion = (from r in _context.Reservacions
                               join ca in _context.Carros on r.Idcarro equals ca.Idcarro
                               where ca.Modelo.ToLower().Contains(modelo.ToLower())
                               select new Reservacion {
                                   Idreservacion = r.Idreservacion,
                                   FechaSalida = r.FechaSalida,
                                   FechaEntrega = r.FechaEntrega,
                                   Idcarro = r.Idcarro,
                                   Idcliente = r.Idcliente,
                                   IdtipoReservacion = r.IdtipoReservacion

                               }).ToList();

            return reservacion;
        }
    }

}
