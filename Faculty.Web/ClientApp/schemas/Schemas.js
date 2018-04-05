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
                type: DataTypes.TEXT,
            },
            {
                name: "name",
                type: DataTypes.TEXT,
                caption: "Имя"
            },
            {
                name: "firstName",
                type: DataTypes.TEXT,
                caption: "Фамилия"
            },
            {
                name: "group",
                type: DataTypes.LOOKUP,
                referenceSchemaName: "Group",
                caption: "Группа"
            }
        ]
    }),
    new ModelSchema({
        name: "Group",
        primaryColumnName: "id",
        displayColumnName: "name",
        resourceName: "groups",
        columns: [
            {
                name: "id",
                type: DataTypes.TEXT
            },
            {
                name: "name",
                type: DataTypes.TEXT,
                caption: "Имя"
            },
            {
                name: "specialty",
                type: DataTypes.LOOKUP,
                referenceSchemaName: "Specialty",
                caption: "Специальность"
            }
        ]
    })
];

export default schemas;