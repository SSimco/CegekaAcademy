import type { PersonDto } from "./PersonDtos";

export interface FundraiserPartialInfoDto {
    name: string;
    status: string;
}

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

export interface FundraiserInfoDto {
    name: string
    status: string
    currentRaisedAmount: number;
    goalValue: number;
    dueDate: Date;
    donors: PersonDto[];
}
