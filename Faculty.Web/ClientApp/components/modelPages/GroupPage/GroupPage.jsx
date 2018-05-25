import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class GroupPage extends BaseModelSchemaPage {
    _renderBody() {
        return (
            <div>
                {this.renderEditComponent("name")}
                {this.renderEditComponent("specialty")}
                {this.renderDetail("Student")}
            </div>
        );
    }
}

export default GroupPage;