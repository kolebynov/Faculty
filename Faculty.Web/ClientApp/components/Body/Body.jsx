import React from "react";
import { Switch, Route } from "react-router-dom";
import StudentSection from "../sections/StudentSection/StudentSection.jsx";

const Body = () => (
    <Switch>
        <Route path = "/section/students" component = {StudentSection} />
    </Switch>
)

export default Body;