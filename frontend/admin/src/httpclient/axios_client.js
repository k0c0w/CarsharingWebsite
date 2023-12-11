import axios from "axios"

function delay(ms) {
    return new Promise((resolve) => {
      setTimeout(resolve, ms);
    });
  }

class AxiosWrapper {
    constructor(url = process.env.REACT_APP_ADMIN_API_URL) {
        const options = {
            baseURL: "https://localhost:7129/api/admin",
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
        this.token = localStorage.getItem("token");

        this.axiosInstance.defaults.headers["Authorization"] = `Bearer ${this.token}`;
        options.baseURL = process.env.REACT_APP_WEBSITE_API_URL

        this.mainSiteAxios = axios.create(options);
        this.mainSiteAxios.defaults.headers["Authorization"] = `Bearer ${this.token}`;

        const s3options = {
            baseURL: "http://localhost:5147",
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                "Access-Control-Allow-Origin": "http://localhost:5147",
                "Access-Control-Allow-Credentials": "true",
                "X-Requested-With": "XMLHttpRequest"
            },
            withCredentials: true,
        };
        this.s3ServiceAxios = axios.create(s3options)
        this.s3ServiceAxios.defaults.headers["Authorization"] = `Bearer ${this.token}`;
    }
    
    async getChatHistory(userId) {
        const history = await this.mainSiteAxios.get(`/chat/${userId}/history`);
        if (history.data)
            return history.data.sort(function (a, b) {
                return a.time.localeCompare(b.time);
            });
        return [];
    }

    async sendDocument(form) {
        const config = {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        };
        const requestBody = {
            Annotation: form.annotation,
            IsPrivate: form.isPublic == true,
            document: form.Document
        };

        let attachmentCreationTrackingId = null;
        const documentCreationResult = { successed: false, attachmentId: null }

        await this.s3ServiceAxios.post("/documents", requestBody, config)
        .then(response =>{
            attachmentCreationTrackingId = response.data;
        })
        .catch(err => {});

        if (attachmentCreationTrackingId)
        {
            let attempt = 0;
            let attemptResult = false;
            while (attempt < 5) {
                await this.axiosInstance.get(`/operation/${attachmentCreationTrackingId}/status`)
                    .then(response => {
                        const status = response.data;
                        if (status == "Failed"){
                            attempt = 1000;
                        }
                        else if (status == "Completed"){
                            attemptResult = true;
                        }
                    })
                    .catch(err => {
                        if (err.response && (err.response.status === 401 || err.response.status === 404))
                            attempt = 1000;
                    });
                if (attemptResult){
                    documentCreationResult.attachmentId = attachmentCreationTrackingId;
                    documentCreationResult.successed = true;
                    break;
                }
                else{
                    ++attempt;
                    await delay(5000);
                }
            }
        }

        return documentCreationResult;
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
        debugger;
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
        {

            this.axiosInstance.defaults.headers["Authorization"] = `Bearer ${this.token}`;
            this.s3ServiceAxios.defaults.headers.Authorization = `Bearer ${this.token}`;
            this.mainSiteAxios.defaults.headers["Authorization"] = `Bearer ${this.token}`;
        }
        localStorage.setItem("token", this.token)
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

    async getAllDocuments() {
        const response = {
            successed: false,
            data: []
        };

        await this.s3ServiceAxios.get('/documents')
            .then(x => {
                response.successed = true;
                response.data = x.data.value;
            })
            .catch(error => {
                if (error.response){
                    response.error = error.response.errorMessage;
                }
                else
                    alert("Ошибка при обработке запроса. Проверьте подключение к интернету и попробуйте снова.");
            });

        return response;
    }

    async makeDocumentPublic(guid, state) {
        const response = {
            successed: false
        };

        await this.s3ServiceAxios.patch(`/documents/${guid}`, JSON.stringify({isPublic: state}))
            .then(x => {
                response.successed = true;
            })
            .catch(error => {
                if (error.response){
                    response.error = error.response.errorMessage;
                }
                else
                    alert("Ошибка при обработке запроса. Проверьте подключение к интернету и попробуйте снова.");
            });

        return response;
    }

    async deleteDocument(guid) {
        const response = {
            successed: false
        };

        await this.s3ServiceAxios.delete(`/documents/${guid}`)
            .then(x => {
                response.successed = true;
            })
            .catch(error => {
                if (error.response){
                    response.error = error.response.errorMessage;
                }
                else
                    alert("Ошибка при обработке запроса. Проверьте подключение к интернету и попробуйте снова.");
            });

        return response;
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

    async completeOccasion(occasionGuid) {
        const occasionCompletionResult = { successed: false, errorMessage: null}
        await this.axiosInstance.post(`/occasions/${occasionGuid}/complete`)
            .then(response => {
                occasionCompletionResult.successed = true;
            })
            .catch(err => {
                if (err.response)
                    occasionCompletionResult.errorMessage = err.response.errorMessage;
            })
        
        return occasionCompletionResult;
    }

    async getUncompletedOccasions() {
        const uncompletedOccasionResult = {successed:false, occasions: [], errorMessage: null}

        await this.axiosInstance.get("/occasions/uncompleted")
            .then(response => {
                uncompletedOccasionResult.successed=true;
                // [ { Id, OccasionType, Topic }
                uncompletedOccasionResult.occasions=response.data;
            })
            .catch(err => {
                if (err.response){
                    uncompletedOccasionResult.errorMessage = response.errorMessage;
                }
            });

        return uncompletedOccasionResult;
    }

    async getOccasionChatHistory(occasionGuid) {
        const historyResult = {successed: false, messages: [], errorMessage: null}

        await this.mainSiteAxios.get($`/occassions/${occasionGuid}/chat`)
            .then(response => {
                historyResult.successed = true;
                historyResult.messages = response.data;
            })
            .catch(err => {
                if(err.response)
                    historyResult.errorMessage = err.response.data;
            });

        return historyResult;
    }

    async getOccasionById(occasionId) {
        const historyResult = {successed: false, occasion: [], errorMessage: null};

        await this.axiosInstance.get(`/occasions/${occasionId}`)
            .then(response => {
                historyResult.successed = true;
                historyResult.occasion = response.data;
            })
            .catch(err => {
                if(err.response)
                    historyResult.errorMessage = err.response.data;
            });

        return historyResult;
    }

    async loadOccasionHistory(occasionId) {
        const loadHistoryResult = {successed: false, messages: []}
        await this.axiosInstance.get(`/occasions/${occasionId}/chat`)
            .then(response => {
                loadHistoryResult.successed = true;
                loadHistoryResult.messages = response.data;
            });
        
        return loadHistoryResult;
    }

    //attachments must be array of files from form multiple data
    async addAttachment(occasionIssuerId, attachments){
        let attachmentCreationTrackingId = null;
        const attachmentCreationResult = { successed: false, attachmentId: null }
        const formData = new FormData();
        attachments.forEach(element => formData.append("files", element, element.name));
        formData.append("occasionUserId", occasionIssuerId);

        await this.s3ServiceAxios.post("/admin/attachments", formData, {
            headers: { "Content-Type": "multipart/form-data" }
        })
        .then(response =>{
            attachmentCreationTrackingId = response.data.value;
        })
        .catch((err) => {
            if (err.response)
                attachmentCreationResult.errorMessage = err.response.data.errorMessage;
        });

        if (attachmentCreationTrackingId)
        {
            let attempt = 0;
            let attemptResult = false;
            while (attempt < 5) {
                await this.s3ServiceAxios.get(`/operation/${attachmentCreationTrackingId}/status`)
                    .then(response => {
                        const status = response.data.status;
                        if (status == "Failed"){
                            attempt = 1000;
                        }
                        else if (status == "Completed"){
                            attemptResult = true;
                        }
                    })
                    .catch(err => {
                        if (err.response && (err.response.status === 401 || err.response.status === 404))
                            attempt = 1000;
                    });
                if (attemptResult){
                    attachmentCreationResult.attachmentId = attachmentCreationTrackingId;
                    attachmentCreationResult.successed = true;
                    break;
                }
                else{
                    ++attempt;
                    await delay(5000);
                }
            }
        }

        return attachmentCreationResult;
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