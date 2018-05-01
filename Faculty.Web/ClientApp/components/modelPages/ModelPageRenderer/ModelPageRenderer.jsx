import React from "react";
import modelPageSchemaProvider from "../../../schemas/ModelPageSchemaProvider";
import modelSchemaProvider from "../../../schemas/ModelSchemaProvider";
import BaseModelPage from "../BaseModelPage/BaseModelPage.jsx";
import urlHelper from "../../../utils/UrlHelper";

class ModelPageRenderer extends React.PureComponent {
    render() {
        const resourceName = this.props.match.params[urlHelper.resourceName];
        const primaryColumnValue = this.props.match.params[urlHelper.primaryColumnValue];
        const modelName = modelSchemaProvider.getSchemaByResourceName(resourceName).name;
        const ModelPageComponent = modelPageSchemaProvider.getSchemaByModelName(modelName).component
            || BaseModelPage;
        return (<ModelPageComponent modelName={modelName} primaryColumnValue={primaryColumnValue}>{this.props.children}</ModelPageComponent>);
    }
}

export default ModelPageRenderer;