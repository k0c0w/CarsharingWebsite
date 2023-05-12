import axios from "axios"

export const axiosInstance = axios.create({
    baseURL: 'https://localhost:7129/api',
    timeout: 10000,
    ssl: false,
    headers: {
        'Accept': 'application/json',
        'Content-type': 'application/json; charset=UTF-8',
      }
});

export function getDataFromEndpoint(endpoint, dataSetterFunction) {
    axiosInstance.get(endpoint)
        .then(r => {dataSetterFunction(r.data)})
        .catch(err => console.log(`Error while recieving data from ${endpoint}`));
}

export default axiosInstance;
