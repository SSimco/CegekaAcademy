namespace PetShelterDemo.Domain
{

    public class Donation
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public Donation(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public override string ToString()
        {
            return $"{Amount} {Currency.Name}";
        }

        public decimal GetAmountInEuro()
        {
            return Amount * Currency.ExchangeRateToEuro;
        }

        public decimal GetAmountInCurrency(Currency targetCurrency)
        {
            return GetAmountInEuro() / targetCurrency.ExchangeRateToEuro;
        }

        public static Donation operator +(Donation donation1, Donation donation2)
        {
            var sumInEuro = donation1.GetAmountInEuro() + donation2.GetAmountInEuro();
            return new Donation(sumInEuro / donation1.Currency.ExchangeRateToEuro, donation1.Currency);
        }
        public static bool operator <=(Donation donation1, Donation donation2)
        {
            return donation1.GetAmountInEuro() <= donation2.GetAmountInEuro();
        }
        public static bool operator >=(Donation donation1, Donation donation2)
        {
            return donation1.GetAmountInEuro() >= donation2.GetAmountInEuro();
        }
    }
}
