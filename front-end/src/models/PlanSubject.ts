import HomeSubject from "./HomeSubject";

interface PlanSubject {
    id: number;
    homeSubject: HomeSubject;
    name: string;
    ects: number;
}

export default PlanSubject;