import React, { useEffect, useState } from 'react';
import TarrifTable from '../components/TarrifsPage/TarrifTableManagement'
import API from '../httpclient/axios_client'
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



function TarrifMngmt() {
    const [tariffsData, setTariffsData] = useState([]);

    var loadData = async () => {
        var result = await API.getTariffs()

        console.log(result);
    
        if (result.error !== null)
            setTariffsData(result.data);
        

        console.log(tariffsData);
        debugger
    }

    useEffect( () => {
        loadData()
    } , []);

    return (
        <>
            <h1>
                Tarrifs Management
            </h1>
            <TableSearchField data={tariffsData} attrs={attrs} defaultAttrName="Price" setData={setTariffsData}/>
            <div className='commandsList'>
                <TarrifTable tariffsData={tariffsData} refreshRows={() => loadData()}/>
            </div>
        </>
    )
}

export default TarrifMngmt;