namespace PetShelterDemo.Domain
{
    public class Fundraiser : INamedEntity
    {
        public string Title { get; }
        public string Description { get; }

        public Donation DonationTarget { get; }
        public Donation TotalDonations { get; private set; }
        public string Name { get { return Title; } }
        private SortedSet<Person> _Donors = new SortedSet<Person>();
        public IReadOnlyList<Person> Donors { get { return _Donors.ToList(); } }
        public Fundraiser(string title, string description, Donation donationTarget)
        {
            TotalDonations = new Donation(0, donationTarget.Currency);
            Title = title;
            Description = description;
            DonationTarget = donationTarget;
        }
        public void Donate(Person person, Donation donation)
        {
            _Donors.Add(person);
            TotalDonations += donation;
        }
    }
}
