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
        label: 'Цена (p./мин)',
        labelHelper: "Ценe"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    }
];


const findIndexById = (array, id) => array.findIndex(x => x.id == id);

function TarrifMngmt() {
    const [tariffsData, setTariffsData] = useState([]);

    const onStateUpdate = (id, state) => {
        const index = findIndexById(tariffsData, id);
        if (index !== -1) {
            const data = [...tariffsData]
            data[index] = Object.assign({}, data[index], { is_active: state });
            setTariffsData(data)
        }
    } 

    const onDelete = (id) => {
        const index = findIndexById(tariffsData, id);
        if (index !== -1) {
            const data = [...tariffsData]
            data.splice(index, 1);
            setTariffsData(data)
        }
    }

    const loadData = async () => {
        let result = await API.getTariffs()
    
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
                <TarrifTable tariffsData={tariffsData} refreshRows={() => loadData()} onDelete={onDelete} onUpdate={onStateUpdate}/>
            </div>
        </>
    )
}

export default TarrifMngmt;