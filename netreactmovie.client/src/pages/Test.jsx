import React from "react";
import axiosClient from "../../axios_client";

const Test = () => {
	// const [movies, setMovies] = useState([]);
 axiosClient
		.get("/search?query=man")
		.then(({ data }) => {
			console.log(data);
		})
		.catch((error) => {
			console.log(error);
		});

	

	

	return (
		<div>
			<h1>Movie Shop On-line</h1>
			<div className="movies">
				<div>Test Page</div>
			</div>
			
		</div>
	);
};

export default Test;
