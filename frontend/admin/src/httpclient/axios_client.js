import axios from "axios"

class AxiosWrapper {
    constructor(url = process.env.REACT_APP_ADMIN_API_URL) {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": process.env.REACT_LOCAL_HOST,
                "Access-Control-Allow-Credentials": "true",
                "X-Requested-With": "XMLHttpRequest"
            },
            withCredentials: true,
        };

        this.axiosInstance = axios.create(options);
        this.token = "";
        options.baseURL = process.env.REACT_APP_WEBSITE_API_URL
        this.mainSiteAxios = axios.create(options);
    }
    
    async getChatHistory(userId) {
        const history = await this.mainSiteAxios.get(`/chat/${userId}/history`);
        if (history.data)
            return history.data.sort(function (a, b) {
                return a.time.localeCompare(b.time);
            });
        return [];
    }

    async sendDocument(body) {
        const config = {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        }
        debugger;
        const result = await this._post("", body, config);
        return result
    }

    async getOnlineRooms() {
        const result =  await this.mainSiteAxios.get("/chat/rooms");
        return result.data;
    }

    //Posts
    async getPosts () {
        const result = await this._get("/post/posts");
        return result;
    }

    async getPostById(id) {
        const result = await this._get("/post/posts"+id);
        return result;
    }

    async deletePost(id) {
        const result = await this._delete(`/post/delete/${id}`);
        return result;
    }

    async updatePost(body) {
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
        result = await this._put("/post/edit/"+body.id, _body);

        return result;
    }

    async createPost(body) {
        const result = await this._post("/post/create", body);
        return result
    }

    // Tariffs
    async getTariffs() {
        const result = await this._get("/tariff/all");
        return result;
    }

    async getTariffById(id) {
        const result = await this._get("/tariff/"+id);
        return result;
    }

    async deleteTariff(id) {
        const result = await this._delete(`/tariff/delete/${id}`); 
        return result;
    }

    async changeTraiffState(id, state) {
        const response = await this._put(`/tariff/setstate/${id}`, state)
        return response
    }

    async updateTariff(body) {
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
        result = await this._put("/tariff/edit/"+body.id, _body); 

        return result;
    }

    async createTariff(body) {
        const result = await this._post("/tariff/create", body);
        return result
    }

    async createUser(body) {
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
    
    async createCarModel(body) {
        const config = {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        }
        
        const result = await this._post("/car/model/create", body, config);
        return result
    }

    async getCars() {
        const result = await this._get("/car/models");
        return result;
    }

    // CarPark
    async getCarPark() {
        const result = await this._get("/car/cars");
        return result;
    }

    // Users
    async getUsers() {
        const result = await this._get("/user/all");
        return result;
    }

    async grantRole(id, role) {
        const result = await this._post(`/user/${id}/GrantRole/${role}`)
        return result;
    }

    async revokeRole(id, role) {
        const result = await this._delete(`/user/${id}/RevokeRole/${role}`)
        return result;
    }    

    // Auth
    async login(body) {
        let response = await this._post("/auth/login", body);
        this.token = response?.data?.bearer_token;
        if (this.token !== "")
            this.axiosInstance.defaults.headers["Authorization"] = `Bearer ${this.token}`;
        return response
    }

    async become() {
        const result = await this._get("/auth/become");

        return result
    }

    async isAdmin() {
        return await this._get('/auth/isAdmin');
    }

    async logout() {
        await this.mainSiteAxios.post("/Account/LogOut");
    }

    async verify_profile(id) {
        return await this._put(`/User/verify/${id}`)
    }

    async giveMoney(id, value) {
        return await this._post(`/User/${id}/BalanceIncrease`, value);
    }

    async subtractMoney(id, value) {
        return await this._post(`/User/${id}/BalanceDecrease`, value);
    }

    async editUser(id, model) {
        return await this._put(`/User/Edit/${id}`, JSON.stringify(model));
    }

    async getUserInfo(id) {
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

    // Chat 
    async getOpenChats() {
        const result = await this._get("/user/getOpenChats");
        return result;
    } 

    async uploadDocument() {
        
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