namespace PetShelterDemo.Domain
{
    public class Currency
    {
        public string Name { get; }
        public decimal ExchangeRateToEuro { get; }

        public Currency(string name, decimal exchangeRateToEuro)
        {
            Name = name;
            ExchangeRateToEuro = exchangeRateToEuro;
        }
    }
}
