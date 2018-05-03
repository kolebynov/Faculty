import React from "react";
import BaseModelPage from "../BaseModelPage/BaseModelPage.jsx";
import ApiService from "../../../services/ApiService";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import PropTypes from "prop-types";
import modelUtils from "../../../utils/ModelUtils";
import dataTypeConverterProvider from "../../../common/DataTypeConverterProvider";
import DataTypes from "../../../common/DataTypes";

class BaseModelSchemaPage extends BaseModelPage {
    componentWillMount() {
        super.componentWillMount();
        this._loadModel();
    }

    renderFooter() {
        return (<input type="button" onClick={this._save}></input>);
    }

    _loadModel() {
        if (this.props.primaryColumnValue) {
            new ApiService(this.props.modelSchema.resourceName)
                .getItems(this.props.primaryColumnValue)
                .then(response => this.setState({
                    model: { ...this.state.model, ...response.data[0] }
                }));
        }
    }

    _save = () => {
        const model = this.state.model;
        const modelSchema = this.props.modelSchema;
        new ApiService(modelSchema.resourceName)
            .updateItem(modelUtils.getPrimaryValue(model, modelSchema), modelUtils.getModelForUpdate(model, modelSchema));
    }
}

BaseModelSchemaPage.propTypes = {
    primaryColumnValue: PropTypes.string
}

export default BaseModelSchemaPage;