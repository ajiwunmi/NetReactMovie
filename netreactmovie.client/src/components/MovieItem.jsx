import React from 'react';

const MovieItem = ({ movie }) => {
  const handleMoreDetails = () => {
    alert(`More details for ${movie.title} (IMDB ID: ${movie.imdbID})`);
    // This is where you can navigate to a details page or display a modal
  };

  return (
    <div style={{ margin: '10px', border: '1px solid #ccc', padding: '10px', width: '250px' }}>
      <img src={movie.poster} alt={movie.title} style={{ width: '100%' }} />
      <h4>{movie.title} ({movie.year})</h4>
      <p>IMDB ID: {movie.imdbID}</p>
      <button onClick={handleMoreDetails}>More Details</button>
    </div>
  );
};

export default MovieItem;
