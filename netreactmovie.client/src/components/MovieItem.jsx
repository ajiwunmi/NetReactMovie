import { useEffect, useState } from "react";
import axiosClient from "../axios_client";
import Modal from "./Modal";

const MovieItem = ({ movie }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
		const [movieDetails, setMovieDetails] = useState(null);
		const [loading, setLoading] = useState(false);
		const [error, setError] = useState(null);
        

     const url = "/Movie";

	const handleMoreDetails = async (moviID) => {
        
		setIsModalOpen(true);
		setLoading(true);

		try {
			const response = await axiosClient.get(`${url}/${moviID}`);
			//const data = await response.json();
			console.log("RESPONSE  ", response.data);

			if (response.status )
            {
				setMovieDetails(response.data)
			} else {
				setError("Movie details not found");
			}
		} catch (error) {
			setError("An error occurred while fetching movie details");
		}
		setLoading(false);
	};

     const handleCloseModal = () => {
				setIsModalOpen(false);
				setMovieDetails(null);
				setError(null);
			};


	return (
		<div
			style={{
				margin: "10px",
				border: "1px solid #ccc",
				padding: "10px",
				width: "250px",
			}}
			className="movie"
		>
			{movie.poster && <img src={movie.poster} alt={movie.title} />}

			<h4>
				{movie.title} ({movie.year})
			</h4>
			<p>IMDB ID: {movie.imdbID}</p>
			<button className="update" onClick={() => handleMoreDetails(movie.id)}>
				More Details
			</button>

			{isModalOpen && (
				<Modal onClose={handleCloseModal}>
					{loading ? (
						<p>Loading...</p>
					) : error ? (
						<p>{error}</p>
					) : (
						movieDetails && (
							<div>
								<h2>
									{movieDetails.title} ({movieDetails.year})
								</h2>
								<img src={movieDetails.poster} alt={movieDetails.title} />
								<p>
									<strong>Genre:</strong> {movieDetails.genre}
								</p>
								<p>
									<strong>Plot:</strong> {movieDetails.plot}
								</p>
								<p>
									<strong>Director:</strong> {movieDetails.director}
								</p>
								<p>
									<strong>Actors:</strong> {movieDetails.actors}
								</p>
								<p>
									<strong>IMDB Rating:</strong> {movieDetails.imdbRating}
								</p>
								<button className="delete" onClick={handleCloseModal}>Close</button>
							</div>
						)
					)}
				</Modal>
			)}
		</div>
	);
};

export default MovieItem;
