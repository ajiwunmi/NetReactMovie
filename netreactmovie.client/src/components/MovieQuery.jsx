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
			<h4 style={{ color: "white" }}>Search Query: {query.query}</h4>
			<h5 style={{ color: "white" }}>
				Search Time: {new Date(query.searchTime).toLocaleString()}
			</h5>
			
			<div style={{ display: "flex", flexWrap: "wrap" }}>
				{query.movies.map((movie) => (
					<MovieItem key={movie.id} movie={movie} />
				))}
			</div>
		</div>
	);
};

export default MovieQuery;
