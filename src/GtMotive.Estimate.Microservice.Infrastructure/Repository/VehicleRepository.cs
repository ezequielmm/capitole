using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capitole.Infrastructure.Data;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Capitole.Infrastructure.Repositories
{
    public class VehicleRepository(AppDbContext context) : IVehicleRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _context.Vehicles
                .Where(x => !x.IsRented && x.YearOfManufacture >= DateTime.Now.Year - 5)
                .ToListAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserHasActiveRentalAsync(string userId)
        {
            return await _context.Vehicles.AnyAsync(v => v.RentedBy == userId);
        }
    }
}
