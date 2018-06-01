import modelSchemaProvider from "../schemas/ModelSchemaProvider";

class UrlHelper {
    primaryColumnName = "primaryColumnValue";
    resourceName = "resourceName";
    returnUrl = "returnUrl"

    getPathForModelSection() {
        return `/section/:${this.resourceName}`;
    }

    getPathForModelPage() {
        return `/page/:${this.resourceName}/:${this.primaryColumnName}`;
    }

    getUrlForModelSection(modelName) {
        const resourceName = modelSchemaProvider.getSchemaByName(modelName).resourceName;
        return this.getPathForModelSection().replace(new RegExp(`:${this.resourceName}`), resourceName);
    }

    getUrlForModelPage(modelName, primaryColumnValue) {
        return this.getPathForModelPage(modelName)
            .replace(new RegExp(`:${this.primaryColumnName}`), primaryColumnValue)
            .replace(new RegExp(`:${this.resourceName}`), modelSchemaProvider.getSchemaByName(modelName).resourceName);
    }

    getLoginPageUrl(returnUrl = "/") {
        return `${this.getLoginPagePath()}?${this.returnUrl}=${returnUrl}`;
    }

    getLoginPagePath() {
        return `/login`;
    }
}

export default new UrlHelper();