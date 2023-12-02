import React from 'react'
import MessageBar from './MessageBar/MessageBar'
import Messages from './Messages/Messages'
import MessageInput from './MessageInput/MessageInput'
import { mdlMessage } from '../../Core/Modals/Message'
interface MessageProps {
  messages?: mdlMessage[];
  fnGetMessages?: Function;
  socket?: any;
  scrollToBottom?: Function;
}

const Message: React.FC<MessageProps> = (props) => {

  return (
    <div className='col-xl-6 col-md-6 col-sm-12 message-container-heighth' style={{backgroundColor: "#111B21", border: "1px solid white", padding: "0px", margin: "0px" }}>
      <MessageBar />
      <Messages messages={props.messages} scrollToBottom={props.scrollToBottom} />
      <MessageInput scrollToBottom={props.scrollToBottom} />
    </div>
  )
}

export default Message