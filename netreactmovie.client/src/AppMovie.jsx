import { BrowserRouter, Routes, Route } from "react-router-dom";
// /import Add from "./browserpages/Add";
import Movies from "./pages/Movies.jsx";
// import Update from "./browserpages/Update";
import "../style.css";

function AppMovie() {
	return (
		<div className="App">
			<BrowserRouter>
				<Routes>
					<Route path="/" element="" />
					<Route path="/" element={<Movies />} />
					{/* <Route path="/add" element={<Add />} /> */}
					{/* <Route path="/update/:id" element={<Update />} /> */}
				</Routes>
			</BrowserRouter>
		</div>
	);
}

export default AppMovie;
