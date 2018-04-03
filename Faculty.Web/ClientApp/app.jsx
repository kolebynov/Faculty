import React from "react";
import LoginPage from "./pages/LoginPage/LoginPage.jsx";
import MainPage from "./pages/MainPage/MainPage.jsx";
import { Switch, Route } from "react-router-dom";
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

const App = () => (
    <MuiThemeProvider>
        <Switch>
            <Route path = "/login" component = {LoginPage} />
            <Route path = "/" component = {MainPage} />
        </Switch>
    </MuiThemeProvider>
)

export default App;