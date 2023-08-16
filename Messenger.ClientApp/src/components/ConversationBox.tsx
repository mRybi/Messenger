import { FC, useCallback, useMemo } from "react";
import { format } from "date-fns";
import clsx from "clsx";

import { useNavigate } from "react-router-dom";
import { AvatarGroup } from "./AvatarGroup";
import { useAppSelector, useOtherUser } from "../hooks";
import { Avatar } from "./Avatar";
import { Conversation } from "../services/conversation/conversationModels";

type ConversationBoxProps = {
  data: Conversation,
  selected?: boolean;
}

export const ConversationBox: FC<ConversationBoxProps> = ({ 
  data, 
  selected 
}) => {
  const otherUser = useOtherUser(data);
  const session = useAppSelector(x => x.userState);
  const navigation = useNavigate();

  const handleClick = useCallback(() => {
    navigation(`/conversations/${data.id}`);
  }, [data, navigation]);

  const lastMessage = useMemo(() => {
    const messages = data.messages || [];

    return messages[messages.length - 1];
  }, [data.messages]);

  const userEmail = useMemo(() => session.user?.email,
  [session.user?.email]);
  
  // const hasSeen = useMemo(() => {
  //   if (!lastMessage) {
  //     return false;
  //   }

  //   const seenArray = lastMessage.seen || [];

  //   if (!userEmail) {
  //     return false;
  //   }

  //   return seenArray
  //     .filter((user) => user.email === userEmail).length !== 0;
  // }, [userEmail, lastMessage]);

  const lastMessageText = useMemo(() => {
    if (lastMessage?.image) {
      return 'Sent an image';
    }

    if (lastMessage?.body) {
      return lastMessage?.body
    }

    return 'Started a conversation';
  }, [lastMessage]);

  return ( 
    <div
      onClick={handleClick}
      className={clsx(`
        w-full 
        relative 
        flex 
        items-center 
        space-x-3 
        p-3 
        hover:bg-neutral-100
        rounded-lg
        transition
        cursor-pointer
        `,
        selected ? 'bg-neutral-100' : 'bg-white'
      )}
    >
      {data.isGroup ? (
        <AvatarGroup users={data.users} />
      ) : (
        <Avatar user={otherUser} />
      )}
      <div className="min-w-0 flex-1">
        <div className="focus:outline-none">
          <span className="absolute inset-0" aria-hidden="true" />
          <div className="flex justify-between items-center mb-1">
            <p className="text-md font-medium text-gray-900">
              {data.name || otherUser.name}
            </p>
            {lastMessage?.createdAt && (
              <p 
                className="
                  text-xs 
                  text-gray-400 
                  font-light
                "
              >
                {format(new Date(lastMessage.createdAt), 'p')}
              </p>
            )}
          </div>
          <p 
            className={clsx(`
              truncate 
              text-sm
              `,
              // hasSeen ? 'text-gray-500' : 'text-black font-medium'
            )}>
              {lastMessageText}
            </p>
        </div>
      </div>
    </div>
  );
}