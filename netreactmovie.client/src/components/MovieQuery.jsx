import React from "react";
import MovieItem from "./MovieItem";

const MovieQuery = ({ query }) => {
	return (
		<div
			style={{
				marginBottom: "2rem",
				borderBottom: "1px solid #ccc",
				paddingBottom: "1rem",
			}}
		>
			<h2>Search Query: {query.query}</h2>
			<p>Search Time: {new Date(query.searchTime).toLocaleString()}</p>
			<h3>Movies:</h3>
			<div style={{ display: "flex", flexWrap: "wrap" }}>
				{query.movies.map((movie) => (
					<MovieItem key={movie.id} movie={movie} />
				))}
			</div>
		</div>
	);
};

export default MovieQuery;
