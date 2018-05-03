import React from "react";
import PropTypes from "prop-types";
import viewCreator from "../../../utils/ViewCreator";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import modelUtils from "../../../utils/ModelUtils";
import DataTypes from "../../../common/DataTypes";
import ApiService from "../../../services/ApiService";
import dataTypeConverterProvider from "../../../common/DataTypeConverterProvider";

class BaseModelPage extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            model: { ...this.props.initialModel }
        };
    }

    componentWillMount() {
        const columns = this.props.modelSchema.getColumns();
        columns.forEach(column => {
            if (column.type === DataTypes.LOOKUP) {
                this._loadLookupCollection(column);
            }
        });
    }

    render() {
        return (
            <div>
                {this.renderBody()}
                {this.renderFooter()}
            </div>
        );
    }

    renderBody() {
        return null;
    }

    renderFooter() {
        return null;
    }

    renderEditComponent(columnName) {
        return viewCreator.createEditViewForModelValue(this.state.model[columnName], columnName, 
            this.props.modelSchema, this.state.model, this._onEditComponentChange, { "data-column": columnName })
    }

    _onEditComponentChange = (e) => {
        const newModel = { ...this.state.model };
        const column = this.props.modelSchema.getColumnByName(e.currentTarget.dataset.column);
        const value = e.currentTarget.value;
        newModel[column.name] = dataTypeConverterProvider.getConverter(column.type)
            .fromString(value, column, this.state.model);
        this.setState({
            model: newModel
        });
    }

    _loadLookupCollection(column) {
        const referenceSchema = modelSchemaProvider.getSchemaByName(column.referenceSchemaName);
        new ApiService(referenceSchema.resourceName).getItems()
            .then(response => this.setState({
                model: modelUtils.setLookupCollection(this.state.model, column.name, response.data)
            }));
    }
}

BaseModelPage.propTypes = {
    initialModel: PropTypes.object,
    modelSchema: PropTypes.object.isRequired
};

export default BaseModelPage;