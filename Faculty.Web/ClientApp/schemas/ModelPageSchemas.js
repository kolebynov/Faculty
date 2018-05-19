import StudentPage from "../components/modelPages/StudentPage/StudentPage.jsx";
import GroupPage from "../components/modelPages/GroupPage/GroupPage.jsx";

const schemas = [
    {
        modelName: "Student",
        component: StudentPage
    },
    {
        modelName: "Group",
        component: GroupPage
    }
];

export default schemas;