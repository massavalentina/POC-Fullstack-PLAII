using Application.Repositories;
using Core.Infraestructure.Repositories.Sql;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Sql
{
    internal sealed class CarRepository(StoreDbContext context) : BaseRepository<Car>(context), ICarRepository
    {
        object ICarRepository.Repository => throw new NotImplementedException();
    }
}
