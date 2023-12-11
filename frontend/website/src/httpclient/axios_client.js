import axios from "axios"

function delay(ms) {
    return new Promise((resolve) => {
      setTimeout(resolve, ms);
    });
  }

class AxiosWrapper {

    constructor(url = "https://localhost:7129/api") {
        const options = {
            baseURL: url,
            timeout: 10000,
            ssl: false,
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=UTF-8',
                "Access-Control-Allow-Origin": "https://localhost:7129",
                "X-Requested-With": "XMLHttpRequest"
            },
            withCredentials: true,
        }
        this.token = localStorage.getItem("token");
        this.axiosInstance = axios.create(options);
        this.axiosInstance.defaults.headers["Authorization"] = `Bearer ${this.token}`;

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

        this.s3ServiceAxios = axios.create(s3options);
        this.s3ServiceAxios.defaults.headers.Authorization = `Bearer ${this.token}`;
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

    async getChatOccasions() {
        try{
            const history = await this.axiosInstance.get(`/chat/appeals`);
            return history.data
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
        const result = {successed: false}
        await axios.get('/documents', {
            baseURL: process.env.REACT_APP_S3_API_URL,
          }) 
            .then(response => {
                result.successed = true;
                result.documents = response.data.value.map(x => {return {annotation: x.annotation, download_url: `http://localhost:7126/${x.download_url}`}});

                return result;
            })
            .catch(() => {
            });

        return result;
    }

    async news() {
        return await this._get(`/information/news`);
    }

    async login(form) {
        let response = await this._post(`/account/login/`, this._getModelFromForm(form));
        this.token = response?.data?.bearer_token ?? "";
        this.axiosInstance.defaults.headers["Authorization"] = `Bearer ${this.token}`;
        this.s3ServiceAxios.defaults.headers.Authorization = `Bearer ${this.token}`;
        localStorage.setItem("token", this.token)
        return response
    }

    async logout() {
        return await this._post(`/account/logout/`);
    }

    async register(form) {
        let response = await this._post('/account/register', this._getModelFromForm(form));
        this.token = response?.data?.bearer_token;
        if (this.token !== "")
            this.axiosInstance.defaults.headers["Authorization"] = `Bearer ${this.token}`;
        return response;
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

    async loadOccasionHistory(occasionId) {
        const loadHistoryResult = {successed: false, messages: []}
        await this.axiosInstance.get(`/occasions/${occasionId}/chat`)
            .then(response => {
                loadHistoryResult.successed = true;
                loadHistoryResult.messages = response.data;
            });
        
        return loadHistoryResult;
    }

    async loadMyOccasion() {
        const occasionInfo = {successed: false, openedOccasionId: null};
        await this.axiosInstance.get("/occasions/my")
        .then(resp => {
            occasionInfo.openedOccasionId = resp.data;
            occasionInfo.successed = true;
        })
        .catch(err => {
            if (err.response){
                const status = err.response.status;
                if (status == 404)
                    occasionInfo.successed = true;
                else
                    alert(err.response.data.errorMessage);
            }
        });

        return occasionInfo;
    }

    async openNewOccasion(occasionType, topic) {
        const occasionCreationInfo = {successed: false, createdOccasionId: null, errorMessage: null};
        await this.axiosInstance.post("/occasions", { event: occasionType, topic: topic })
            .then(response => {
                occasionCreationInfo.successed = true;
                occasionCreationInfo.createdOccasionId = response.data;
            })
            .catch(err => {
                if (err.response) {
                    const response = err.response;
                    if (response.status === 400 && response.data)
                        occasionCreationInfo.errorMessage = response.data;
                }
            });

        return occasionCreationInfo;
    }

    async getOccasionTypes() {
        let occasionTypes = [];
        await this.axiosInstance.get("/occasions/types")
        .then(response => occasionTypes = response.data)
        .catch(() => {});

        return occasionTypes;
    }

        //attachments must be array of files from form multiple data
    async addAttachment(attachments){
        let attachmentCreationTrackingId = null;
        const attachmentCreationResult = { successed: false, attachmentId: null }
        const formData = new FormData();
        attachments.forEach(element => formData.append("file", element, element.name));
        await this.s3ServiceAxios.post("/attachments", formData, {
            headers: { "Content-Type": "multipart/form-data" }
        })
        .then(response =>{
            attachmentCreationTrackingId = response.data;
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

    async _post(endpoint, model) {
        const result = {successed: false};
        await this.axiosInstance.post(endpoint, model)
            .then(response => {
                result.status = response.status; 
                result.successed = true;
                result.data = response.data;
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