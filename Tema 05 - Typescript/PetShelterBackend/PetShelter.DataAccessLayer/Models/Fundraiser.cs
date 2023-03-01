namespace PetShelter.DataAccessLayer.Models;

public class Fundraiser : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal GoalValue { get; set; }
    public decimal CurrentRaisedAmount { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime CreationDate { get; set; }
    public string Status { get; set; }
    public int OwnerId { get; set; }
    public Person Owner { get; set; }
    public ICollection<Person> Donors { get; set; }
}
