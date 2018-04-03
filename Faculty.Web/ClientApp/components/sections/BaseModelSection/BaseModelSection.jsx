import React from "react";
import PropTypes from "prop-types";
import ModelSchemaProvider from "../../../schemas/ModelSchemaProvider";

class BaseModelSection extends React.Component {
    constructor() {
        super();
        this._modelSchemaProvider = new ModelSchemaProvider();
    }

    render() {
        return (
            <div>
                {JSON.stringify(this._modelSchemaProvider.getSchemaByName(this.props.modelName))}
            </div>
        );
    }
}

BaseModelSection.propTypes = {
    modelName: PropTypes.string
};

export default BaseModelSection;