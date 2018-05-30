import queryString from "query-string";

const xsrfMethods = ["POST", "PUT", "DELETE"];

class ApiService {
    constructor(resourceName) {
        this.apiRoute = `/api/${resourceName}`;
        this._defaultRequestOptions = {
            credentials: "include",
            headers: {
                "Content-Type": "application/json"
            }
        };
    }

    getItems(id, options, linkedResouceName) {
        id = id || "";
        return this._request(`${this.apiRoute}/${id}${linkedResouceName ? `/${linkedResouceName}` : ""}${options ? "?" + queryString.stringify(options) : ""}`);
    }

    addItem(item) {
        return this._request(this.apiRoute, "POST", item);
    }

    updateItem(id, item) {
        return this._request(`${this.apiRoute}/${id}`, "PUT", item);
    }

    deleteItem(id) {
        return this._request(`${this.apiRoute}/${id}`, "DELETE");
    }

    _request(url, method = "GET", data) {
        return fetch(url, this._getRequestConfig(method, data))
            .then(response => response.json());
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

export default ApiService;