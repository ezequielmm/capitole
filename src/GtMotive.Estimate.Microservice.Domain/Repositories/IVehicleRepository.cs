using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Repositories
{
    /// <summary>
    /// Interface for vehicle repository operations.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle asynchronously.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Gets the list of available vehicles asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of available vehicles.</returns>
        Task<List<Vehicle>> GetAvailableVehiclesAsync();

        /// <summary>
        /// Gets a vehicle by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the vehicle.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the vehicle if found; otherwise, null.</returns>
        Task<Vehicle> GetVehicleByIdAsync(int id);

        /// <summary>
        /// Updates an existing vehicle asynchronously.
        /// </summary>
        /// <param name="vehicle">The vehicle to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Checks if a user has an active rental asynchronously.
        /// </summary>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the user has an active rental; otherwise, false.</returns>
        Task<bool> UserHasActiveRentalAsync(string userId);
    }
}
