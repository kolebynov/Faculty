import React from "react";
import BaseModelPage from "../BaseModelPage/BaseModelPage.jsx";
import ApiService from "../../../services/ApiService";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import PropTypes from "prop-types";
import modelUtils from "../../../utils/ModelUtils";
import dataTypeConverterProvider from "../../../common/DataTypeConverterProvider";
import DataTypes from "../../../common/DataTypes";
import FlatButton from 'material-ui/FlatButton';
import constants from "../../../utils/Constants";

class BaseModelSchemaPage extends BaseModelPage {
    constructor() {
        super(...arguments);

        if (this._isEmptyGuid(this.props.primaryColumnValue)) {
            this.state.hasChanges = true;
        }
    }

    componentWillMount() {
        super.componentWillMount();
        this._loadModel();
    }

    _renderHeader() {
        return (
            <div>
                <FlatButton label="Сохранить" onClick={this._save}></FlatButton>
                <FlatButton label="Назад" onClick={this._back}></FlatButton>
            </div>
        );
    }

    _loadModel() {
        if (!this._isEmptyGuid(this.props.primaryColumnValue)) {
            new ApiService(this.props.modelSchema.resourceName)
                .getItems(this.props.primaryColumnValue)
                .then(response => this.setState({
                    model: { ...response.data[0], ...this.state.model }
                }));
        }
    }

    _save = () => {
        if (this.state.hasChanges) {
            const modelSchema = this.props.modelSchema;
            const model = modelUtils.getModelForUpdate(this.state.model, modelSchema);
            const apiService = new ApiService(modelSchema.resourceName);
            let updatePromise = null;
            if (!this._isEmptyGuid(this.props.primaryColumnValue)) {
                updatePromise = apiService.updateItem(modelUtils.getPrimaryValue(model, modelSchema), model);
            }
            else {
                updatePromise = apiService.addItem(model);
            }
            updatePromise
                .then(response => this._back())
                .catch(err => {
                    this.setState({
                        model: { ...this.state.model, errors: err.getErrors() }
                    });
                });
        }
        else {
            this._back();
        }
    }

    _back = () => {
        this.context.router.history.goBack();
    }

    _isEmptyGuid(guid) {
        return !guid || guid === constants.EMPTY_GUID;
    }
}

BaseModelSchemaPage.propTypes = {
    primaryColumnValue: PropTypes.string
}

BaseModelSchemaPage.contextTypes = {
    router: PropTypes.object.isRequired
};

export default BaseModelSchemaPage;