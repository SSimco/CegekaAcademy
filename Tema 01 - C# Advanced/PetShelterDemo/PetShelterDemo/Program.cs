//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

var shelter = new PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!");

var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                { "Donate", Donate },
                { "Create a fundraiser", CreateFundraiser },
                { "See the information of a fundraiser", SeeFundraisers },
                { "Donate to a fundraiser", DonateToFundraiser },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Break our database connection", BreakDatabaseConnection },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}

void CreateFundraiser()
{
    var title = ReadString("Fundraiser's title?");
    var description = ReadString("Fundraiser's description?");
    var donationTarget = ReadDonation("Foundraiser's donation target?");

    Fundraiser fundraiser = new Fundraiser(title, description, donationTarget);
    shelter.RegisterFundraiser(fundraiser);
}
Currency ReadCurrency()
{
    var name = ReadString("Currency name?");
    System.Console.WriteLine($"Exchange rate of 1 {name} to EUR?");
    var rate = ReadDecimal();
    return new Currency(name, rate);
}
Donation ReadDonation(string? header = null)
{
    if (header != null)
    {
        Console.WriteLine(header);
    }
    var amount = ReadDecimal("Donation amount?");
    var currency = ReadCurrency();
    return new Donation(amount, currency);
}
Person ReadPerson()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();

    return new Person(name, id);
}
void ChooseFundraiser(string header, Action<Fundraiser> onFundraiserChoice)
{
    var fundraisers = shelter.GetAllFundraisers();
    if (fundraisers.Count == 0)
    {
        Console.WriteLine("There are no fundraisers at the moment\n");
        return;
    }
    var fundraiserOption = new Dictionary<string, Action>();
    foreach (var fundraiser in fundraisers)
    {
        fundraiserOption.Add(fundraiser.Title, () => onFundraiserChoice(fundraiser));
    }

    PresentOptions(header, fundraiserOption);
}
void DonateToFundraiser()
{
    ChooseFundraiser("Choose a fundraiser to donate to:",
    (fundraiser) =>
    {
        var person = ReadPerson();
        var donation = ReadDonation();
        fundraiser.Donate(person, donation);
    });
}

void SeeFundraisers()
{
    ChooseFundraiser("Choose a fundraiser to see:",
  (fundraiser) =>
  {
      Console.WriteLine($"Title: {fundraiser.Title} Total donations: {fundraiser.TotalDonations} Donation target: {fundraiser.DonationTarget}");
      var donors = fundraiser.Donors;
      if (donors.Count == 0)
      {
          Console.WriteLine("No one donated to this fundraiser yet.\n");
          return;
      }
      Console.WriteLine("Donors:");
      foreach (var donor in donors)
      {
          Console.WriteLine(donor.Name);
      }
      Console.WriteLine("");
  });
}
void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    var person = ReadPerson();
    var donation = ReadDonation();
    shelter.Donate(person, donation);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonations()}");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}
decimal ReadDecimal(string? header = null)
{
    if (header != null) Console.WriteLine(header);
    var isUserInputValid = decimal.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadDecimal(header);
    }

    Console.WriteLine("");
    return userInput;
}
int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}