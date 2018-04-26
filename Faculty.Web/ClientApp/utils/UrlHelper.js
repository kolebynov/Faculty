import modelSchemaProvider from "../schemas/ModelSchemaProvider";

class UrlHelper {
    primaryColumnName = "primaryColumnValue";
    resourceName = "resourceName";

    getPathForModelSection() {
        return `/section/:${this.resourceName}`;
    }

    getPathForModelPage(modelName) {
        return `/page/${modelSchemaProvider.getSchemaByName(modelName).resourceName}/:${this.primaryColumnName}`;
    }

    getUrlForModelSection(modelName) {
        const resourceName = modelSchemaProvider.getSchemaByName(modelName).resourceName;
        return this.getPathForModelSection().replace(new RegExp(`:${this.resourceName}`), resourceName);
    }

    getUrlForModelPage(modelName, primaryColumnValue) {
        return this.getPathForModelPage(modelName).replace(new RegExp(`:${this.primaryColumnName}`), primaryColumnValue);
    }
}

export default new UrlHelper();