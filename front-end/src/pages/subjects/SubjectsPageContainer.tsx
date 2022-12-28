// React
import { useEffect, useState } from "react";
// Components
import HomeSubject from "models/HomeSubject";
import SubjectsPage from "./SubjectsPage";

const SubjectsPageContainer = () => {
    const [subjects, setSubjects] = useState<HomeSubject[]>([]);

    useEffect(() => {
        console.log("use effect");
        if(subjects.length === 0){
            setSubjects([
                {
                    id: 1,
                    name: "polski",
                    ects: 1,
                },
                {
                    id: 2,
                    name: "angieski",
                    ects: 1,
                },
                {
                    id: 3,
                    name: "matma",
                    ects: 1,
                },
                {
                    id: 4,
                    name: "historia",
                    ects: 1,
                },
            ]);
        }

    }, [subjects]);

   

    return <SubjectsPage
        subjects={subjects}
        setSubjects={setSubjects}
    />;
}

export default SubjectsPageContainer;