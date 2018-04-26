import React from "react";
import { List, ListItem, makeSelectable } from "material-ui/List";
import UrlHelper from "../../utils/UrlHelper";
import PropTypes from "prop-types";

const SelectableList = makeSelectable(List);

class LeftPanel extends React.PureComponent {
    constructor(props, context) {
        debugger;
        super(props);
        this.state = {
            selectedSection: props.initialSelectedSection || context.router.match.params
        };
    }

    render() {
        return (
            <div id="LeftPanel">
                <SelectableList value={this.state.selectedSection} onChange={this._onSelectChange}>
                    {this.props.sections.map(sectionSchema => 
                        (<ListItem key={sectionSchema.modelName} primaryText={sectionSchema.caption} data-model-name={sectionSchema.modelName} 
                            onClick={this._onListItemClick} value={sectionSchema.modelName}></ListItem>)
                    )}
                </SelectableList>
            </div>
        );
    }

    _goToSection(modelName) {
        const url = UrlHelper.getUrlForModelSection(modelName);
        this.context.router.history.push(url);
    }

    _onListItemClick = (e) => {
        this._goToSection(e.currentTarget.dataset.modelName);
    }
    
    _onSelectChange = (e, modelName) => {
        this.setState({
            selectedSection: modelName
        });
    }
}

LeftPanel.contextTypes = {
    router: PropTypes.object.isRequired
};

LeftPanel.propTypes = {
    sections: PropTypes.array.isRequired,
    initialSelectedSection: PropTypes.string
};

export default LeftPanel;