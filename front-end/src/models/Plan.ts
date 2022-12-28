import PlanSubject from "./PlanSubject";

interface Plan {
    id: number;
    specialtyId: number;
    name: string;
    planSubjects?: PlanSubject[]; 
}

export default Plan;