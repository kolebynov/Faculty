import React from "react";
import PropTypes from "prop-types";

class BaseModelPage extends React.Component {
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
}

BaseModelPage.propTypes = {
    modelName: PropTypes.string
};

export default BaseModelPage;