export interface ChatModel {
    messageId: string;
    senderId: string;
    receiverId: string;
    senderName?: string;
    text: string;
    sendAt: Date;
    isRead?: boolean; 
  }