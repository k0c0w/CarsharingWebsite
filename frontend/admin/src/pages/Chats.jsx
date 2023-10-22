import { useEffect, useState } from "react";
import API from "../httpclient/axios_client"
import '../styles/chats-container.css';
import RoomList from "../components/Chat/RoomList";
import Chat from "./Chat";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


export default function Chats () {
    const [connection, setConnection] = useState();

    const [onlineRooms, setOnlineRooms] = useState([]);
    const [activeRoom, setActiveRoom] = useState();

    const [activeRoomMessages, setActiveRoomMessages] = useState([])

    const [errorMessage, setErrorMessage] = useState();

    const onJoinRoomResultRecieved = async (update) => {
        if (update.success){
          const history = await API.getChatHistory(update.roomId);
          setActiveRoomMessages(history);
          const room = onlineRooms.find(elem => elem.roomId === update.roomId);
          setActiveRoom(room);
        }
        else{
          setErrorMessage("Не возможно присоедениться к комнате");
        }
    }

    const onLeaveRoomResultRecieved = (update) => {
      if (update.success){
        setActiveRoom();
        setActiveRoomMessages([]);
      }
      else{
        setErrorMessage("Не возможно поикнуть комнату");
      }
    }
    useEffect(() => {
      async function retriveRooms() {
        const rooms = await API.getOnlineRooms();
        const mappedRooms = rooms.map(room => { 
          return { roomId: room.roomId, roomName: room.roomName, pending: room.processingManagersCount === 0}});
        setOnlineRooms([...onlineRooms, ...mappedRooms]);
      }
      retriveRooms();
    }, []);

    useEffect( () => {
      const startConnection = async () => {
          try {
          const localConnection = new HubConnectionBuilder()
           .withUrl('https://localhost:7129/chat')
            .configureLogging(LogLevel.Information)
           .build();
      
           localConnection.on('RecieveMessage', (message) => {
              setActiveRoomMessages(messages => [...messages, message])
              //todo: should trigger only on message
            });
      
          localConnection.on('JoinRoomResult', update => onJoinRoomResultRecieved(update));
          localConnection.on('LeaveRoomResult', update => onLeaveRoomResultRecieved(update));
          localConnection.on('ChatRoomUpdate', update => {
            console.debug(update);
            switch(update.event) {
              // Created
              case 1:
              {
                  const newRooms = [{roomName: update.roomName, roomId: update.roomId, pending: true}, ...onlineRooms];
                  setOnlineRooms(newRooms);
                  break;
              }
              // Deleted
              case 2:
              {
                  const elemIndex = onlineRooms.map(room => room.roomId).indexOf(update.roomId);
                  if (elemIndex > -1) {
                      setOnlineRooms(onlineRooms.splice(elemIndex, 1));
                  }

                  if (activeRoom && activeRoom.roomId === update.roomId){
                      setErrorMessage("Комната была закрыта");
                      setActiveRoomMessages([]);
                      setActiveRoom();
                  }

                  break;
              }
              // Manager joined
              case 3: {
                  const joinedRoomIndex = onlineRooms.map(room => room.roomId).indexOf(update.roomId);
                  if (joinedRoomIndex > -1) {
                      const oldRoom = onlineRooms[joinedRoomIndex];
                      oldRoom.pending = false;
                      onlineRooms[joinedRoomIndex] = oldRoom;
                      setOnlineRooms(onlineRooms);
                  }
                  break;
              }

              // manager left
              case 4: {
                  const joinedRoomIndex = onlineRooms.map(room => room.roomId).indexOf(update.roomId);
                  const leftRoomIndex = onlineRooms.map(room => room.roomId).indexOf(update.roomId);
                  if (joinedRoomIndex > -1) {
                      const oldRoom = onlineRooms[leftRoomIndex];
                      oldRoom.pending = true;
                      onlineRooms[leftRoomIndex] = oldRoom;
                      setOnlineRooms(onlineRooms);
                  }
                  break;
              }
              default: {
                  console.log(update);
                  break;
              }
              
            }
          });
            
          localConnection.onclose(() => {
              setConnection();
              setActiveRoomMessages([]);
              setActiveRoom();
          });
      
          await localConnection.start();
              setConnection(localConnection);
          } catch (e) {
          console.log(e);
          }
      }
      startConnection()
      // todo: causes multiple reconnections but without it onlinerooms are not rendered
    }, [onlineRooms]
    );


    const joinRoom = async (roomId) => {
        try {
                await connection.invoke('JoinRoom', roomId);
            } catch (e) {
            console.log(e)
            }   
    }

    const leaveRoom = async (roomId) => {
        try {
                await connection.invoke('LeaveRoom', roomId);
            } catch (e) {
            console.log(e)
            }   
    }

    const sendMessage = async (message) => {
        try {
          const messageModel = {
            Text: message,
            Time: new Date().toJSON(),
            RoomId: activeRoom.roomId,
          };
    
          await connection.invoke('SendMessage', messageModel);
        } catch (e) {
          console.log(e)
        }
      }

    return (
        <>
            {!activeRoom && <RoomList  onlineRooms={onlineRooms} joinRoom = {joinRoom}/>}
            {errorMessage && <div style='color:"red"'>{errorMessage}</div>}
            {activeRoom && <Chat leaveRoom = {() => leaveRoom(activeRoom.roomId)} messages = {activeRoomMessages} sendMessage = {sendMessage}/>}
        </>
    )
}