export var getTarrifs = async () => {
    let settings = require('../appsettings.json');
    var result = null;
    fetch(`http://${settings.appUrl}/api/tariff/tariffs`, {
        method: "get",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    })
        .then((response) => { result = response.json() })
    return await result;
}