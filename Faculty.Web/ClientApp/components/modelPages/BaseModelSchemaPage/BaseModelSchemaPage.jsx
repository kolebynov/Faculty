import React from "react";
import BaseModelPage from "../BaseModelPage/BaseModelPage.jsx";
import ApiService from "../../../services/ApiService";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import PropTypes from "prop-types";

class BaseModelSchemaPage extends BaseModelPage {
    componentWillMount() {
        this._loadModel();
    }

    _loadModel() {
        if (this.props.primaryColumnValue) {
            new ApiService(modelSchemaProvider.getSchemaByName(this.props.modelName).resourceName)
                .getItems(this.props.primaryColumnValue)
                .then(response => this.setState({
                    model: response.data[0]
                }));
        }
    }
}

BaseModelSchemaPage.propTypes = {
    primaryColumnValue: PropTypes.string
}

export default BaseModelSchemaPage;