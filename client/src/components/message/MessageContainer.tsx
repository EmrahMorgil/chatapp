import React from 'react'
import MessageBar from './MessageBar'
import Messages from './Messages'
import MessageInput from './MessageInput'
import mdlMessageDto from '../../core/dto/MessageDto';
import mdlUser from '../../core/models/User';
import mdlUserDetailDto from '../../core/dto/UserDetailDto';

interface IMessageContainerProps {
  messages?: Array<mdlMessageDto>;
  fnGetMessages?: Function;
  socket?: any;
  scrollToBottom?: Function;
  activeUser?: mdlUserDetailDto;
}

const MessageContainer: React.FC<IMessageContainerProps> = (props) => {

  return (
    <div className='col-xl-6 col-md-6 col-sm-12 message-container-heighth' style={{ backgroundColor: "#111B21", borderLeft: "1px solid white", padding: "0px", margin: "0px" }}>
      <MessageBar />
      <Messages messages={props.messages} scrollToBottom={props.scrollToBottom} activeUser={props.activeUser}/>
      <MessageInput scrollToBottom={props.scrollToBottom}/>
    </div>
  )
}

export default MessageContainer