using Core.Domain.Validators;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
    /// <summary>
    /// Ejemplo de validador de entidad Dummy
    /// Todo validador de entidad de dominio debe heredar de <see cref="EntityValidator{TEntity}"/>
    /// Donde TEntity es del tipo <see cref="Core.Domain.Entities.DomainEntity{TEntity, TValidator}"/>
    /// </summary>
    public class CarValidator : EntityValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(DomainConstants.NOTNULL_OR_EMPTY); ;           // No empty Guid
            RuleFor(x => x.Make).NotEmpty();
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Color).NotEmpty();
        }
    }
}
