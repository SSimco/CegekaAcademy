import { FundraiserPartialInfo } from "./FundraiserPartialInfo";
import type { Person } from "./Person"

export class Fundraiser extends FundraiserPartialInfo {
    currentRaisedAmount?: number;
    goalValue?: number;
    dueDate?: Date;
    donors: Person[] = [];

    constructor(name: string, status: string, currentRaisedAmount: number, goalValue: number, dueDate: Date, donors: Person[]) {
        super(name, status);
        this.name = name;
        this.status = status;
        this.currentRaisedAmount = currentRaisedAmount;
        this.goalValue = goalValue;
        this.dueDate = dueDate;
        this.donors = donors;
    }
}
