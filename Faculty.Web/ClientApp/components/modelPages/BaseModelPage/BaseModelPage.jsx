import React from "react";
import PropTypes from "prop-types";
import viewCreator from "../../../utils/ViewCreator";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";

class BaseModelPage extends React.PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            model: { ...this.props.initialModel }
        };
    }

    render() {
        return (
            <div>
                {this.renderBody()}
            </div>
        );
    }

    renderBody() {
        return null;
    }

    renderEditComponent(columnName) {
        return viewCreator.createEditViewForModelValue(this.state.model[columnName], columnName, 
            modelSchemaProvider.getSchemaByName(this.props.modelName), this.state.model, this._onEditComponentChange,
            { "data-column": columnName })
    }

    _onEditComponentChange = (e) => {
        debugger;
        const newModel = { ...this.state.model };
        newModel[e.currentTarget.dataset.column] = e.currentTarget.value;
        this.setState({
            model: newModel
        });
    }
}

BaseModelPage.propTypes = {
    initialModel: PropTypes.object,
    modelName: PropTypes.string.isRequired
};

export default BaseModelPage;