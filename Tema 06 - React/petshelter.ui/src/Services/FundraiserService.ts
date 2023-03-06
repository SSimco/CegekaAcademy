import axios from 'axios';
import type { FundraiserCreateRequest } from '../Models/FundraiserCreateRequest';
import type { FundraiserDonationRequest } from '../Models/FundraiserDonationRequest';
import { Fundraiser } from '../Models/Fundraiser';
import { Person } from '../Models/Person';

import type { FundraiserCreationInfoDto, FundraiserDto, FundraiserDonationDto } from './dto/FundraiserDtos';

export class FundraiserService {
    private apiUrl: string;

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;
    }

    public getAll(): Promise<Fundraiser[]> {
        return axios.get(this.apiUrl + '/Fundraisers')
            .then(response => {
                const fundraiserPartialInfos: FundraiserDto[] = response.data;
                return fundraiserPartialInfos.map(
                    (fundraiserDto: FundraiserDto) =>
                        new Fundraiser(fundraiserDto.id,
                            fundraiserDto.name,
                            fundraiserDto.status,
                            fundraiserDto.currentRaisedAmount,
                            fundraiserDto.goalValue,
                            fundraiserDto.dueDate,
                            fundraiserDto.donors == null ? undefined : fundraiserDto.donors.map(personDto => new Person(personDto.name, personDto.idNumber, personDto.dateOfBirth))
                        )
                );
            })
    }

    public get(id: number): Promise<Fundraiser> {
        return axios.get(this.apiUrl + `/Fundraisers/${id}`)
            .then(response => {
                const fundraiserDto: FundraiserDto = response.data;
                return new Fundraiser(fundraiserDto.id,
                    fundraiserDto.name,
                    fundraiserDto.status,
                    fundraiserDto.currentRaisedAmount,
                    fundraiserDto.goalValue,
                    fundraiserDto.dueDate,
                    fundraiserDto.donors.map(personDto => new Person(personDto.name, personDto.idNumber, personDto.dateOfBirth)))
            });
    }

    public createFundraiser(fundraiserCreateRequest: FundraiserCreateRequest): Promise<boolean> {
        const fundraiserCreateRequestDto: FundraiserCreationInfoDto = {
            goalValue: fundraiserCreateRequest.goalValue,
            name: fundraiserCreateRequest.name,
            dueDate: fundraiserCreateRequest.dueDate,
            owner: {
                idNumber: fundraiserCreateRequest.owner.id,
                name: fundraiserCreateRequest.owner.name,
                dateOfBirth: fundraiserCreateRequest.owner.dateOfBirth
            }
        };
        return axios.post(this.apiUrl + '/Fundraisers', fundraiserCreateRequestDto)
            .then(response => response.status === 204);
    }

    public closeFundraiser(id: number): Promise<number> {
        return axios.delete(this.apiUrl + `/Fundraisers/${id}`).then(response => response.status);
    }

    public donateToFundraiser(donationRequest: FundraiserDonationRequest): Promise<number> {
        const fundraiserDonationRequestDto: FundraiserDonationDto = {
            donor: { name: donationRequest.donor.name, idNumber: donationRequest.donor.id, dateOfBirth: donationRequest.donor.dateOfBirth },
            donationAmount: donationRequest.donationAmount
        };
        return axios.post(this.apiUrl + `/Fundraisers/${donationRequest.id}/donate`, fundraiserDonationRequestDto).then(response => response.status);
    }
}
