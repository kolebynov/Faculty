import React from "react";
import UrlHelper from "./UrlHelper";
import { Link } from 'react-router-dom'
import DataTypes from "../common/DataTypes";
import modelSchemaProvider from "../schemas/ModelSchemaProvider";

class ViewCreator {
    createLinkForModelPage(modelName, primaryColumnValue, caption) {
        return (<Link to={UrlHelper.getUrlForModelPage(modelName, primaryColumnValue)}>{caption}</Link>);
    }

    createViewForModelValue(value, columnName, schema, model) {
        const column = schema.getColumnByName(columnName);
        if (column.name === schema.displayColumnName) {
            return this.createLinkForModelPage(schema.name, model[schema.primaryColumnName], value);
        }
        if (column.type === DataTypes.LOOKUP) {
            let referenceSchema = modelSchemaProvider.getSchemaByName(column.referenceSchemaName);
            return this.createLinkForModelPage(column.referenceSchemaName, value[referenceSchema.primaryColumnName], value[referenceSchema.displayColumnName]);
        }

        return value;
    }
}

export default new ViewCreator();