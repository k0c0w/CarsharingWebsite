import React from 'react';
import AddCar from '../components/CarsPage/AddCar';
import RemvoeCar from '../components/CarsPage/RemoveUpdateCar';

import '../styles/car-page.css';

function CarsMngmt() {
    return (
        <>
            <h1>
                Car Management
            </h1>
            <div className='commandsList'>
                <AddCar></AddCar>
                <RemvoeCar></RemvoeCar>
            </div>
        </>
    )
}

export default CarsMngmt;