import modelSchemaProvider from "../schemas/ModelSchemaProvider";

class UrlHelper {
    getPathForModelSection(modelName) {
        return this.getUrlForModelSection(modelName);
    }

    getPathForModelPage(modelName) {
        return `/page/${modelSchemaProvider.getSchemaByName(modelName).resourceName}/${UrlHelper.primaryColumnName}`;
    }

    getUrlForModelSection(modelName) {
        return `/section/${modelSchemaProvider.getSchemaByName(modelName).resourceName}`;
    }

    getUrlForModelPage(modelName, primaryColumnValue) {
        return this.getPathForModelPage(modelName).replace(new RegExp(UrlHelper.primaryColumnName), primaryColumnValue);
    }
}

UrlHelper.primaryColumnName = ":primaryColumnValue";

export default new UrlHelper();