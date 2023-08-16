import { UserInfo } from "../auth";

export type Conversation = {
    id: string;
    name: string;
    isGroup: boolean;
    createdAt: Date;
    lastMessageAt: Date;
    users: UserInfo[]; 
    messages: Message[];
};

export type Message = {
    id: string;
    body: string;
    image: string;
    createdAt: Date;
    sender: UserInfo;
}

export type CreateConversationCommand = {
    name: string;
    isGroup: boolean;
    userIds: string[];
}