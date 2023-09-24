import settings from '../appsettings.json';

export const getTarrifs = async () => {
    return await fetch(`http://${settings.appUrl}/api/tariff/tariffs`, {
        method: "get",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        }
    })
    .then((response) => response.json());
}