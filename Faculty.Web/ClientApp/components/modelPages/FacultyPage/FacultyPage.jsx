import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class FacultyPage extends BaseModelSchemaPage {
    _renderBody() {
        return (
            <div>
                {this.renderEditComponent("name")}
                {this.renderDetail("Specialty")}
                {this.renderDetail("Group")}
            </div>
        );
    }
}

export default FacultyPage;