import StudentPage from "../components/modelPages/StudentPage/StudentPage.jsx";
import GroupPage from "../components/modelPages/GroupPage/GroupPage.jsx";
import SpecialtyPage from "../components/modelPages/SpecialtyPage/SpecialtyPage.jsx";
import FacultyPage from "../components/modelPages/FacultyPage/FacultyPage.jsx";

const schemas = [
    {
        modelName: "Student",
        component: StudentPage
    },
    {
        modelName: "Group",
        component: GroupPage
    },
    {
        modelName: "Specialty",
        component: SpecialtyPage
    },
    {
        modelName: "Faculty",
        component: FacultyPage
    }
];

export default schemas;