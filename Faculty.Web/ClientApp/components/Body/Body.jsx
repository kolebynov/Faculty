import React from "react";
import { Switch, Route } from "react-router-dom";
import StudentSection from "../sections/StudentSection/StudentSection.jsx";
import GroupSection from "../sections/GroupSection/GroupSection.jsx";
import urlHelper from "../../utils/UrlHelper";

const Body = () => (
    <Switch>
        <Route path={urlHelper.getPathForModelSection("Student")} component={StudentSection} />
        <Route path={urlHelper.getPathForModelSection("Group")} component={GroupSection} />
    </Switch>
)

export default Body;