namespace PetShelter.Domain;

public class Fundraiser : INamedEntity
{
    public string Name { get; set; }
    public decimal GoalValue { get; set; }
    public decimal CurrentRaisedAmount { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreationDate { get; set; }
    public FundraiserStatus Status { get; set; }
    public Person Owner { get; set; }
    public ICollection<Person> Donors { get; set; }
}