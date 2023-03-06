import { PersonIdNumberLength, PersonNameMinLength } from "./PersonValidatorsConstants";

export const validatePersonName = (personName: string) => {
    if (personName === "") {
        return "Please fill in your name";
    }
    if (personName.length < PersonNameMinLength) {
        return `Name must have at least ${PersonNameMinLength} characters`;
    }
    return null;
}

export const validatePersonId = (personIdNumber: string) => {
    if (personIdNumber.length > 0 && personIdNumber.charAt(0) === '0') {
        return "The ID number can't start with zero";
    }
    const id = Number(personIdNumber);
    if (isNaN(id) || personIdNumber.length !== PersonIdNumberLength) {
        return `Please enter a valid ${PersonIdNumberLength}-digit ID number`;
    }
    return null;
}
