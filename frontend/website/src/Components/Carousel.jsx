import React, { useEffect, useState } from 'react';
import '../css/carousel.css';

export default function Carousel(props) {
    const {children} = props;
    const step = 3;
    const [currentIndex, setCurrentIndex] = useState(0);
    const [length, setLength] = useState(children.length);
    const [slice, setSlice] = useState(children?.slice(currentIndex, currentIndex + step));
    useEffect(() => {
        setLength(children.length)
    }, [children]);
    useEffect(() => {
        setSlice(children?.slice(currentIndex, currentIndex + step));
    }, [currentIndex]);

    const next = () => {
        if (currentIndex < (length - 1)) {
            setCurrentIndex(prevState => prevState + step)
        }
    }
    
    const prev = () => {
        if (currentIndex > 0) {
            setCurrentIndex(prevState => prevState - step)
        }
    }

    return (
        <div className="carousel-container">
            <div className="carousel-wrapper">
            { currentIndex > 0 && <button onClick={prev} className="left-arrow">&lt;</button>}
                <button onClick={prev} className="left-arrow">&lt;</button>
                <div className="carousel-content-wrapper">
                    <div className="carousel-content">
                        {slice}
                    </div>
                </div>
            { currentIndex < (length - 1) && <button onClick={next} className="right-arrow">&gt;</button>}
            </div>
        </div>);
}