import { useMemo } from "react";
import { useParams } from "react-router-dom";

export const useConversation = () => {
  let { conversationId } = useParams();

  const convId = useMemo(() => {
    if (!conversationId) {
      return '';
    }

    return conversationId as string;
  }, [conversationId]);

  const isOpen = useMemo(() => !!convId, [convId]);

  return useMemo(() => ({
    isOpen,
    conversationId: convId
  }), [isOpen, conversationId]);
};