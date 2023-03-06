import { PetMinWeight, PetNameMinLength, PetTypes } from "./PetValidatorsConstants";

export const validatePetName = (petName: string) => {
    if (petName.length < PetNameMinLength) {
        return `Name must have at least ${PetNameMinLength} characters`;
    }
    return null;
}


export const validatePetType = (petType: string) => {
    if (!PetTypes.includes(petType)) {
        return `Invalid pet type "${petType}"`;
    }
    return null;
}

export const validatePetWeight = (petWeight: string) => {
    const weight = Number(petWeight);
    if (isNaN(weight) || weight <=PetMinWeight) {
        return `The weight of the pet must be greater than ${PetMinWeight}`;
    }
    return null;
}
