import MovieQuery from "./MovieQuery";

const MovieQueryList = ({ queries }) => {
	return (
		<div>
			{queries.map((query) => (
				<MovieQuery key={query.id} query={query} />
			))}
		</div>
	);
};

export default MovieQueryList;
