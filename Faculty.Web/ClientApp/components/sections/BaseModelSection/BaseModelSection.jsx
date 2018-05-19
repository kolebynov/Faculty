import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import DataGrid from "../../DataGrid/DataGrid.jsx";
import ApiService from "../../../services/ApiService";
import Pagination from "../../Pagination/Pagination.jsx";

class BaseModelSection extends React.PureComponent {
    constructor(props) {
        super(props);
        this.state = {}
    }

    render() {
        return (
            <div>
                <DataGrid modelName={this.props.modelName} />
            </div>
        );
    }
}

BaseModelSection.propTypes = {
    modelName: PropTypes.string.isRequired
};

export default BaseModelSection;