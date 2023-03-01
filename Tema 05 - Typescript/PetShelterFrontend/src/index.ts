import axios from 'axios';
import { FundraiserCreateRequest } from './models/FundraiserCreateRequest';
import { FundraiserDonationRequest } from './models/FundraiserDonationRequest';
import { Person } from './models/Person';
import { Pet } from './models/Pet';
import { FundraiserService } from './services/FundraiserService';
import { PetService } from './services/PetService';

const apiUrl = "http://localhost:5009";
let petService = new PetService(apiUrl);

var petToRescue = new Pet(
    "Maricel",
    "https://i.imgur.com/AO6wMYS.jpeg",
    "Cat",
    "AAAAA",
    new Date(),
    8,
    new Person("Costel", "1234567890123")
)

petService.rescue(petToRescue)
    .then(() =>
        petService.getAll()
            .then(pets => console.log(pets))
    );

const fundraiserService = new FundraiserService(apiUrl);

const fundraiserToCreate = new FundraiserCreateRequest(
    1000,
    "Food and toys fundraiser for good dogs only",
    new Date("2024-12-24"),
    new Person("Ghiță", "1234567890123", new Date("2000-01-01"))
);
fundraiserService.createFundraiser(fundraiserToCreate);

fundraiserService.getAll().then(console.log);

fundraiserService.get(0).then(console.log);
const fundraiserDonationRequest = new FundraiserDonationRequest(7, 100, new Person("Ghiță", "1234567890123", new Date("2000-01-01")));
fundraiserService.donateToFundraiser(fundraiserDonationRequest);

fundraiserService.closeFundraiser(7);
