import Login from "../screens/Login";
import Home from "../screens/Home";
import Register from "../screens/Register";
import Rooms from "../screens/Rooms";
import MakeBooking from "../screens/MakeBooking";

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
    {
        path: '/rooms',
        element: <Rooms />
    },
    {
        path: '/make-booking',
        element: <MakeBooking />
    },
]
export default AppRoutes;