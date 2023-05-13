import {Section, SectionTitle} from "../Components/Sections";
import DocumentTitle from "../DocumentTitle";
import Container from "../Components/Container";
import {Figure} from "../Components/Figure";
import { Dim } from "../Components/TextTags";
import Line from "../Components/Line";
import HorizontalArrow from "../Components/HorizontalArrow";
import "../css/rectangle-link.css";
import "../css/documents.css";
import "../css/common.css";
import { useEffect, useState } from "react";
import { getDataFromEndpoint } from "../httpclient/axios_client";
import "../css/carousel.css";

const NewsCard = (props) => (
    <Figure figureName="news">
        <div>
            <h3 style={{display:"inline-block"}}>Новый тариф доступен!</h3>
            <div>12 января</div>
        </div>
        <Line style={{width:"inherit"}}/>
        <Dim className="truncate-text">
        существенных финансовых и административных условий. Не следует, однако забывать, что консультация с широким активом обеспечивает широкому кругу (специалистов) участие в формировании позиций, занимаемых участниками в отношении поставленных задач.gmfdpomjfdpkgmdflogdfoigdofijgdfoijgodifjgodfijgdofijgdijfgji
        </Dim>
    </Figure>
);

const news = [<NewsCard/>, <NewsCard/>, <NewsCard/>, <NewsCard/>, <NewsCard/>, <NewsCard/>, <NewsCard/>];



function Carousel({data}) {
    const step = 3;
    const [currentIndex, setCurrentIndex] = useState(0);
    console.log(currentIndex);
    const [length, setLength] = useState(data.length);
    const [slice, setSlice] = useState(data?.slice(currentIndex, currentIndex + step));
    useEffect(() => {
        setLength(data.length)
    }, [data]);
    useEffect(() => {
        setSlice(data?.slice(currentIndex, currentIndex + step));
    }, [currentIndex]);

    const next = () => {
        if (currentIndex < (length - step)) {
            setCurrentIndex(currentIndex + step);console.log(currentIndex);
        }
    }
    
    const prev = () => {
        if (currentIndex >= step) {
            setCurrentIndex(currentIndex - step);
            console.log(currentIndex);
        }
    }

    return (
        <div className="carousel-container">
            <div className="carousel-wrapper">
            { currentIndex >= step && <button onClick={prev} className="left-arrow">&lt;</button>}
                <div className="carousel-content-wrapper">
                    <div className="carousel-content">
                        {slice.map(x => 
                            <NewsCard/>)}
                    </div>
                </div>
            { currentIndex < (length - step) && <button onClick={next} className="right-arrow">&gt;</button>}
            </div>
        </div>);
}





const News = () => (
    <Section id="news" className="news-screen greeting-background news-background news-mobile">
        <Container>
           <div className="flex-container">
            <Carousel data = {news}/>
           </div>
        </Container>
    </Section>
)

const Document = ({documentInfo}) => (
    <a href={documentInfo.url} target="_blank" className="flex-container rectangle-container-item_link">
        <div className="flex-container" style={{overflow:"hidden"}}>
            <h3 className="rectangle-container-item_head">{documentInfo.documentName}</h3>
        </div>
        <HorizontalArrow/>
    </a>
);

export function Documents (props) {
    const [documentLinks, setDocumentLinks] = useState([]);
    useState(() => {getDataFromEndpoint("documents", setDocumentLinks)}, []);

    return <>
    <DocumentTitle>Документы</DocumentTitle>
    {/*<News/>*/}
    <News/>
    <Section style={{backgroundColor: "#DEF0F0"}}>
        <Container>
            <SectionTitle>Документы</SectionTitle>
            <div className="rectangle-container">
                <ul className="tariff-holder">
                    {documentLinks.map((info, i) => <li className="rectangle-container-item" key={i}><Document documentInfo={info}/></li>)}
                </ul>
            </div>
        </Container>
    </Section>
    </>;
}