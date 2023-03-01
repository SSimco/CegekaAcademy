import type { Person } from "./Person";

export class FundraiserCreateRequest {
    goalValue: number;
    name: string;
    dueDate: Date;
    owner: Person;

    constructor(goalValue: number, name: string, dueDate: Date, owner: Person) {
        this.goalValue = goalValue;
        this.name = name;
        this.dueDate = dueDate;
        this.owner = owner;
    }
}