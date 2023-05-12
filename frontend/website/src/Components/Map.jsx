import { YMaps, Map as YMap, Placemark, ZoomControl, GeolocationControl} from '@pbe/react-yandex-maps'
const mapState = { center: [55.789363, 49.124080], zoom:18};
export default function MyMap(props) {
    return <div className= {props.className}>
        <YMaps>
            <YMap state={mapState} width="100%" height="100%">
                <Placemark  geometry={[55.789363, 49.124080]}>
                <div onClick={()=> console.log("asdsada")}>
                </div>

                </Placemark>

                <Placemark geometry={[55.789287, 49.124038]}/>
                <Placemark geometry={[55.789668, 49.125013]}/>
                <ZoomControl options={{ float: "left" }} />
                <GeolocationControl options={{ float: "left" }} />
            </YMap>
        </YMaps>
    </div>
}