import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class StudentPage extends BaseModelSchemaPage {
    _renderBody() {
        return (
            <div>
                {this.renderEditComponent("name")}
                {this.renderEditComponent("firstName")}
                {this.renderEditComponent("group")}
            </div>
        );
    }
}

export default StudentPage;