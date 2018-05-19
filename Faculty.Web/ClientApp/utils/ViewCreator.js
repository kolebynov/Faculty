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
import dataTypeConverterProvider from "../common/DataTypeConverterProvider";
import Detail from "../components/Detail/Detail.jsx";

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
            return modelUtils.getDisplayValue(value, referenceSchema);
        }

        return value;
    }

    createEditViewForModelValue(value, columnName, schema, model, onChangeHandler) {
        const column = schema.getColumnByName(columnName);
        let editComponent = null;
        switch (column.type) {
            case DataTypes.LOOKUP:
                editComponent = this.createSelectField(value, column, model, onChangeHandler);
                break;
            default:
                editComponent = <TextField id={columnName} value={value} onChange={(e, newValue) => 
                    this._onEditComponentChange(newValue, column, model, onChangeHandler)} 
                    floatingLabelText={column.getCaption()} />;
        }

        return <div>{editComponent}</div>
    }

    createSelectField(value, column, model, onChangeHandler) {
        const referenceSchema = modelSchemaProvider.getSchemaByName(column.referenceSchemaName);
        return (
            <SelectField floatingLabelText={column.getCaption()} value={modelUtils.getPrimaryValue(value, referenceSchema)} 
                onChange={(e, index, newValue) => this._onEditComponentChange(newValue, column, model, onChangeHandler)}>
                {modelUtils.getLookupCollection(model, column.name).map(lookupValue => {
                    const primaryValue = modelUtils.getPrimaryValue(lookupValue, referenceSchema);
                    return (
                        <MenuItem key={primaryValue} value={primaryValue} primaryText={modelUtils.getDisplayValue(lookupValue, referenceSchema)} />
                    );
                })}
            </SelectField>
        );
    }

    createDetail(detailModelName, rootModel, otherProps) {
        return <Detail modelName={detailModelName} rootModel={rootModel} {...otherProps} />;
    }

    _onEditComponentChange(rawValue, column, model, onChangeHandler) {
        const newValue = dataTypeConverterProvider.getConverter(column.type)
            .fromString(rawValue, column, model);
        onChangeHandler(newValue, column);
    }
}

export default new ViewCreator();