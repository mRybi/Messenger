import { FC } from 'react';
import { HomePage } from './pages';
import {
  Routes,
  Route,
} from "react-router-dom";
import { ConversationPage } from './pages/ConversationsPage';
import { PrivateRoute } from './components/PrivateRoute';

export const App: FC = () => {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />
      <Route path="/conversations" element={<PrivateRoute><ConversationPage /></PrivateRoute>} />

    </Routes>
  )
}

