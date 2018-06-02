import ModelSchema from "./ModelSchema";
import DataTypes from "../common/DataTypes";
import ModelColumnSchema from "./ModelColumnSchema";

const schemas = [
    new ModelSchema({
        name: "Student",
        caption: "Студенты",
        primaryColumnName: "id",
        displayColumnName: "name",
        resourceName: "students",
        columns: [
            new ModelColumnSchema({
                name: "id",
                type: DataTypes.TEXT,
            }),
            new ModelColumnSchema({
                name: "name",
                type: DataTypes.TEXT,
                caption: "Имя"
            }),
            new ModelColumnSchema({
                name: "firstName",
                type: DataTypes.TEXT,
                caption: "Фамилия"
            }),
            new ModelColumnSchema({
                name: "group",
                type: DataTypes.LOOKUP,
                referenceSchemaName: "Group",
                caption: "Группа",
                keyColumnName: "groupId"
            })
        ]
    }),
    new ModelSchema({
        name: "Group",
        caption: "Группы",
        primaryColumnName: "id",
        displayColumnName: "name",
        resourceName: "groups",
        columns: [
            new ModelColumnSchema({
                name: "id",
                type: DataTypes.TEXT
            }),
            new ModelColumnSchema({
                name: "name",
                type: DataTypes.TEXT,
                caption: "Имя"
            }),
            new ModelColumnSchema({
                name: "specialty",
                type: DataTypes.LOOKUP,
                referenceSchemaName: "Specialty",
                caption: "Специальность",
                keyColumnName: "specialtyId"
            })
        ]
    }),
    new ModelSchema({
        name: "Specialty",
        caption: "Специальности",
        primaryColumnName: "id",
        displayColumnName: "name",
        resourceName: "specialties",
        columns: [
            new ModelColumnSchema({
                name: "id",
                type: DataTypes.TEXT
            }),
            new ModelColumnSchema({
                name: "name",
                type: DataTypes.TEXT,
                caption: "Имя"
            }),
            new ModelColumnSchema({
                name: "faculty",
                type: DataTypes.LOOKUP,
                referenceSchemaName: "Faculty",
                caption: "Факультет",
                keyColumnName: "facultyId"
            })
        ]
    }),
    new ModelSchema({
        name: "Faculty",
        caption: "Факультеты",
        primaryColumnName: "id",
        displayColumnName: "name",
        resourceName: "faculties",
        columns: [
            new ModelColumnSchema({
                name: "id",
                type: DataTypes.TEXT
            }),
            new ModelColumnSchema({
                name: "name",
                type: DataTypes.TEXT,
                caption: "Имя"
            })
        ]
    })
];

export default schemas;