namespace PetShelter.Api.Resources;

public class FundraiserInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public decimal CurrentRaisedAmount { get; set; }
    public decimal GoalValue { get; set; }
    public DateTime DueDate { get; set; }
    public ICollection<Person> Donors { get; set; }
}
