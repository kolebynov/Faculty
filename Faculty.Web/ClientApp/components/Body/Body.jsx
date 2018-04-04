import React from "react";
import { Switch, Route } from "react-router-dom";
import StudentSection from "../sections/StudentSection/StudentSection.jsx";
import urlHelper from "../../utils/UrlHelper";

const Body = () => (
    <Switch>
        <Route path={urlHelper.getPathForModelSection("Student")} component={StudentSection} />
    </Switch>
)

export default Body;