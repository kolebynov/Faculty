import React from "react";
import BaseModelSchemaPage from "../BaseModelSchemaPage/BaseModelSchemaPage.jsx";

class StudentPage extends BaseModelSchemaPage {
    renderBody() {
        return (
            <div>
                {this.state.model.name}
            </div>
        );
    }
}

export default StudentPage;