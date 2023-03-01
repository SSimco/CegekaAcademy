namespace PetShelter.Api.Resources;

public class FundraiserInfo : FundraiserPartialInfo
{
    public decimal CurrentRaisedAmount { get; set; }
    public decimal GoalValue { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<Person> Donors { get; set; }
}
