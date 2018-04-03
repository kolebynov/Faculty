import React from "react";
import PropTypes from "prop-types";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import DataGrid from "../../DataGrid/DataGrid.jsx";

class BaseModelSection extends React.Component {
    render() {
        return (
            <div>
                <DataGrid modelName={this.props.modelName} />
            </div>
        );
    }
}

BaseModelSection.propTypes = {
    modelName: PropTypes.string
};

export default BaseModelSection;