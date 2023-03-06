import type { Person } from "./Person"

export class Fundraiser {
    id: number;
    name: string;
    status: string;
    currentRaisedAmount: number;
    goalValue: number;
    dueDate: Date;
    donors: Person[] | undefined;

    constructor(id: number, name: string, status: string, currentRaisedAmount: number, goalValue: number, dueDate: Date, donors?: Person[]) {
        this.id = id;
        this.name = name;
        this.status = status;
        this.currentRaisedAmount = currentRaisedAmount;
        this.goalValue = goalValue;
        this.dueDate = dueDate;
        this.donors = donors;
    }
}
