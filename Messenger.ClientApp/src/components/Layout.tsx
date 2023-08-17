import { FC } from 'react';
import { Sidebar } from './sidebar/Sidebar';
import { ConversationList } from './ConversationList';
import { useGetAllUsersQuery } from '../services/auth/authApi';
import { ClipLoader } from 'react-spinners';
import { useGetConversationForUserQuery } from '../services/conversation/conversationApi';
import { useAppSelector, useSessionUser } from '../hooks';

type LayoutProps = {
	children: JSX.Element;
}

export const Layout: FC<LayoutProps> = ({ children }) => {
	const user = useSessionUser();
	const { data: conversations, isFetching, isLoading } = useGetConversationForUserQuery(user?.id|| "");

	console.log(" conversations, users", conversations)

	return isLoading ? <ClipLoader size={40} color="#0284c7" /> : (
		<Sidebar>
			<div className="h-full">
				<ConversationList
					title="Messages"
					initialItems={conversations || []}
				/>
				{children}
			</div>
		</Sidebar> )
}