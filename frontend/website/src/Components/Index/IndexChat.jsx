import React from "react";
import Section, {SectionTitle} from "../Sections";
import Container from "../Container";
import Figure from "../Figure";
import Chat from "../../Containers/Chat";

export const IndexChat = React.forwardRef( (props, ref) => (
    <Section ref={ref} style={{backgroundColor:"#DEF0F0"}}>
        <Container>
            <SectionTitle style={{justifyContent:"center"}}>Остались вопросы?</SectionTitle>
            <Chat/>
        </Container>
    </Section>
));
export default IndexChat;