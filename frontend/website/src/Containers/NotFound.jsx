import Container from "../Components/Container";
import { SectionTitle } from "../Components/Sections";
import Bold from "../Components/TextTags";
import DocumentTitle from "../DocumentTitle";

export default function NotFound() {
    return <>
        <DocumentTitle>404</DocumentTitle>
        <Container>
            <SectionTitle style={{position:"absolute", top:0, right:"50%", bottom: 0}}>Страница не найдена.</SectionTitle>
        </Container>
    </>
}