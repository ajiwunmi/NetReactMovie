import React from 'react'
import { useEffect, useState } from 'react'
import { Link } from 'react-router-dom'
import axiosClient from "../axios_client";
import MovieQueryList from "../components/MovieQueryList";

const Movies = () => {
  const [movieQueryData, setMovieQueryData] = useState([]);
    const url = "/Movie/latest";

  useEffect(()=>{   
    fetchAllMovies()
  },[])

     const fetchAllMovies = async ()=> {
      try{
        const resp = await axiosClient.get(url)
        // console.log(resp);
         setMovieQueryData(resp.data);
      }catch(err){
        console.log(err)
      }
    }

  
  return (
		<div>
			<h2 style={{color:"white"}}>Latest Movie Queries</h2>
			<div className="movies">
			   	<MovieQueryList queries={movieQueryData} />
			</div>
			{/* <button className="movieButton">
				<Link to="/add">Add new movie</Link>
			</button> */}
		</div>
	);
}


export default Movies
