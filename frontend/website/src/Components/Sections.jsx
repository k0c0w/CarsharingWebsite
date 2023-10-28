import React, { useEffect, useRef } from "react";
import Container from "./Container";
import "../css/index.css"

export const Section = React.forwardRef((props, ref) => 
    (<section className={`frame ${props.className ? props.className : ""}`} style={props?.style} ref={ref} id={props?.id} onClick={props?.onClick}>{props.children}</section>));

Section.displayName = "Section"
export function GreetingSection({style, backgroundImageClass, title, subtitle, children}){
    const section = useRef(null);
    useEffect(() => { section?.current.classList.add("loaded")}, []);
    const className = backgroundImageClass ? `flex-container greeting-screen greeting-background ${backgroundImageClass}` 
                    : "flex-container greeting-screen greeting-background"; 
    return <Section ref={section}>

      <div className={className} style={style}>
      <Container>
            <div className="flex-container banner">
              <h1 className="title"><span>{title}</span></h1>
              <h2 className="subtitle">{subtitle}</h2>
            </div>
            <div>{children}</div>
    </Container>
        
      </div>
</Section>
}

export const SectionTitle = React.forwardRef((props, ref) => (
  <div ref={ref} className="flex-container section-title" style={props.style}>
      <h2>{props.children}</h2>
      {props.subtitle && <h4>{props.subtitle}</h4>}
  </div>
));
SectionTitle.displayName = "SectionTitle"
export default Section;