using ECommerce.Common;
using FluentValidation.Results;

namespace ECommerce.BLL
{
    public class ErrorMapper : IErrorMapper
    {
        public Dictionary<string, List<Errors>> MapToErrors(ValidationResult validationResult)
        {
            {
                return validationResult.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => new Errors { Code = e.ErrorCode, Message = e.ErrorMessage }).ToList()
                    );
            }
        }
    }
}
