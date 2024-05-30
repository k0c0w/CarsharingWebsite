import API from '../httpclient/axios_client';

const login = (model) => {
    API.axiosInstance.post(`account/login/`, model)
        .then((response) => console.log(response))
};

export default login;