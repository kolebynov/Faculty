import React from "react";
import PropTypes from "prop-types";

class BaseModelPage extends React.Component {
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

    
}

BaseModelPage.propTypes = {
    initialModel: PropTypes.object
};

export default BaseModelPage;