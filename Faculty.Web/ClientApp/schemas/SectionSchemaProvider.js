import schemas from "./SectionSchemas";

class SectionSchemaProvider {
    getSchemaByModelName(modelName) {
        let schema = schemas.find(s => s.modelName === modelName);
        if (schema) {
            return schema;
        }
        else {
            throw new Error(`Section schema by model name "${modelName}" not found.`);
        }
    }

    getSchemas() {
        return schemas;
    }
}

export default new SectionSchemaProvider();