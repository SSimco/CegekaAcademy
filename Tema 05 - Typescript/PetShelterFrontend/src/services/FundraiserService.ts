import axios from 'axios';
import type { FundraiserCreateRequest } from '../models/FundraiserCreateRequest';
import { Fundraiser } from '../models/Fundraiser';
import type { FundraiserDonationRequest } from '../models/FundraiserDonationRequest';
import { FundraiserPartialInfo } from '../models/FundraiserPartialInfo';
import { Person } from '../models/Person';
import type { FundraiserPartialInfoDto, FundraiserCreationInfoDto, FundraiserInfoDto, FundraiserDonationDto } from './dto/FundraiserDtos';

export class FundraiserService {
    private apiUrl: string;

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;
    }

    public getAll(): Promise<FundraiserPartialInfo[]> {
        return axios.get(this.apiUrl + '/Fundraisers')
            .then(response => {
                const fundraiserPartialInfos: FundraiserPartialInfo[] = response.data;
                return fundraiserPartialInfos.map((fundraiserPartialInfo: FundraiserPartialInfoDto) => new FundraiserPartialInfo(fundraiserPartialInfo.name, fundraiserPartialInfo.status));
            })
    }

    public get(id: number): Promise<Fundraiser> {
        return axios.get(this.apiUrl + `/Fundraisers/${id}`)
            .then(response => {
                const fundraiserInfoDto: FundraiserInfoDto = response.data;
                return new Fundraiser(fundraiserInfoDto.name,
                    fundraiserInfoDto.status,
                    fundraiserInfoDto.currentRaisedAmount,
                    fundraiserInfoDto.goalValue,
                    fundraiserInfoDto.dueDate,
                    fundraiserInfoDto.donors.map(personDto => new Person(personDto.name, personDto.idNumber, personDto.dateOfBirth)))
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
            .then(response => response.status == 204);
    }

    public closeFundraiser(id: number): Promise<boolean> {
        return axios.delete(this.apiUrl + `/Fundraisers/${id}`)
            .then(response => response.status == 204);
    }

    public donateToFundraiser(donationRequest: FundraiserDonationRequest): Promise<boolean> {
        const fundraiserDonationRequestDto: FundraiserDonationDto = {
            donor: { name: donationRequest.donor.name, idNumber: donationRequest.donor.id, dateOfBirth: donationRequest.donor.dateOfBirth },
            donationAmount: donationRequest.donationAmount
        };
        return axios.post(this.apiUrl + `/Fundraisers/${donationRequest.id}/donate`, fundraiserDonationRequestDto)
            .then(response => response.status == 204);
    }
}
