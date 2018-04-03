import ModelSchema from "./ModelSchema";
import DataTypes from "../common/DataTypes";

const schemas = [
    new ModelSchema({
        name: "Student",
        primaryColumnName: "id",
        displayColumnName: "name",
        resourceName: "students",
        columns: [
            {
                name: "id",
                type: DataTypes.TEXT
            },
            {
                name: "name",
                type: DataTypes.TEXT
            },
            {
                name: "firstName",
                type: DataTypes.TEXT
            },
            {
                name: "group",
                type: DataTypes.LOOKUP,
                referenceSchemaName: "Group"
            }
        ]
    })
];

export default schemas;