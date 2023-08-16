import { useMemo } from "react";
import { useAppSelector } from ".";
import { UserInfo } from "../services";

export const useSessionUser = () => {
	const session = useAppSelector(x => x.userState);

	const sessionUser = useMemo(() => {
		const sessionUser = sessionStorage.getItem("user");
		const user: UserInfo = sessionUser ? JSON.parse(sessionUser) : null;

		return session.user ? session.user : user;
	}, [sessionStorage, session])

	return sessionUser;
};