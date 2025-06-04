export interface ChatList{
    senderId: string,
    recieverId: string,
    senderName: string,
    recieverName: string,
    text: string,
    time: string,
    userPhoto?: string,
    chatType: string
}