import type { Person } from "./Person";

export class FundraiserDonationRequest {
    id: number;
    donationAmount: number;
    donor: Person;

    constructor(id: number, donationAmount: number, donor: Person) {
        this.id = id;
        this.donationAmount = donationAmount;
        this.donor = donor;
    }
}