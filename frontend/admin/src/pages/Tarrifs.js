import React, { useEffect, useState } from 'react';

import TarrifTable from '../components/TarrifsPage/TarrifTableManagement'
import {refreshData} from '../httpclient/axios_client'
import { TableSearchField } from '../components/TableCommon';



const attrs = [
    {
        value: 'Name',
        label: 'Имя',
        labelHelper: "Имени"
    },
    {
        value: 'Price',
        label: 'Цена (p.)',
        labelHelper: "Ценe"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    }
];



const tariffEndpoint = "tariff/tariffs";

function TarrifMngmt() {
    const [tariffsData, setTariffsData] = useState([]);
    
    useEffect(()=>{refreshData(setTariffsData, tariffEndpoint)}, []);

    return (
        <>
            <h1>
                Tarrifs Management
            </h1>
            <TableSearchField data={tariffsData} attrs={attrs} defaultAttrName="Price" setData={setTariffsData}/>
            <div className='commandsList'>
                <TarrifTable tariffsData={tariffsData} refreshRows={() => refreshData(setTariffsData, tariffEndpoint)}/>
            </div>
        </>
    )
}

export default TarrifMngmt;