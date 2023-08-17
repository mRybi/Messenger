import { FC } from 'react';
import { Navigate, useLocation } from 'react-router-dom';

import { useAppSelector, useSessionUser } from '../hooks';

type PrivateRouteProps = {
    children: JSX.Element;
}

export const PrivateRoute: FC<PrivateRouteProps> = ({ children }) => {
    const user = useSessionUser();

    console.log("USR", user);

    let location = useLocation();
    if (!user?.token) {
        // not logged in so redirect to login page with the return url
        return <Navigate to="/" state={{ from: location }} replace />
    }

    // authorized so return child components
    return children;
}