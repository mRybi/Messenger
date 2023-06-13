import { FC } from 'react';
import { Navigate, useLocation } from 'react-router-dom';

import { useAppSelector } from '../hooks';

type PrivateRouteProps = {
    children: JSX.Element;
}

export const PrivateRoute: FC<PrivateRouteProps> = ({ children }) => {
console.log("PrivateRoute");

    const user = useAppSelector(x => x.userState);
    let location = useLocation();
console.log("user",  user);
    if (!user.user?.token) {
        // not logged in so redirect to login page with the return url
        return <Navigate to="/" state={{ from: location }} replace />
    }

    // authorized so return child components
    return children;
}