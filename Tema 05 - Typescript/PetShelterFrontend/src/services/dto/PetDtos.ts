import type { PersonDto } from "./PersonDtos";

export interface PetDto {
    birthDate: Date;
    description: string;
    imageUrl: string;
    isHealthy: boolean;
    name: string;
    weightInKg: number;
    type: string;
}

export interface IdentifiablePetDto extends PetDto {
    id: number;
    rescuer: PersonDto;
    adopter: PersonDto;
}

export interface RescuedPetDto extends PetDto {
    rescuer: PersonDto;
}
