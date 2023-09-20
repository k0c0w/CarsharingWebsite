import axios from "axios"


class AxiosWrapper {
    constructor(url = 'http://localhost:80/api') {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=UTF-8',
                "Access-Control-Allow-Origin": "http://localhost:80",
                "Access-Control-Allow-Origin": "http://localhost:3001",
                "Access-Control-Allow-Credentials": "true",
                "X-Requested-With": "XMLHttpRequest"
            },
            withCredentials: true,
        };

        this.axiosInstance = axios.create(options);
    };

    book = async (model) => {
        return await this._post('booking/rent', model); 
    }

    tariffs = async (id) => {
        if(id)
            return await this._get(`tariffs/${id}`);
        return await this._get('tariffs');
    }

    car_description = async (modelId) => await this._get(`cars/model/${modelId}`);

    car_prototypes = async (tariffId) => {
        return await this._get(`cars/models/${tariffId}`);
    }

    available_cars = async (params) => await this._get('cars/available', params);

    documents = async () => {
        return await this._get(`/information/documents`);
    }

    news = async () => {
        return await this._get(`/information/news`);
    }

    login = async (form) => {
        return await this._post(`/account/login/`, this._getModelFromForm(form));
    }

    logout = async () => {
        return await this._post(`/account/logout/`);
    }

    register = async (form) => {
        return await this._post('/account/register', this._getModelFromForm(form));
    }

    IsUserAuthorized = async () => {
        return await this._get('/Account/IsAuthorized');
    }

    personalInfo = async ()  => {
        return await this._get('/Account/PersonalInfo');
    }

    editPersonalInfo = async (form)  => {
        return await this._post('/Account/PersonalInfo/Edit', this._getModelFromForm(form));
    }

    profile = async () => {
        return await this._get('/Account');
    }

    tryChangePassword = async (form) => {
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
