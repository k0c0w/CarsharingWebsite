import API from "../httpclient/axios_client";

export function sendForm(form, endpointUrl, additionalData) {
    const finalFormEndpoint = endpointUrl || form.action;
    const data = Array.from(form.elements)
        .filter((element) => element.name)
        .reduce(
          (obj, input) => Object.assign(obj, { [input.name]: input.value }),
          {}
        );

    if (additionalData) {
        Object.assign(data, additionalData);
    }
    console.log(data);
    return new API().axiosInstance.post(finalFormEndpoint, data)
}