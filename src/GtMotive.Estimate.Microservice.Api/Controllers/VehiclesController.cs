using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Services;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController(VehicleService vehicleService) : ControllerBase
    {
        private readonly VehicleService _vehicleService = vehicleService;

        /// <summary>
        /// Crea un nuevo vehículo en la flota.
        /// </summary>
        /// <param name="vehicle">El vehículo a devolver.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _vehicleService.CreateVehicleAsync(vehicle);
                return Ok("Vehículo creado");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista los vehículos disponibles para alquilar.
        /// </summary>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            var vehicles = await _vehicleService.GetAvailableVehiclesAsync();
            return Ok(vehicles);
        }

        /// <summary>
        /// Alquila un vehículo para un usuario específico.
        /// </summary>
        /// <param name="vehicleId">El ID del vehículo a alquilar.</param>
        /// <param name="userId">El ID del usuario que alquila el vehículo.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        [HttpPost("{vehicleId}/rent")]
        public async Task<IActionResult> RentVehicle(int vehicleId, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("El userId es requerido.");
            }

            try
            {
                await _vehicleService.RentVehicleAsync(vehicleId, userId);
                return Ok("Vehículo alquilado");
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve un vehículo alquilado por un usuario específico.
        /// </summary>
        /// <param name="vehicleId">El ID del vehículo a devolver.</param>
        /// <param name="userId">El ID del usuario que devuelve el vehículo.</param>
        /// <returns>Una tarea que representa la operación asincrónica.</returns>
        [HttpPost("{vehicleId}/return")]
        public async Task<IActionResult> ReturnVehicle(int vehicleId, [FromQuery] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("El userId es requerido.");
            }

            try
            {
                await _vehicleService.ReturnVehicleAsync(vehicleId, userId);
                return Ok("Vehículo devuelto");
            }
            catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
