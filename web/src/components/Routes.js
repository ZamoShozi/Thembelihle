import Login from "../screens/Login";
import Home from "../screens/Home";

const AppRoutes  = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/login',
        element: <Login />
    },
]
export default AppRoutes;