import React, { useEffect, useState } from 'react';
import {PostTable} from "../components/PostsPage/PostTableManagment";
import API from '../httpclient/axios_client'
import { TableSearchField } from '../components/TableCommon';



const attrs = [
    {
        value: 'Title',
        label: 'Заголовок',
        labelHelper: "Заголовку"
    },
    {
        value: 'CreatedAt',
        label: 'Время создания',
        labelHelper: "Времени создания"
    },
    {
        value: 'Id',
        label: 'Id объекта',
        labelHelper: 'Id объекта'
    }
];


const findIndexById = (array, id) => array.findIndex(x => x.id == id);

export function PostMngmt() {
    const [postsData, setPostsData] = useState([]);

    const onDelete = (id) => {
        const index = findIndexById(postsData, id);
        if (index !== -1) {
            const data = [...postsData]
            data.splice(index, 1);
            setPostsData(data)
        }
    }

    const loadData = async () => {
        var result = await API.getPosts()
        if (result.error !== null)
            setPostsData(result.data);
    }

    useEffect( () => {
        loadData()
    } , []);

    return (
        <>
            <h1>
                Posts Management
            </h1>
            <TableSearchField data={postsData} attrs={attrs} defaultAttrName="Id" setData={setPostsData}/>
            <div className='commandsList'>
                <PostTable postData={postsData} refreshRows={() => loadData()} onDelete={onDelete}/>
            </div>
        </>
    )
}