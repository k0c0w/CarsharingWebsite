import React from "react";
import Section, {SectionTitle} from "../Sections";
import Container from "../Container";
import Bold from "../TextTags";

const IndexChat = React.forwardRef( (props, ref) => (
    <Section ref={ref} style={{backgroundColor:"#DEF0F0"}}>
        <Container>
            <SectionTitle style={{justifyContent:"center"}}>Остались вопросы?</SectionTitle>
            <Bold>Задайте их в наешм онлайн чате!</Bold>
        </Container>
    </Section>
));
IndexChat.displayName="Chat"
export default IndexChat;