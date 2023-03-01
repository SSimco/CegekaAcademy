export class Person {
    name: string;
    id: string;
    dateOfBirth: Date | undefined;

    constructor(name: string, id: string, dateOfBirth?: Date) {
        this.name = name;
        this.id = id;
        this.dateOfBirth = dateOfBirth;
    }
}