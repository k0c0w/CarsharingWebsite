import axios, {isCancel, AxiosError} from 'axios';

var postCreateCar = (url, model) => {
    axios.post(`${url}/api/tariff/create`, model)
        .then((response) => console.log(response))
};