namespace PetShelter.Domain.Extensions.DomainModel
{
    internal static class FundraiserExtensions
    {
        public static Fundraiser? ToDomainModel(this DataAccessLayer.Models.Fundraiser fundraiser)
        {
            if (fundraiser == null)
            {
                return null;
            }

            return new Fundraiser
            {
                Id = fundraiser.Id,
                Name = fundraiser.Name,
                GoalValue = fundraiser.GoalValue,
                CurrentRaisedAmount = fundraiser.CurrentRaisedAmount,
                DueDate = fundraiser.DueDate,
                CreationDate = fundraiser.CreationDate,
                Status = Enum.Parse<FundraiserStatus>(fundraiser.Status),
                Owner = fundraiser.Owner?.ToDomainModel(),
                Donors = fundraiser.Donors?.Select(p => p.ToDomainModel()).ToList(),
            }; ;
        }

        public static DataAccessLayer.Models.Fundraiser FromDomainModel(this Fundraiser fundraiser)
        {
            var entity = new DataAccessLayer.Models.Fundraiser
            {
                Id = fundraiser.Id,
                Name = fundraiser.Name,
                GoalValue = fundraiser.GoalValue,
                CurrentRaisedAmount = fundraiser.CurrentRaisedAmount,
                DueDate = fundraiser.DueDate,
                CreationDate = fundraiser.CreationDate,
                Status = fundraiser.Status.ToString(),
                Owner = fundraiser.Owner?.FromDomainModel(),
                Donors = fundraiser.Donors?.Select(p => p.FromDomainModel()).ToList(),
            };
            return entity;
        }
    }
}
