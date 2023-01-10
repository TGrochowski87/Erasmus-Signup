// React
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
// Components
import Plan from "models/Plan";
import PlansPage from "./PlansPage";

interface Props {
    isCoordinator: boolean;
  }

const PlansPageContainer = ({isCoordinator}: Props) => {
    const { id } = useParams();
    const [plans, setPlans] = useState<Plan[]>([]);
    const [currentPlan, setCurrentPlan] = useState<Plan | undefined>(undefined);
    // const [isCoordinator, setIsCoordinator] = useState<boolean>(true);

    useEffect(() => {
        if(plans.length === 0){
            setPlans([
                {
                    id: 1,
                    specialtyId: 1,
                    name: "plan1",
                },
                {
                    id: 2,
                    specialtyId: 1,
                    name: "plan2",
                },
                {
                    id: 3,
                    specialtyId: 2,
                    name: "plan3",
                },
                {
                    id: 4,
                    specialtyId: 3,
                    name: "plan4",
                },
            ]);
        }

        if(currentPlan === undefined){
            loadPlan(id);
        }
        
        // setIsCoordinator(true);
    }, [plans, currentPlan, id]);
    
    const loadPlan = (id: string|undefined) => {
        if(id){
            window.history.replaceState({}, "", (isCoordinator ? "/plans/coordinator/" : "/plans/") + id);
            setCurrentPlan({
                id: 4,
                specialtyId: 3,
                name: "Plan testowy",
                planSubjects: [
                    {
                        id: 1,
                        name: "polish",
                        homeSubject: {
                            id: 1,
                            name: "polski",
                            ects: 4,
                        },
                        ects: 5,
                    },
                    {
                        id: 2,
                        name: "math",
                        homeSubject: {
                            id: 3,
                            name: "matma",
                            ects: 1,
                        },
                        ects: 1,
                    },
                    {
                        id: 5,
                        name: "biology",
                        homeSubject: {
                            id: 1,
                            name: "biologia",
                            ects: 5,
                        },
                        ects: 4,
                    }
                ]
            });
        }
    }

    return <PlansPage
        currentPlan={currentPlan}
        plans={plans}
        setCurrentPlan={setCurrentPlan}
        loadPlan={loadPlan}
        isCoordinator={isCoordinator}
    />;
}

export default PlansPageContainer;