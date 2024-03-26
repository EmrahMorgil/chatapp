import { enmSoundType } from "../../core/enums/SoundType";

const sounds = [{ type: enmSoundType.get, sound: new Audio("../../sounds/getmessage.wav") }, { type: enmSoundType.send, sound: new Audio("../../sounds/sendtomessage.wav") },
{ type: enmSoundType.join, sound: new Audio("../../sounds/joinroom.wav") }, { type: enmSoundType.leave, sound: new Audio("../../sounds/leaveroom.wav") }];


export default sounds;