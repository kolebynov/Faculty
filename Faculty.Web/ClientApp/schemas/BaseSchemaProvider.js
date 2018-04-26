class BaseSchemaProvider {
    constructor(schemas) {
        this._schemas = schemas;
    }

    getSchemas() {
        return this._schemas;
    }

    getSchemaByFilter(filter, notFoundMessage) {
        let schema = this._schemas.find(filter);
        if (schema) {
            return schema;
        }
        else {
            throw new Error(notFoundMessage);
        }
    }
}

export default BaseSchemaProvider;