import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ChatModel } from '../../../models/chat/chat.module';

@Injectable({
  providedIn: 'root'
})

export class ChatService {

<<<<<<< HEAD
  private readonly baseUrl = 'http://localhost:7215/Chat'; 
=======
  private readonly baseUrl = 'http://localhost:5208/Chat'; 
>>>>>>> 875b81d (Initial commit to main)

  constructor(private http: HttpClient) {}

  sendMessage(model: ChatModel): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/sentToPrivateChat`, model);
  }

  getChatHistory(SenderId: string, ReceiverId: string): Observable<ChatModel[]> {
    const body = { SenderId, ReceiverId };
    return this.http.post<ChatModel[]>(`${this.baseUrl}/getHistory`, body);
  }

  getUserChats(userId: string): Observable<ChatModel[]> {
    return this.http.get<ChatModel[]>(`${this.baseUrl}/getUserChats${userId}`);
  }

  makeMessageAsRead(messageId: string) : Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/updateMessageReadStatus/${messageId}`, messageId)
  }

  createGroup(groupRequest: any) : Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/createGroup`, groupRequest)
  }

  sentGroupMessage(model: ChatModel): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/sentGroupMessage`, model)
  }

  getGroupMessageHistory(groupId: string): Observable<ChatModel[]> {
    return this.http.get<ChatModel[]>(`${this.baseUrl}/getGroupChatHistory${groupId}`)
  }

  makeGroupMessageAsRead(messageId: string) : Observable<any> {
    return this.http.patch<any>(`${this.baseUrl}/updateGroupMessageReadStatus/${messageId}`, messageId)
  }

  getGroupUsers(groupId: string): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/getUsersByGroup/${groupId}`)
  }

  deleteUsersFromGroup(userIds: string[], groupId: string): Observable<boolean> {
    const body = {
      groupId: groupId,
      usersId: userIds 
    };
<<<<<<< HEAD
    return this.http.post<boolean>(`https://localhost:7215/Chat/deleteFromGroup`, body);
=======
    return this.http.post<boolean>(`https://localhost:5208/Chat/deleteFromGroup`, body);
>>>>>>> 875b81d (Initial commit to main)
  }
  
  addUsersToGroup(users: { id: string, recieverName: string, userPhoto: string }[], groupId: string): Observable<any> {
    const body = {
      groupId: groupId,
      users: users
    };

  
    return this.http.post(`${this.baseUrl}/addUserToGroup`, body);
  }
  
}
