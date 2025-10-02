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

        public async Task<Car?> GetByChassisNumberAsync(string chassisNumber, CancellationToken cancellationToken = default)
        {
            return await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ChassisNumber == chassisNumber, cancellationToken);
        }
    }
}
