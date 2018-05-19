import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class GroupPage extends BaseModelSchemaPage {
    renderBody() {
        return (
            <div>
                {this.renderEditComponent("name")}
                {this.renderEditComponent("specialty")}
            </div>
        );
    }
}

export default GroupPage;