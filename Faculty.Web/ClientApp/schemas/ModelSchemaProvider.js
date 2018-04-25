import schemas from "./Schemas";

class ModelSchemaProvider {
    getSchemaByName(name) {
        let schema = schemas.find(s => s.name === name);
        if (schema) {
            return schema;
        }
        else {
            throw new Error(`Model schema "${name}" not found.`);
        }
    }

    getSchemas() {
        return schemas;
    }
}

export default new ModelSchemaProvider();