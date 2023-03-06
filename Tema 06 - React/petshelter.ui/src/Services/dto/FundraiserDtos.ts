import type { PersonDto } from "./PersonDtos";

export interface FundraiserCreationInfoDto {
    goalValue: number;
    name: string;
    dueDate: Date;
    owner: PersonDto;
}

export interface FundraiserDonationDto {
    donor: PersonDto,
    donationAmount: number;
}

export interface FundraiserDto {
    id: number,
    name: string
    status: string
    currentRaisedAmount: number;
    goalValue: number;
    dueDate: Date;
    donors: PersonDto[];
}
