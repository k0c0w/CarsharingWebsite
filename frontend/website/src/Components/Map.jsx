import { YMaps, Map as YMap, Placemark, ZoomControl, GeolocationControl} from '@pbe/react-yandex-maps'

export default function MyMap({className, geo, cars}) {
    const mapState = { center: [geo.latitude, geo.longitude], zoom:18};
    debugger;
    return <div className= {className}>
        <YMaps>
            <YMap state={mapState} width="100%" height="100%">
                {cars.map(x => {
                    <Placemark  geometry={[parseFloat(x.latitude), parseFloat(x.longitude)]}/>
                })}
                <Placemark geometry={[geo.latitude, geo.longitude]}/>
                <ZoomControl options={{ float: "left" }} />
                <GeolocationControl options={{ float: "left" }} />
            </YMap>
        </YMaps>
    </div>
}