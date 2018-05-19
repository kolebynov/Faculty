import React from "react";
import DataGrid from "../DataGrid/DataGrid.jsx";
import PropTypes from "prop-types";

class Detail extends React.Component {
    render() {
        return (
            <div>
                {this.props.caption}
                <DataGrid modelName={this.props.modelName} rootModel={this.props.rootModel} />
            </div>
        );
    }
}

Detail.propTypes = {
    modelName: PropTypes.string.isRequired,
    rootModel: PropTypes.object.isRequired,
    caption: PropTypes.string.isRequired
};

export default Detail;