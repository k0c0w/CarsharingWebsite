import { YMaps, Map as YMap, Placemark, ZoomControl, GeolocationControl} from '@pbe/react-yandex-maps'
import { useState } from 'react';

const defaultPreset = {preset: 'islands#blueAutoIcon'};
const selectedPreset = {preset: 'islands#redAutoIcon'};

export default function MyMap({className, geo, cars, chooseCarFunc}) {
    const [prev, setPrev] = useState(null);
    const mapState = { center: [geo.latitude, geo.longitude], zoom:18};

    function handleClick(event) {
        const target = event.originalEvent.target;
        chooseCarFunc(target.options._options.id);
    }

    return <div className= {className}>
        <YMaps key={"6cf04ff9-9772-446d-b5e7-35cd65093895"}>
            <YMap modules={["geolocation", "geocode", "control.GeolocationControl"]} state={mapState} width="100%" height="100%">
                {cars.map((x, i) => {
                    return <Placemark key={i} onClick={handleClick} options={{preset: 'islands#blueAutoIcon', id:x.id}}
                     geometry={[parseFloat(x.latitude), parseFloat(x.longitude)]}>
                    </Placemark>
                })}
                <ZoomControl options={{ float: "left" }} />
                {/*<GeolocationControl  options={{ float: "left" }} />*/}
            </YMap>
        </YMaps>
    </div>
}