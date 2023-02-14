namespace PetShelterDemo.Domain
{
    public class Person : INamedEntity, IComparable
    {
        public string Name { get; }
        public string IdNumber { get; }

        public Person(string name, string idNumber)
        {
            Name = name;
            IdNumber = idNumber;
        }

        public int CompareTo(object? obj)
        {
            if (obj == null)
                return 1;
            Person other = obj as Person;
            if (obj != null)
            {
                if (Name.Equals(other.Name) && IdNumber.Equals(other.IdNumber))
                    return 0;
                return IdNumber.CompareTo(other.IdNumber);
            }
            else
            {
                throw new ArgumentException("Object is not a Person");
            }
        }
    }
}
