class ModelSchema {
    constructor(config) {
        Object.assign(this, config);
    }

    getColumnByName(name) {
        let column = this.columns.find(column => column.columnName === name);
        if (column) {
            return column;
        }
        else {
            throw new Error(`Column ${name} not found`);
        }
    }
}

export default ModelSchema;