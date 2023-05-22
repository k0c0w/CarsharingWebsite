import axios from "axios"



class AxiosWrapper {
    constructor(url = 'https://localhost:7129/admin') {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=UTF-8',
                "Access-Control-Allow-Origin": "https://localhost:7129",
                "Access-Control-Allow-Origin": "http://localhost:3000",
                "Access-Control-Allow-Credentials": "true"
            },
            withCredentials: true,
        };

        this.axiosInstance = axios.create(options);

        options.baseURL = 'https://localhost:7129/api'
        this.axiosInstanceAuthorize = axios.create(options);
    };

    getTariffs = async () => {
        var result = await this._get("/tariff/all");
        return result;
    }

    deleteTariffs = async (body) => {
        var result = await this._post("/tariff/delete", body); 
        return result;
    }

    updateTariff = async (body) => {
        var result = {};
        var _body = {};
        _body.price = body.price;
        _body.description = body.description;
        _body.name = body.name
        _body.MaxMileage = body.max_mileage
        console.log("-----------")
        // _body = JSON.stringify( _body)
        console.log(_body)
        if (body.id === undefined)
        {
            result.error = "no id provided";
            result.status = "404";
            return result;
        }
        debugger;
        result = await this._put("/tariff/edit/"+body.id, _body); 
        return result;
    }

    createTariff = async (body) => {
        var result = await this._post("/tariff/create", body);
        return result
    }

    createCarModel = async (body) => {
        var result = await this._post("/car/model/create", body);
        return result
    }

    getCars = async () => {
        var result = await this._get("/car/models");
        return result;
    }

    login = async (body) => {
        this.become()
        body = {
            email: "user@example.com",
            password: "veryCoolEmail1!!+"
        }
        var result = await this._post("/auth/login", body);

        return result
    }

    become = async () => {
        var result = await this._get("/auth/admin");
        return result
    }

    register = async () => {
        var body = 
            {
                email: "user@example.com",
                password: "veryCoolEmail1!!+",
                name: "Марсель",
                surname: "Альметов",
                birthdate: "1991-05-21T20:29:53.682Z",
                accept: "on"
            }
        
        var result = await this.axiosInstanceAuthorize.post("/Account/register", body);
        return result
    }


    isAdmin = async () => {
        return await this._get('/auth/isAdmin');
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
                    result.error = error.response.data.error ?? error.response.statusText;
                    result.status = error.response.status;
                }
                else
                    alert("Ошибка при обработке запроса. Проверьте подключение к интернету и попробуйте снова.");
            })
        return result;
    }

    async _put(endpoint, model) {
        const result = {successed: false};
        await this.axiosInstance.put(endpoint,JSON.stringify( model ))
            .then(response => {
                result.status = response.status; 
                result.successed = true;
            })
            .catch(error => {
                if(error.response){
                    result.error = error.response.data.error ?? error.response.statusText;
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
                    response.error = error.response.data.error ?? error.response.statusText;
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


