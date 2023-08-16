import { useMemo } from "react";
import { HiChat } from 'react-icons/hi';
import { HiArrowLeftOnRectangle, HiUsers } from 'react-icons/hi2';
import { useLocation } from "react-router-dom";
import { useConversation } from ".";
import { useLogoutMutation } from "../services/auth/authApi";

export const useRoutes = () => {
  const location = useLocation();
  const { conversationId } = useConversation();
  const [logout] = useLogoutMutation();

  const onLogout = async () => {
    await logout();
    sessionStorage.removeItem("user"); 
  } 

  const routes = useMemo(() => [
    { 
      label: 'Chat', 
      href: '/conversations', 
      icon: HiChat,
      active: location.pathname === '/conversations' || !!conversationId
    },
    { 
      label: 'Users', 
      href: '/users', 
      icon: HiUsers, 
      active: location.pathname === '/users'
    },
    {
      label: 'Logout', 
      onClick: async () => await onLogout(),
      href: '/',
      icon: HiArrowLeftOnRectangle, 
    }
  ], [location, conversationId]);

  return routes;
};