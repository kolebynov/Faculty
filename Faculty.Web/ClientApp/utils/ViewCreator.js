import React from "react";
import UrlHelper from "./UrlHelper";
import { Link } from 'react-router-dom'
import DataTypes from "../common/DataTypes";
import modelSchemaProvider from "../schemas/ModelSchemaProvider";
import modelPageSchemaProvider from "../schemas/ModelPageSchemaProvider";
import TextField from "material-ui/TextField";
import SelectField from "material-ui/SelectField";
import MenuItem from "material-ui/MenuItem";
import modelUtils from "../utils/ModelUtils";

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
            return this.createLinkForModelPage(schema.name, modelUtils.getPrimaryValue(model, schema), value);
        }
        if (column.type === DataTypes.LOOKUP) {
            let referenceSchema = modelSchemaProvider.getSchemaByName(column.referenceSchemaName);
            if (modelPageSchemaProvider.findSchemaByModelName(column.referenceSchemaName)) {
                return this.createLinkForModelPage(column.referenceSchemaName, 
                    modelUtils.getPrimaryValue(value, referenceSchema), modelUtils.getDisplayValue(value, referenceSchema));
            }
            return modelUtils.getDisplayValue(model, schema);
        }

        return value;
    }

    createEditViewForModelValue(value, columnName, schema, model, onChangeHandler, otherProps) {
        const column = schema.getColumnByName(columnName);
        switch (column.type) {
            case DataTypes.LOOKUP:
                return this.createSelectField(value, column, model);
            default:
                return <TextField id={columnName} defaultValue={value} onChange={onChangeHandler} {...otherProps} floatingLabelText={column.getCaption()} />;
        }
    }

    createSelectField(value, column, model, onChangeHandler) {
        const referenceSchema = modelSchemaProvider.getSchemaByName(column.referenceSchemaName);
        return (
            <SelectField floatingLabelText={column.getCaption()} value={modelUtils.getPrimaryValue(value, referenceSchema)} onChange={onChangeHandler}>
                {modelUtils.getLookupCollection(model, column.name).map(lookupValue => (
                    <MenuItem value={modelUtils.getPrimaryValue(lookupValue, referenceSchema)} primaryText={modelUtils.getDisplayValue(lookupValue, referenceSchema)} />
                ))}
            </SelectField>
        );
    }
}

export default new ViewCreator();