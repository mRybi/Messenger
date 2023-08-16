import { FC, useEffect, useMemo, useState } from "react";
import { MdOutlineGroupAdd } from 'react-icons/md';
import clsx from "clsx";
import { find, uniq } from 'lodash';

// import { pusherClient } from "@/app/libs/pusher"; // do notyfikacji
import { ConversationBox } from "./ConversationBox";
import { Conversation } from "../services/conversation/conversationModels";
import { useAppSelector, useConversation } from "../hooks";
import { GroupChatModal } from "./modals/GroupChatModal";
import { useNavigate } from "react-router-dom";

type ConversationListProps = {
  initialItems: Conversation[];
  title?: string;
}

export const ConversationList: FC<ConversationListProps> = ({ 
  initialItems, 
}) => {
  const [items, setItems] = useState(initialItems);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const navigation = useNavigate();
  const session = useAppSelector(x => x.userState);

  const { conversationId, isOpen } = useConversation();

  const pusherKey = useMemo(() => {
    return session.user?.email
  }, [session.user?.email])

  // useEffect(() => {
  //   if (!pusherKey) {
  //     return;
  //   }

  //   pusherClient.subscribe(pusherKey);

  //   const updateHandler = (conversation: Conversation) => {
  //     setItems((current) => current.map((currentConversation) => {
  //       if (currentConversation.id === conversation.id) {
  //         return {
  //           ...currentConversation,
  //           messages: conversation.messages
  //         };
  //       }

  //       return currentConversation;
  //     }));
  //   }

  //   const newHandler = (conversation: Conversation) => {
  //     setItems((current) => {
  //       if (find(current, { id: conversation.id })) {
  //         return current;
  //       }

  //       return [conversation, ...current]
  //     });
  //   }

  //   const removeHandler = (conversation: Conversation) => {
  //     setItems((current) => {
  //       return [...current.filter((convo) => convo.id !== conversation.id)]
  //     });
  //   }

  //   pusherClient.bind('conversation:update', updateHandler)
  //   pusherClient.bind('conversation:new', newHandler)
  //   pusherClient.bind('conversation:remove', removeHandler)
  // }, [pusherKey, navigation]);

  return (
    <>
      <GroupChatModal 
        isOpen={isModalOpen} 
        onClose={() => setIsModalOpen(false)}
      />
      <aside className={clsx(`
        fixed 
        inset-y-0 
        pb-20
        lg:pb-0
        lg:left-20 
        lg:w-80 
        lg:block
        overflow-y-auto 
        border-r 
        border-gray-200 
      `, isOpen ? 'hidden' : 'block w-full left-0')}>
        <div className="px-5">
          <div className="flex justify-between mb-4 pt-4">
            <div className="text-2xl font-bold text-neutral-800">
              Messages
            </div>
            <div 
              onClick={() => setIsModalOpen(true)} 
              className="
                rounded-full 
                p-2 
                bg-gray-100 
                text-gray-600 
                cursor-pointer 
                hover:opacity-75 
                transition
              "
            >
              <MdOutlineGroupAdd size={20} />
            </div>
          </div>
          {items.length > 0 ? items?.map((item) => (
            <ConversationBox
              key={item.id}
              data={item}
              selected={conversationId === item.id}
            />
          )) : <p>pustooo dodaj konwersacje</p>}
        </div>
      </aside>
    </>
   );
}