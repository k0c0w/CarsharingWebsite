import React, { useEffect, useState } from 'react';
import CarParkTable from '../components/CarParkPage/CarParkTableManagement';
import '../styles/car-page.css';
import API from '../httpclient/axios_client';
import { TableSearchField } from '../components/TableCommon';

const attrs = [
    {
        value: 'LicensePlate',
        label: 'Гос. Номер',
        labelHelper: "Гос. Номер"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    },
    {
        value: "IsOpen",
        label: "Блокировка",
        labelHelper: "Открыта или нет"
    },
    {
        value: "IsTaken",
        label: "Забронирована",
        labelHelper: "Используется ли машина"
    },
    {
        value: "ParkingLattitude",
        label: "Широта",
        labelHelper: "Координата"
    },
    {
        value: "ParkingLongitude",
        label: "Долгота",
        labelHelper: "Координата"
    },
    {
        value: 'CarModelId',
        label: 'Id модели',
        labelHelper: 'Id модели'
    }
];

function CarParkMngmt() {
    const [carParkData, setCarParkData] = useState([]);
    
    let loadData = async () => {
        let result = await API.getCarPark();
        console.log(result);

        if (result.error !== null)
        setCarParkData(result.data);
    }

    useEffect(() => { 
        loadData()
    }, []);


    return (
        <>
            <h1>
                Car Models
            </h1>
            <TableSearchField data={carParkData} attrs={attrs} defaultAttrName="LicensePlate" setData={setCarParkData}/>
            <div className='commandsList'>
                <CarParkTable carParkData={carParkData} refreshRows={() => loadData()}/>
            </div>
        </>
    )
}

export default CarParkMngmt;