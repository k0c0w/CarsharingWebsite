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


    const onStateUpdate = (id, state) => {
        const index = tariffsData.findIndex(x => x.id == id);
        if (index !== -1) {
            const data = [...tariffsData]
            data[index] = Object.assign({}, data[index], { is_active: state });
            setTariffsData(data)
        }
    } 

    const loadData = async () => {
        var result = await API.getTariffs()
    
        if (result.error !== null)
            setTariffsData(result.data);
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
                <TarrifTable tariffsData={tariffsData} refreshRows={() => loadData()} onUpdate={onStateUpdate}/>
            </div>
        </>
    )
}

export default TarrifMngmt;