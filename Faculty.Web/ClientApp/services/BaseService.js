import getHistory from "../common/History";
import urlHelper from "../utils/UrlHelper";
import ApiException from "../exceptions/ApiException";

const xsrfMethods = ["POST", "PUT", "DELETE"];
const statusCodes = {
    UNAUTHORIZED: 401
};

class BaseService {
    _defaultRequestOptions = {
        credentials: "include",
        headers: {
            "Content-Type": "application/json"
        }
    }

    _request(url, method = "GET", data) {
        return fetch(url, this._getRequestConfig(method, data))
            .then(response => {
                if (response.status === statusCodes.UNAUTHORIZED) {
                    getHistory().push(urlHelper.getLoginPageUrl());
                }
                
                return response.json();
            });
    }

    _getRequestConfig(method = "GET", data) {
        let config = Object.assign({}, this._defaultRequestOptions, { method: method });
        if (method !== "GET" && data) {
            config.body = JSON.stringify(data);
        }
        
        if (xsrfMethods.indexOf(method) > -1) {
            config.headers.RequestVerificationToken = window.xsrfToken;
        }
        return config;
    }
}

export default BaseService;