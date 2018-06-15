class ApiException {
    constructor(apiErrors) {
        this._apiErrors = apiErrors;
    }

    getErrors() {
        return this._apiErrors;
    }

    toString() {
        return this._errorString || (this._errorString = this._createErrorString());
    }

    _createErrorString() {
        return this.getErrors().map(err => err.message).join("\n");
    }
}

export default ApiException;