import Login from "../screens/Login";
import Home from "../screens/Home";
import Register from "../screens/Register";

const AppRoutes  = [
    {
        index: true,
        element: <Home />
    },
    {
        path: '/login',
        element: <Login />
    },
    {
        path: '/register',
        element: <Register />
    },
]
export default AppRoutes;