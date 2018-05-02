import React from "react";
import UrlHelper from "./UrlHelper";
import { Link } from 'react-router-dom'
import DataTypes from "../common/DataTypes";
import modelSchemaProvider from "../schemas/ModelSchemaProvider";
import modelPageSchemaProvider from "../schemas/ModelPageSchemaProvider";
import TextField from 'material-ui/TextField';

class ViewCreator {
    createLinkForModelSection(modelName, caption) {
        return (<Link to={UrlHelper.getUrlForModelSection(modelName)}>{caption}</Link>);
    }

    createLinkForModelPage(modelName, primaryColumnValue, caption) {
        return (<Link to={UrlHelper.getUrlForModelPage(modelName, primaryColumnValue)}>{caption}</Link>);
    }

    createViewForModelValue(value, columnName, schema, model) {
        const column = schema.getColumnByName(columnName);
        if (column.name === schema.displayColumnName && modelPageSchemaProvider.findSchemaByModelName(schema.name)) {
            return this.createLinkForModelPage(schema.name, model[schema.primaryColumnName], value);
        }
        if (column.type === DataTypes.LOOKUP) {
            let referenceSchema = modelSchemaProvider.getSchemaByName(column.referenceSchemaName);
            if (modelPageSchemaProvider.findSchemaByModelName(column.referenceSchemaName)) {
                return this.createLinkForModelPage(column.referenceSchemaName, 
                    value[referenceSchema.primaryColumnName], value[referenceSchema.displayColumnName]);
            }
            return (value || {})[referenceSchema.displayColumnName];
        }

        return value;
    }

    createEditViewForModelValue(value, columnName, schema, model, onChangeHandler, otherProps) {
        const column = schema.getColumnByName(columnName);
        debugger;
        switch (column.type) {
            default:
                return <TextField id={columnName} defaultValue={value} value={value} onChange={onChangeHandler} {...otherProps} floatingLabelText={column.caption || column.name} />;
        }
    }
}

export default new ViewCreator();