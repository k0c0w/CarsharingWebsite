import Figure from "../Figure";
import Section, {SectionTitle} from "../Sections";

import "../../css/index.css";
import Bold, { Dim } from "../TextTags";
import Container from "../Container";
import Line from "../Line";
import Restrictions from "./Restrictions";
import "../../css/common.css";


const SideContent = () => (
    <div className="flex-container flex-column side-content">
        <div className="flex-container flex-column" style={{transform: "translateX(-5vw)"}}>
            <Figure figureName="circle" style={{backgroundColor:"#ffff"}} className="indexAbout-sidecontent_top">
                <svg width="105" height="108" viewBox="0 0 105 108" fill="none" xmlns="http://www.w3.org/2000/  svg"xmlnsXlink="http://www.w3.org/1999/xlink">
                    <rect width="105" height="108" fill="url(#pattern0)"/><defs><pattern id="pattern0" patternContentUnits="objectBoundingBox" width="1" height="1"><use xlinkHref="#image0" transform="scale(0.00952381 0.00925926)"/></pattern><image id="image0" width="105" height="108" xlinkHref="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAGkAAABsCAYAAABpT5yJAAABN2lDQ1BBZG9iZSBSR0IgKDE5OTgpAAAokZWPv0rDUBSHvxtFxaFWCOLgcCdRUGzVwYxJW4ogWKtDkq1JQ5ViEm6uf/oQjm4dXNx9AidHwUHxCXwDxamDQ4QMBYvf9J3fORzOAaNi152GUYbzWKt205Gu58vZF2aYAoBOmKV2q3UAECdxxBjf7wiA10277jTG+38yH6ZKAyNguxtlIYgK0L/SqQYxBMygn2oQD4CpTto1EE9AqZf7G1AKcv8ASsr1fBBfgNlzPR+MOcAMcl8BTB1da4Bakg7UWe9Uy6plWdLuJkEkjweZjs4zuR+HiUoT1dFRF8jvA2AxH2w3HblWtay99X/+PRHX82Vun0cIQCw9F1lBeKEuf1UYO5PrYsdwGQ7vYXpUZLs3cLcBC7dFtlqF8hY8Dn8AwMZP/fNTP8gAAAAJcEhZcwAACxMAAAsTAQCanBgAAATzaVRYdFhNTDpjb20uYWRvYmUueG1wAAAAAAA8P3hwYWNrZXQgYmVnaW49Iu+7vyIgaWQ9Ilc1TTBNcENlaGlIenJlU3pOVGN6a2M5ZCI/PiA8eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSJBZG9iZSBYTVAgQ29yZSA1LjYtYzE0NSA3OS4xNjM0OTksIDIwMTgvMDgvMTMtMTY6NDA6MjIgICAgICAgICI+IDxyZGY6UkRGIHhtbG5zOnJkZj0iaHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyI+IDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiIHhtbG5zOnhtcD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wLyIgeG1sbnM6ZGM9Imh0dHA6Ly9wdXJsLm9yZy9kYy9lbGVtZW50cy8xLjEvIiB4bWxuczpwaG90b3Nob3A9Imh0dHA6Ly9ucy5hZG9iZS5jb20vcGhvdG9zaG9wLzEuMC8iIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIiB4bWxuczpzdEV2dD0iaHR0cDovL25zLmFkb2JlLmNvbS94YXAvMS4wL3NUeXBlL1Jlc291cmNlRXZlbnQjIiB4bXA6Q3JlYXRvclRvb2w9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIgeG1wOkNyZWF0ZURhdGU9IjIwMjAtMDQtMjZUMTQ6MDM6NDErMDM6MDAiIHhtcDpNb2RpZnlEYXRlPSIyMDIwLTA0LTI2VDE0OjA1OjM2KzAzOjAwIiB4bXA6TWV0YWRhdGFEYXRlPSIyMDIwLTA0LTI2VDE0OjA1OjM2KzAzOjAwIiBkYzpmb3JtYXQ9ImltYWdlL3BuZyIgcGhvdG9zaG9wOkNvbG9yTW9kZT0iMyIgeG1wTU06SW5zdGFuY2VJRD0ieG1wLmlpZDoyYjZiMTdlNi04MzE1LTA1NGItODUyYy01ZGY3NWEyZGUyYTUiIHhtcE1NOkRvY3VtZW50SUQ9InhtcC5kaWQ6MmI2YjE3ZTYtODMxNS0wNTRiLTg1MmMtNWRmNzVhMmRlMmE1IiB4bXBNTTpPcmlnaW5hbERvY3VtZW50SUQ9InhtcC5kaWQ6MmI2YjE3ZTYtODMxNS0wNTRiLTg1MmMtNWRmNzVhMmRlMmE1Ij4gPHhtcE1NOkhpc3Rvcnk+IDxyZGY6U2VxPiA8cmRmOmxpIHN0RXZ0OmFjdGlvbj0iY3JlYXRlZCIgc3RFdnQ6aW5zdGFuY2VJRD0ieG1wLmlpZDoyYjZiMTdlNi04MzE1LTA1NGItODUyYy01ZGY3NWEyZGUyYTUiIHN0RXZ0OndoZW49IjIwMjAtMDQtMjZUMTQ6MDM6NDErMDM6MDAiIHN0RXZ0OnNvZnR3YXJlQWdlbnQ9IkFkb2JlIFBob3Rvc2hvcCBDQyAyMDE5IChXaW5kb3dzKSIvPiA8L3JkZjpTZXE+IDwveG1wTU06SGlzdG9yeT4gPC9yZGY6RGVzY3JpcHRpb24+IDwvcmRmOlJERj4gPC94OnhtcG1ldGE+IDw/eHBhY2tldCBlbmQ9InIiPz4VIU3cAAA4d0lEQVR4nLW9W4xs2Xke9v3rsndV93B4aFmxEF2m7TixEwTgUR4SvwQ8YxgJ5CAhZceOA0jm0IqRN3GoKHrIg2YYIIgdOJaph0BIgHAIBBIEyRQJxQ8OkHCEXCDDQTIMIkOkY7J5JzMczpk53VV7r9ufh3/9u/5apw7pl2yg0d1Vtdde679+/2WtIgAg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"/></defs>
                </svg>
            </Figure>
            <div className="indexAbout-sidecontent_bottom">
                <Bold>страховка</Bold>
                <Dim>все машины застрахованы КАСКО</Dim>
            </div>
        </div>
        <div className="flex-container flex-column" style={{transform: "translateX(5vw)"}}>
            <Figure figureName="circle" style={{backgroundColor:"#ffff"}} className="circle indexAbout-sidecontent_top">
                <img alt="umbrella" src="../sources/icons/docs.png"/>
            </Figure>
            <div className="indexAbout-sidecontent_bottom">
                <Bold>по двум документам</Bold>
                <Dim>Понадобятся Ваш паспорт и водительское удостоверение</Dim>
            </div>
        </div>
    </div>);

const RestrictionsContent = () => (
    <div>
        <Line width="auto"/>
        <Restrictions title="Ограничения">
            <div className="index-restrictions">
                Вам должно быть не менее 23 лет, у Вас должно быть не менее 3 лет водительского стажа.
            </div>
        </Restrictions>
        <Line width="auto"/>
        <Restrictions title="Автомобили">
            <div className="index-restrictions">
                Все автомобили стоят на учете компании. Транспортный налог уже включен в стоимость аренды.
            </div>
        </Restrictions>
        <Line width="auto"/>
        <Restrictions title="Оплата">
            <div className="index-restrictions">
                Оплата взимается с Вашего внутреннего счета. Перед арендой его необходимо пополнить, учитывайте это при планировании своего времени.
            </div>
        </Restrictions>
        <Line width="auto"/>
    </div>);


export default function IndexAbout() {
    return <Section style={{backgroundColor:"#DEF0F0"}}>
        <Container style={{paddingBottom:"4vh"}}>
        <div className="flex-container about-container">
            <div className="about-content">
                <SectionTitle>А что это?</SectionTitle>
                <div className="flex-container flex-column index-about_text_container">
                    <div className="index-text">
                        Всё просто! Мы даём Вам машину на время! Вам лишь необходимо выбрать подходящуюю машину, нажать пару кнопок и она Ваша.
                    </div>
                    <RestrictionsContent/>
                </div>
            </div>
            <SideContent/>
        </div>
        </Container>
    </Section>
}