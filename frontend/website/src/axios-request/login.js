import {axiosInstance} from '../httpclient/axios_client';

const login = (model) => {
    axiosInstance.post(`account/login/`, model)
        .then((response) => console.log(response))
};

export default login;