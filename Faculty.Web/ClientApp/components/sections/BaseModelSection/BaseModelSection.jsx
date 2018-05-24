import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import DataGrid from "../../DataGrid/DataGrid.jsx";
import ApiService from "../../../services/ApiService";
import Pagination from "../../Pagination/Pagination.jsx";
import constants from "../../../utils/Constants";
import FlatButton from 'material-ui/FlatButton';
import urlHelper from "../../../utils/UrlHelper";

class BaseModelSection extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {}
    }

    render() {
        return (
            <div>
                {this.renderHeader()}
                <DataGrid modelName={this.props.modelName} rowActions={this._getDataGridRowActions()} 
                    onRowAction={this._onGridRowAction} ref="dataGrid"/>
            </div>
        );
    }

    renderHeader() {
        return (
            <div>
                {this.renderHeaderButtons()}
            </div>
        );
    }

    renderHeaderButtons() {
        return (
            <div>
                <FlatButton label="Добавить" onClick={this._onAddButtonClick}></FlatButton>
            </div>
        );
    }

    openEditPage(primaryValue) {
        const url = urlHelper.getUrlForModelPage(this.props.modelName, primaryValue);
        this.context.router.history.push(url);
    }

    removeRow(primaryValue) {
        this.refs.dataGrid.removeRow(primaryValue);
    }

    _onAddButtonClick = () => {
        this.openEditPage(constants.EMPTY_GUID);
    }

    _getDataGridRowActions() {
        return [
            {
                component: FlatButton,
                props: {
                    label: "Открыть"
                },
                actionPropName: "onClick",
                name: "open"
            },
            {
                component: FlatButton,
                props: {
                    label: "Удалить"
                },
                actionPropName: "onClick",
                name: "remove"
            }
        ];
    }

    _onGridRowAction = (actionName, rowPrimaryValue) => {
        debugger;
        switch (actionName) {
            case "open": 
                this.openEditPage(rowPrimaryValue);
                break;
            case "remove":
                this.removeRow(rowPrimaryValue);
                break;
        }
    }
}

BaseModelSection.propTypes = {
    modelName: PropTypes.string.isRequired
};

BaseModelSection.contextTypes = {
    router: PropTypes.object.isRequired
};

export default BaseModelSection;