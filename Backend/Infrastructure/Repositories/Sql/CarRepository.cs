using Application.Repositories;
using Core.Infraestructure.Repositories.Sql;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Sql
{
    internal sealed class CarRepository : BaseRepository<Car>, ICarRepository
    {
        private readonly StoreDbContext _context;

        public CarRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Car> GetByChassisNumberAsync(string chassisNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ChassisNumber == chassisNumber, cancellationToken);
        }

        public async Task<Guid> AddAsync(Car entity, CancellationToken ct = default)
        {
            await _context.Cars.AddAsync(entity, ct);
            await _context.SaveChangesAsync(ct);
            return entity.Id;
        public async Task<Car> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}
