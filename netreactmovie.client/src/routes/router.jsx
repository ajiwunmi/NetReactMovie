import {Navigate, createBrowserRouter} from 'react-router-dom';

import Login from '../views/Login';
import Signup from '../views/Signup';
import Users from '../views/Users';
import Movies from "../views/Movies";
import NotFound from '../views/NotFound';
import DefaultLayout from '../components/DefaultLayout';
import GuestLayout from '../components/GuestLayout';
import Dashboard from '../views/Dashboard';
import UserForm from '../views/UserForm';

const router = createBrowserRouter([
	{
		path: "/",
		element: <DefaultLayout />,
		children: [
			{
				path: "/",
				element: <Navigate to="/Movies" />,
			},
			{
				path: "/dashboard",
				element: <Dashboard />,
			},
			{
				path: "/users",
				element: <Movies />,
			},
			{
				path: "/users/new",
				element: <UserForm key="create" />,
			},
			{
				path: "/users/:id",
				element: <UserForm key="update" />,
			},
		],
	},
	{
		path: "/",
		element: <GuestLayout />,
		children: [
			{
				path: "/login",
				element: <Navigate to="/Movies" />,
			},
			{
				path: "/signup",
				element: <Signup />,
			},
		],
	},

	{
		path: "*",
		element: <NotFound />,
	},
]);

export default router;
