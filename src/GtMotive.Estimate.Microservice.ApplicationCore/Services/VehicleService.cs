using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Services
{
    /// <summary>
    /// Servicio para gestionar vehículos.
    /// </summary>
    public class VehicleService(IVehicleRepository vehicleRepository)
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository;

        /// <summary>
        /// Crea un nuevo vehículo.
        /// </summary>
        /// <param name="vehicle">El vehículo a crear.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        /// <exception cref="InvalidOperationException">Si el vehículo tiene más de 5 años de antigüedad.</exception>
        public async Task CreateVehicleAsync(Vehicle vehicle)
        {
            if (vehicle is not null)
            {
                var currentYear = DateTime.Now.Year;
                if (vehicle.YearOfManufacture < currentYear - 5)
                {
                    throw new InvalidOperationException("El vehículo no puede tener más de 5 años de antigüedad.");
                }

                await _vehicleRepository.AddVehicleAsync(vehicle);
            }
        }

        /// <summary>
        /// Obtiene la lista de vehículos disponibles.
        /// </summary>
        /// <returns>Una lista de vehículos disponibles.</returns>
        public async Task<List<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _vehicleRepository.GetAvailableVehiclesAsync();
        }

        /// <summary>
        /// Alquila un vehículo.
        /// </summary>
        /// <param name="vehicleId">El ID del vehículo a alquilar.</param>
        /// <param name="userId">El ID del usuario que alquila el vehículo.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        /// <exception cref="ArgumentException">Si el vehículo no se encuentra.</exception>
        /// <exception cref="InvalidOperationException">Si el vehículo ya está alquilado o el usuario ya tiene un vehículo alquilado.</exception>
        public async Task RentVehicleAsync(int vehicleId, string userId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId) ?? throw new ArgumentException("Vehículo no encontrado.");
            if (vehicle.IsRented)
            {
                throw new InvalidOperationException("El vehículo ya está alquilado.");
            }

            var userHasVehicle = await _vehicleRepository.UserHasActiveRentalAsync(userId);
            if (userHasVehicle)
            {
                throw new InvalidOperationException("El usuario ya tiene un vehículo alquilado.");
            }

            vehicle.IsRented = true;
            vehicle.RentedBy = userId;
            await _vehicleRepository.UpdateVehicleAsync(vehicle);
        }

        /// <summary>
        /// Devuelve un vehículo alquilado.
        /// </summary>
        /// <param name="vehicleId">El ID del vehículo a devolver.</param>
        /// <param name="userId">El ID del usuario que devuelve el vehículo.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        /// <exception cref="ArgumentException">Si el vehículo no se encuentra.</exception>
        /// <exception cref="InvalidOperationException">Si el vehículo no está alquilado o si el usuario no es el que alquiló el vehículo.</exception>
        public async Task ReturnVehicleAsync(int vehicleId, string userId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(vehicleId) ?? throw new ArgumentException("Vehículo no encontrado.");
            if (!vehicle.IsRented)
            {
                throw new InvalidOperationException("El vehículo no está alquilado.");
            }

            if (vehicle.RentedBy != userId)
            {
                throw new InvalidOperationException("Solo el usuario que alquiló el vehículo puede devolverlo.");
            }

            vehicle.IsRented = false;
            vehicle.RentedBy = null;
            await _vehicleRepository.UpdateVehicleAsync(vehicle);
        }
    }
}
