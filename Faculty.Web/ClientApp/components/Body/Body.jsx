import React from "react";
import { Switch, Route } from "react-router-dom";
import SectionRenderer from "../sections/SectionRenderer/SectionRenderer.jsx";
import StudentPage from "../modelPages/StudentPage/StudentPage.jsx";
import urlHelper from "../../utils/UrlHelper";

const Body = () => (
    <Switch>
        <Route path={urlHelper.getPathForModelSection()} component={SectionRenderer} />

        <Route path={urlHelper.getPathForModelPage("Student")} component={StudentPage} />
    </Switch>
)

export default Body;