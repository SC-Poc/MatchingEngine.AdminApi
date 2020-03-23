using FluentValidation;
using MatchingEngine.AdminApi.Models.Assets;

namespace MatchingEngine.AdminApi.Validators.Assets
{
    public class AssetEditModelValidator : AbstractValidator<AssetEditModel>
    {
        public AssetEditModelValidator()
        {
            RuleFor(o => o.Id)
                .NotEmpty()
                .WithMessage("Identifier required.")
                .MaximumLength(36)
                .WithMessage("Identifier shouldn't be longer than 36 characters.");

            RuleFor(o => o.Name)
                .NotEmpty()
                .WithMessage("Name required.")
                .MaximumLength(100)
                .WithMessage("Name shouldn't be longer than 100 characters.");

            RuleFor(o => o.Description)
                .MaximumLength(500)
                .WithMessage("Description shouldn't be longer than 500 characters.");

            RuleFor(o => o.Accuracy)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Accuracy should be greater than or equal to 0.");
        }
    }
}
