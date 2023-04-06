import axios from "axios";

export const axiosInstance = axios.create(
    {
        baseURL: 'https://localhost:7129/api',
        timeout: 10000,
        ssl: false,
        headers: {
            'Accept': 'application/json',
            'Content-type': 'application/json; charset=UTF-8',
          }
    }
);


export const refreshData = (newStateSetter, endpoint, searchQuery='') => {
    axiosInstance.get(`${endpoint}?${searchQuery}`)
        .then(r => { newStateSetter(r.data)})
        .catch(err => { alert(`Error occured while recieving from '${endpoint}'.`); console.log(err)});
};

export default axiosInstance;
