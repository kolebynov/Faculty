import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class SpecialtyPage extends BaseModelSchemaPage {
    _renderBody() {
        return (
            <div>
                {this.renderEditComponent("name")}
                {this.renderEditComponent("faculty")}
                {this.renderDetail("Group")}
            </div>
        );
    }
}

export default SpecialtyPage;