import React from "react";
import LoginPage from "./pages/LoginPage/LoginPage.jsx";
import MainPage from "./pages/MainPage/MainPage.jsx";
import { Switch, Route } from "react-router-dom";

const App = () => (
    <Switch>
        <Route path = "/login" component = {LoginPage} />
        <Route path = "/" component = {MainPage} />
    </Switch>
)

export default App;