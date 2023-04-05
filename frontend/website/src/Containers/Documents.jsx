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
import { useState } from "react";
import { getDataFromEndpoint } from "../httpclient/axios_client";


const NewsCard = (props) => (
    <Figure figureName="news">
        <div>
            <h3>Новый тариф доступен!</h3>
        </div>
        <Line style={{width:"inherit"}}/>
        <Dim className="truncate-text">
        существенных финансовых и административных условий. Не следует, однако забывать, что консультация с широким активом обеспечивает широкому кругу (специалистов) участие в формировании позиций, занимаемых участниками в отношении поставленных задач.gmfdpomjfdpkgmdflogdfoigdofijgdfoijgodifjgodfijgdofijgdijfgji
        </Dim>

    </Figure>
);

const News = () => (
    <Section id="news" className="news-screen greeting-background news-background news-mobile">
        <Container>
            <div className="flex-container">
            <NewsCard/>
            <NewsCard/>
            <NewsCard/>
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