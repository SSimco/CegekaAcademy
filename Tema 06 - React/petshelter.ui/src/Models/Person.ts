export class Person {
    name: string;
    id: number;
    dateOfBirth: Date | undefined;

    constructor(name: string, id: number, dateOfBirth?: Date) {
        this.name = name;
        this.id = id;
        this.dateOfBirth = dateOfBirth;
    }
}