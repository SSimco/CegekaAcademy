import { FundraiserDonationMinAmount } from "./FundraiserValidatorsConstants";

export const validateDonationAmount = (donationAmount: string) => {
    if (donationAmount.length === 0) {
        return "Please enter a donation value";
    }
    const amount = Number(donationAmount);
    if (isNaN(amount) || amount <= FundraiserDonationMinAmount) {
        return "Please enter a donation value that is greater or equal than $1";
    }
    return null;
}
