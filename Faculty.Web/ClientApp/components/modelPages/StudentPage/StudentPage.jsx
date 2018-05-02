import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class StudentPage extends BaseModelSchemaPage {
    renderBody() {
        return (
            <div>
                {this.renderEditComponent("name")}
                {this.renderEditComponent("group")}
            </div>
        );
    }
}

export default StudentPage;