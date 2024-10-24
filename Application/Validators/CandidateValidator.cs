using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class CandidateValidator : AbstractValidator<CandidateDto>
{
    public CandidateValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        
        RuleFor(x => x.FirstName).NotNull();
        
        RuleFor(x => x.LastName).NotNull();
        
        RuleFor(x => x.Comment).NotNull().Length(1, 250);
        
        RuleFor(x => x.AvailableTo).GreaterThan(x => x.AvailableFrom);
    }
}