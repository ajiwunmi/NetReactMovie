import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
// import App from './App.jsx';
//mport AppMovie from "./AppMovie.jsx";
import Test from "./pages/Test.jsx";
import './index.css'


createRoot(document.getElementById("root")).render(
	<StrictMode>
		<>
			<Test />
		</>
	</StrictMode>
);
