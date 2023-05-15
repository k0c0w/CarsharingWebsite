import axios from "axios"

export default class API {
    constructor(url = 'https://localhost:7129/api') {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=UTF-8',
                "Access-Control-Allow-Origin": "https://localhost:7129"
            },
            defaults: {
                withCredentials: true,
            }
        };

        this.axiosInstance = axios.create(options);
    };

    login = async (model) => {
        var result = null
        this.axiosInstance.defaults.withCredentials = true;
        await this.axiosInstance.post(`/account/login/`, model)
            .then(response => {
                result = { message: response.data, isFailed: false }
            })
            .catch(error => {
                // var errorMessage = error.response.data
                // console.log(errorMessage)
                // this._renameKeys(errorMessage, keysTranslations)
                result = { message: error.response.data, isFailed: true }
            })
        return result;
    }

    getDataFromEndpoint(endpoint, dataSetterFunction) {
        this.axiosInstance.get(endpoint)
            .then(r => {
                dataSetterFunction(r.data)
            })
            .catch(err => console.log(`Error while recieving data from ${endpoint}`));
    }

    _renameKey(obj, oldKey, newKey) {
        obj[newKey] = obj[oldKey];
        delete obj[oldKey];
    }

    _renameKeys(obj, keys) {
        
        for (var key in keys){
            for (var elem in obj) {
                console.log(elem)
                if (elem === key) {
                    this._renameKey(obj, elem, keys[key])
                }
            }
        }
    }
}