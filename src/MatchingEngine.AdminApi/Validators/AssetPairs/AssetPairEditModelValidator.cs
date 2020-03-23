using FluentValidation;
using MatchingEngine.AdminApi.Models.AssetPairs;

namespace MatchingEngine.AdminApi.Validators.AssetPairs
{
    public class AssetPairEditModelValidator : AbstractValidator<AssetPairEditModel>
    {
        public AssetPairEditModelValidator()
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

            RuleFor(o => o.BaseAssetId)
                .NotEmpty()
                .WithMessage("Base asset identifier required.")
                .MaximumLength(36)
                .WithMessage("Id shouldn't be longer than 36 characters.");

            RuleFor(o => o.QuotingAssetId)
                .NotEmpty()
                .WithMessage("Quoting asset identifier required.")
                .MaximumLength(36)
                .WithMessage("Quoting asset identifier shouldn't be longer than 36 characters.");

            RuleFor(o => o.Accuracy)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Accuracy should be greater than or equal to 0.");

            RuleFor(o => o.MinVolume)
                .GreaterThan(0m)
                .WithMessage("Min volume should be greater than 0.");

            RuleFor(o => o.MaxVolume)
                .GreaterThan(0m)
                .WithMessage("Max volume should be greater than 0.");

            RuleFor(o => o.MaxOppositeVolume)
                .GreaterThan(0m)
                .WithMessage("Max opposite volume should be greater than 0.");

            RuleFor(o => o.MarketOrderPriceThreshold)
                .GreaterThan(0m)
                .WithMessage("Market order price threshold should be greater than 0.");
        }
    }
}
