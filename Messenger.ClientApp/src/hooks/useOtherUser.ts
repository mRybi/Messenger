import { useMemo } from "react";
import { useAppSelector } from ".";
import { UserInfo } from "../services/auth/authModels";
import { Conversation } from "../services/conversation/conversationModels";

export const useOtherUser = (conversation: Conversation | { users: UserInfo[] }) => {
  const session = useAppSelector(x => x.userState);

  const otherUser = useMemo(() => {
    const currentUserEmail = session.user?.email;

    const otherUser = conversation.users.filter((user: UserInfo) => user.email !== currentUserEmail);

    return otherUser[0];
  }, [session.user?.email, conversation.users]);

  return otherUser;
};