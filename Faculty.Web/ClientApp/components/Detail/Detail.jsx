import React from "react";
import DataGrid from "../DataGrid/DataGrid.jsx";
import PropTypes from "prop-types";

class Detail extends React.PureComponent {
    render() {
        return (
            <div>
                {this.props.caption}
                <DataGrid modelName={this.props.modelName} rootModel={this.props.rootModel} itemsPerPage={10} />
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