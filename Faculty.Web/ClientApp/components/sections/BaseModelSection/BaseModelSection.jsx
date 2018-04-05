import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import DataGrid from "../../DataGrid/DataGrid.jsx";
import ApiService from "../../../services/ApiService";

class BaseModelSection extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            data: []
        };
    }

    componentDidMount() {
        this._loadData();
    }

    render() {
        return (
            <div>
                <DataGrid modelName={this.props.modelName} data={this.state.data} />
            </div>
        );
    }

    _loadData() {
        let apiService = new ApiService(modelSchemaProvider.getSchemaByName(this.props.modelName).resourceName);
        apiService.getItems()
            .then(response => this.setState({
                data: response.data
            }));
    }
}

BaseModelSection.propTypes = {
    modelName: PropTypes.string
};

export default BaseModelSection;