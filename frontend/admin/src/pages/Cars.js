import React, { useEffect, useState } from 'react';
import CarTable from '../components/CarsPage/CarTableManagement';
import '../styles/car-page.css';
import API from '../httpclient/axios_client';
import { TableSearchField } from '../components/TableCommon';

const attrs = [
    {
        value: 'Brand',
        label: 'Производитель',
        labelHelper: "Производителю"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    },
    {
        value: 'TariffId',
        label: 'Id тарифа',
        labelHelper: 'Id тарифа'
    }
];

function CarsMngmt() {
    const [carsData, setCarsData] = useState([]);
    
    let loadData = async () => {
        let result = await API.getCars();
        console.log(result);

        if (result.error !== null)
        setCarsData(result.data);
    }

    useEffect(() => { 
        loadData()
    }, []);

    return (
        <>
            <h1>
                Car Models
            </h1>
            <TableSearchField data={carsData} attrs={attrs} defaultAttrName="Brand" setData={setCarsData}/>
            <div className='commandsList'>
                <CarTable carsData={carsData} refreshRows={() => loadData()}/>
            </div>
        </>
    )
}

export default CarsMngmt;