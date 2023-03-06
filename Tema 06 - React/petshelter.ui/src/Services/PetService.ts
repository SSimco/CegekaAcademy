import axios from 'axios';
import { Person } from '../Models/Person';
import { Pet } from '../Models/Pet';
import type { IdentifiablePetDto, RescuedPetDto } from './dto/PetDtos';

export class PetService {

    private apiUrl: string;

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;
    }

    public async getAll(): Promise<Pet[]> {
        const response = await axios
            .get(this.apiUrl + '/Pets');
        console.log(response.data);
        var petsResponse: Pet[] = [];
        response.data.forEach((petFromApi: IdentifiablePetDto) => {

            let rescuer = new Person(petFromApi.rescuer.name, petFromApi.rescuer.idNumber);
            let adopter = petFromApi.adopter ? new Person(petFromApi.adopter.name, petFromApi.adopter.idNumber) : undefined;

            petsResponse.push(
                new Pet(
                    petFromApi.name,
                    petFromApi.imageUrl,
                    petFromApi.type,
                    petFromApi.description,
                    petFromApi.birthDate,
                    petFromApi.weightInKg,
                    rescuer,
                    petFromApi.id,
                    adopter
                )
            );
        });
        return petsResponse;
    }

    rescue(pet: Pet): Promise<void> {

        let rescuedPet: RescuedPetDto = {
            name: pet.name,
            description: pet.description,
            birthDate: pet.birthdate,
            imageUrl: pet.imageUrl,
            isHealthy: true,
            rescuer: {
                dateOfBirth: new Date(),
                idNumber: pet.rescuer.id,
                name: pet.rescuer.name
            },
            type: pet.type,
            weightInKg: pet.wieghtInKg
        }
        return axios.post(this.apiUrl + '/Pets', rescuedPet);
    }
}
