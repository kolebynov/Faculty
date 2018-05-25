import React from "react";
import DataGrid from "../DataGrid/DataGrid.jsx";
import PropTypes from "prop-types";
import {Toolbar, ToolbarGroup, ToolbarSeparator, ToolbarTitle} from 'material-ui/Toolbar';

class Detail extends React.PureComponent {
    render() {
        return (
            <div>
                {this._renderToolbar()}
                <DataGrid modelName={this.props.modelName} rootModel={this.props.rootModel} itemsPerPage={10} />
            </div>
        );
    }

    _renderToolbar() {
        return (
            <Toolbar>
                <ToolbarGroup>
                    <ToolbarTitle text={this.props.caption}/>
                </ToolbarGroup>
                <ToolbarGroup>

                </ToolbarGroup>
            </Toolbar>
        );
    }
}

Detail.propTypes = {
    modelName: PropTypes.string.isRequired,
    rootModel: PropTypes.object.isRequired,
    caption: PropTypes.string.isRequired
};

export default Detail;