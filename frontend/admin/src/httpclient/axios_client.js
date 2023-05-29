import axios from "axios"



class AxiosWrapper {
    constructor(url = 'https://localhost:7129/api/admin') {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "https://localhost:7129",
                "Access-Control-Allow-Origin": "http://localhost:3000",
                "Access-Control-Allow-Credentials": "true",
                "X-Requested-With": "XMLHttpRequest"
            },
            withCredentials: true,
        };

        this.axiosInstance = axios.create(options);

        options.baseURL = 'https://localhost:7129/api/'
        this.mainSiteAxios = axios.create(options);
    };
    
    //Posts
    getPosts = async () => {
        const result = await this._get("/post/posts");
        return result;
    }

    getPostById = async (id) => {
        const result = await this._get("/post/posts"+id);
        return result;
    }

    deletePost = async (id) => {
        const result = await this._delete(`/post/delete/${id}`);
        return result;
    }

    updatePost = async (body) => {
        let result = {};
        const _body = {};
        _body.title = body.title;
        _body.body = body.body;
        if (!body.id)
        {
            result.error = "no id provided";
            result.status = "404";
            return result;
        }
        console.log(_body,body)
        result = await this._put("post/edit/"+body.id, _body);

        return result;
    }

    createPost = async (body) => {
        const result = await this._post("/post/create", body);
        return result
    }
    

    // Tariffs
    getTariffs = async () => {
        const result = await this._get("/tariff/all");
        return result;
    }

    getTariffById = async (id) => {
        const result = await this._get("/tariff/"+id);
        return result;
    }

    deleteTariff = async (id) => {
        const result = await this._delete(`/tariff/delete/${id}`); 
        return result;
    }

    changeTraiffState = async(id, state) => {
        const response = await this._put(`/tariff/setstate/${id}`, state)
        return response
    }

    updateTariff = async (body) => {
        let result = {};
        const _body = {};
        _body.price = body.price;
        _body.description = body.description;
        _body.name = body.name
        _body.max_millage = body.max_millage;
        if (!body.id)
        {
            result.error = "no id provided";
            result.status = "404";
            return result;
        }
        result = await this._put("tariff/edit/"+body.id, _body); 

        return result;
    }

    createTariff = async (body) => {
        const result = await this._post("/tariff/create", body);
        return result
    }

    createUser = async (body) => {
        const result = {successed: false};
        await  this.mainSiteAxios.post("/account/register", body)

            .then(response => {
                result.status = response.status; 
                result.data = response.data;
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


    // Car models
    createCarModel = async (body) => {
        const config = {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        }
        
        const result = await this._post("/car/model/create", body, config);
        return result
    }

    getCars = async () => {
        const result = await this._get("/car/models");
        return result;
    }


    // Users
    getUsers = async () => {
        const result = await this._get("/user/all");
        return result;
    }

    grantRole = async (id, role) => {
        const result = await this._post(`/user/${id}/GrantRole/${role}`)
        return result;
    }

    revokeRole = async (id, role) => {
        const result = await this._delete(`/user/${id}/RevokeRole/${role}`)
        return result;
    }    

    // Auth
    login = async (body) => {
        const result = await this._post("/auth/login", body);

        return result
    }

    become = async () => {
        const result = await this._get("/auth/become");

        return result
    }

    isAdmin = async () => {
        return await this._get('/auth/isAdmin');
    }

    logout = async () => {
        await this.axiosInstanceAuthorize.post("/Account/LogOut");
    }

    verify_profile = async (id) => {
        return await this._put(`/User/verify/${id}`)
    }

    giveMoney = async (id, value) => {
        return await this._post(`/User/${id}/BalanceIncrease`, value);
    }

    subtractMoney = async (id, value) => {
        return await this._post(`/User/${id}/BalanceDecrease`, value);
    }

    editUser = async (id, model) => {
        return await this._put(`/User/Edit/${id}`, JSON.stringify(model));
    }

    getUserInfo = async (id) => {
        return await this._get(`/User/${id}`);
    }

    async _post(endpoint, model, props) {
        const result = {successed: false};
        await this.axiosInstance.post(endpoint, model, props)

            .then(response => {
                result.status = response.status; 
                result.data = response.data;
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
        await this.axiosInstance.put(endpoint, model)
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

    async _delete(endpoint, model) {
        const result = {successed: false};
        await this.axiosInstance.delete(endpoint,JSON.stringify( model ))
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
        
        for (let key in keys){
            for (let elem in obj) {
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


