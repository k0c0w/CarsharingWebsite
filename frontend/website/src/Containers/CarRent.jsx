import Section from "../Components/Sections";
import { Dim } from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import MyMap from "../Components/Map";
import "../css/car-rent.css";
import { useEffect, useState } from "react";
import axiosInstance, { getDataFromEndpoint } from "../httpclient/axios_client";
import { NavLink, useNavigate, useNavigation, useParams } from "react-router-dom";



export default function CarRent() {    
    const {modelId} = useParams();
    const [modelInfo, setModelInfo] = useState({});
    const [geo, setGeo] = useState({latitude: 55.793987, longitude: 49.120208}) 
    const [radius, setRadius] = useState(10000);
    const [carList, setCarList] = useState([]);
    const navigate = useNavigate();
    

    function success(position) {
        const latitude = position.coords.latitude;
        const longitude = position.coords.longitude;
        setGeo({latitude: latitude, longitude: longitude});
      }
    
      function error() {
        alert("Невозможно получить ваше местоположение.");
        navigate("/");
      }

    
    function getCarList() {
        axiosInstance.get('cars/available', {params: {
            CarModelId: modelId,
            Longitude: geo.longitude,
            Latitude: geo.latitude,
            Radius: radius
        }})
        .then(x => setCarList(x.data))
        .catch(err => alert("Невозможно получить список машин."))
    }  


    useEffect(() => {
        axiosInstance.get(`cars/model/${modelId}`)
        .then(x => setModelInfo(x.data))
        .then(() => {
            if (!navigator.geolocation) {
                alert("Geolocation is not supported by your browser");
                navigate("/");
              } else {
                navigator.geolocation.getCurrentPosition(success, error);
                getCarList();
              }
        })
        .catch(err => { navigate('/notFound')});
    }, []);

    useEffect(() => {getCarList()}, [radius]);


    return <>
    <DocumentTitle>{modelInfo?.model}</DocumentTitle>
    <div style={{height:"96px"}}></div>
    <Section className="renting">
        <div className="renting-car-info flex-container">
            <div><span style={{marginLeft:0}}>
                <NavLink  className="renting-car-info__tariff" to={`/tariffs/${modelInfo?.tariff_id}`}>{modelInfo?.tariff_name}</NavLink></span></div>
            <div className="renting-car-info__img"><img src={modelInfo?.image_url} alt={modelInfo?.model} /></div>
            <div style={{width:"100%", textAlign: "center", padding: "0 15px"}}>
                <div>
                <h1 className="renting-car-info__car">{modelInfo?.brand} {modelInfo?.model}</h1>
                <div className="renting-car-info__car-tarrif"><span>{modelInfo?.price} р./мес.</span><span>{modelInfo?.max_milage && <>{modelInfo?.max_milage} км/день</>}</span></div>
                <Dim style={{textAlign: "left", paddingTop:"20px"}}>
                    {modelInfo?.description}
                </Dim>
                </div>
            </div>
        </div>
        <div className="renting-sidebar">
            <MyMap geo={geo} cars={carList} className="renting-sidebar-map"/>
            <div className="renting-sidebar-period">
                <div className="flex-container flex-column">
                    <div className="renting-sidebar-period__holder">
                        <span>Преиод:</span>
                        <span>с</span>
                        <input type="date"/>
                        <span>по</span>
                        <input type="date"/>
                    </div>
                    <div className="renting-sidebar-period__error">Неверная дата!</div>
                    <Dim>Расчетная стоимость: 10000р</Dim>
                    <button className="button">Аренда</button>
                </div>
            </div>
        </div>
    </Section>
    </>;
}