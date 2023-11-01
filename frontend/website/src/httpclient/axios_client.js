import axios from "axios"


class AxiosWrapper {
    constructor(url = process.env.REACT_APP_WEBSITE_API_URL) {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=UTF-8',
                "Access-Control-Allow-Origin": process.env.REACT_LOCAL_HOST,
                "X-Requested-With": "XMLHttpRequest"
            },
            withCredentials: true,
        }

        this.axiosInstance = axios.create(options);
    }

    async getChatHistory(userId) {
        try{
            const history = await this.axiosInstance.get(`/chat/${userId}/history`);
            if (history.data)
                return history.data.sort(function (a, b) {
                    return a.time.localeCompare(b.time);
                });
            return [];
        }
        catch{
            return [];
        }
    }

    async book(model) {
        return await this._post('booking/rent', model);
    } 

    async tariffs(id) {
        if(id)
            return await this._get(`tariffs/${id}`);
        return await this._get('tariffs');
    }

    async car_description (modelId) {
        return await this._get(`cars/model/${modelId}`);
    }

    async car_prototypes (tariffId) {
        return await this._get(`cars/models/${tariffId}`);
    }

    async available_cars (params) {
        return await this._get('cars/available', params);
    }

    async documents() {
        return await this._get(`/information/documents`);
    }

    async news() {
        return await this._get(`/information/news`);
    }

    async login(form) {
        return await this._post(`/account/login/`, this._getModelFromForm(form));
    }

    async logout() {
        return await this._post(`/account/logout/`);
    }

    async register(form) {
        return await this._post('/account/register', this._getModelFromForm(form));
    }

    async IsUserAuthorized () {
        return await this._get('/Account/IsAuthorized');
    }

    async personalInfo() {
        return await this._get('/Account/PersonalInfo');
    }

    async editPersonalInfo(form) {
        return await this._post('/Account/PersonalInfo/Edit', this._getModelFromForm(form));
    }

    async profile() {
        return await this._get('/Account');
    }

    async tryChangePassword(form) {
        return await this._post('/Account/ChangePassword', this._getModelFromForm(form));
    }

    async _post(endpoint, model) {
        const result = {successed: false};
        await this.axiosInstance.post(endpoint, model)
            .then(response => {
                result.status = response.status; 
                result.successed = true;
            })
            .catch(error => {
                if(error.response){
                    result.error = error.response.data.error;
                    result.status = error.response.status;
                }
                else
                    alert("Ошибка при обработке запроса. Проверьте подключение к интернету и попробуйте снова.");
            })
        return result;
    }

    async _get(endpoint, params) {
        const response = {successed: false};
        await this.axiosInstance.get(endpoint, {params: params})
            .then(r => {
                response.response = r.response;
                response.successed = true;
                response.data = r.data;
                response.status = r.status;
            })
            .catch(error =>{ 
                if(error.response) {
                    response.error = error.response.data.error;
                    response.status = error.response.status;
                }
            else
                alert("Ошибка при обработке запроса. Проверьте подключение к интернету и попробуйте снова.")}
            );
        return response;
    }

    _getModelFromForm(form) {
        return Array.from(form.elements)
            .filter((element) => element.name)
            .reduce(
              (obj, input) => {
                let value = input.value;
                if(input.type==="date")
                    value = (new Date(input.value)).toJSON();
    
                return Object.assign(obj, { [input.name]:  value})},
              {}
            );
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


const API = new AxiosWrapper();
export default API;