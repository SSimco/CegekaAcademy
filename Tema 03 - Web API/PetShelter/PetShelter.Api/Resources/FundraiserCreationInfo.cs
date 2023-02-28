namespace PetShelter.Api.Resources;

public class FundraiserCreationInfo
{
    public decimal GoalValue { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public Person Owner { get; set; }
}