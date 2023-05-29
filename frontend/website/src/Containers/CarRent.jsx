import Section from "../Components/Sections";
import { Dim } from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";
import MyMap from "../Components/Map";
import "../css/car-rent.css";
import { useEffect, useState } from "react";
import API from "../httpclient/axios_client";
import { NavLink, useNavigate, useParams } from "react-router-dom";

function days(startDate, endDate) {
    if(startDate && endDate){
        const diffTime = Math.abs(endDate - startDate);
        return Math.ceil(diffTime / (1000 * 60 * 60 * 24)); 
    }
    return 0;
}


async function sendReuest(startDate, endDate, carId, block) {
    block(true);
    const response = await API.book({
            car_id: carId,
            start_date: startDate.toJSON(),
            end_date: endDate.toJSON()
        })
    block(false);
    if(response && response.successed){
        alert("Успешно забронирована.")
    }
    else if (response) {
        if(response.status === 401)
            document.location.href = `/login?return_uri=${document.location.href}`;
        else if(response.status === 400)
            alert("Машина не может быть забронирована. Недоступна или недостаточно средств.");
    }
    else{
        alert("Ошибка");
    }
}


export default function CarRent() {
    const {modelId} = useParams();
    const [modelInfo, setModelInfo] = useState({});
    const [geo, setGeo] = useState({latitude: 55.793987, longitude: 49.120208}) 
    const [carList, setCarList] = useState([]);
    const [block, setBlock] = useState(false);
    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(null);
    const [chose, setChose] = useState(null);
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

    
    async function getCarList() {
        const response = await API.available_cars({
            CarModelId: modelId,
            Longitude: parseFloat(geo.longitude),
            Latitude: parseFloat(geo.latitude),
            Radius: 8192
        });
        if(response.successed){
            setCarList(response.data);
        }
        else{
            alert("Невозможно получить список машин.");
        }
    }  

    useEffect(() => {

        async function fetchData(){
            const response = await API.car_description(modelId);
            if(response.successed){
                setModelInfo(response.data);
                if(!navigator.geolocation){
                    alert("Geolocation is not supported by your browser");
                    navigate("/");
                }
                else{
                    navigator.geolocation.getCurrentPosition(success, error);
                    getCarList();
                    console.log(modelInfo)
                }
            }
            else if(response.status === 404)
                navigate("/notFound");
        }
        fetchData();
    }, []);

    function set(id){
        const obj = carList.find(x => x.id == id)
        if(obj){
            setChose(obj)
        }
    }

    function rentCar(){

        if(startDate && endDate && startDate <= endDate && chose){
            sendReuest(startDate, endDate, chose.id, setBlock);
        }
        else{
            alert("Не все поля заполнены. Убедитесь, что выбрали машину.");
        }
        return false;
    }


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
            <MyMap geo={geo} cars={carList} chooseCarFunc={set} className="renting-sidebar-map"/>
            <div className="renting-sidebar-period">
                <div className="flex-container flex-column">
                    <div className="renting-sidebar-period__holder">
                        <span>Преиод:</span>
                        <span>с</span>
                        <input type="data" disabled value={new Date()}/>
                        {/*todo: <input type="date" onChange={(e) => {setStartDate(new Date(e.target.value + 'T00:00'))}}/>*/}
                        <span>по</span>
                        <input type="date" onChange={(e) => {setEndDate(new Date(e.target.value + 'T00:00'))}}/>
                    </div>
                    <div className="renting-sidebar-period__error">{startDate > endDate && <>Неверная дата!</>}</div>
                    {chose && <Dim>Выбранная машина: {chose?.license_plate}</Dim>}
                    { startDate != null && endDate != null && startDate <= endDate && modelInfo?.price 
                    && <Dim>Расчетная стоимость: {(days(startDate, endDate) * modelInfo?.price)?.toFixed(2)} р</Dim>}
                    {!block && <button className="button" onClick={rentCar}>Аренда</button>}
                </div>
            </div>
        </div>
    </Section>
    </>
}