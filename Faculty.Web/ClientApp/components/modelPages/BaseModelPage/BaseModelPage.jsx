import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import modelUtils from "../../../utils/ModelUtils";
import DataTypes from "../../../common/DataTypes";
import ApiService from "../../../services/ApiService";
import ModelValueEdit from "../../ModelValueEdit/ModelValueEdit.jsx";
import Detail from "../../Detail/Detail.jsx";

class BaseModelPage extends React.PureComponent {
    constructor(props) {
        super(props);
        
        this.state = {
            model: { ...this.props.initialModel },
            hasChanges: false
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
        return <ModelValueEdit columnName={columnName} schema={this.props.modelSchema} model={this.state.model} 
            onChange={this._onEditComponentChange}/>
    }

    renderDetail(detailModelName) {
        const rootModel = {
            name: this.props.modelSchema.name,
            primaryValue: modelUtils.getPrimaryValue(this.state.model, this.props.modelSchema)
        };
        if (!rootModel.name || !rootModel.primaryValue) {
            return null;
        }

        return <Detail modelName={detailModelName} rootModel={rootModel} 
            caption={modelSchemaProvider.getSchemaByName(detailModelName).getCaption()}/>
    }

    _onEditComponentChange = (newValue, column) => {
        const newModel = { ...this.state.model };
        newModel[column.name] = newValue;
        if (column.type === DataTypes.LOOKUP) {
            newModel[column.keyColumnName] = modelUtils.getPrimaryValue(newValue, 
                modelSchemaProvider.getSchemaByName(column.referenceSchemaName));
        }
        this.setState({
            model: newModel,
            hasChanges: true
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