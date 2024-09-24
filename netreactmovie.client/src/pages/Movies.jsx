import React from 'react'
import { useEffect } from 'react'
import { useState } from 'react'
import { Link } from 'react-router-dom'
// import axiosClient from "../../axiosClient";
  //   setMovies(resp.data);

const Movies = () => {
  const [movies, setMovies] = useState([])

  useEffect(()=>{
    const fetchAllMovies = async ()=> {
      try{
       
      const resp = await axiosClient.get("/search?query=man");
      console.log("==================Pass================");
      console.log(resp);
  
      }catch(err){
         console.log("===============Fail===================");
        console.log(err)
      }
    }
     fetchAllMovies()
  },[])


  const handleDelete = async (id) => {
    try {
      const confirmed = window.confirm("Are you sure you want to delete this movie?");
      if (confirmed) {
        await axios.delete("http://localhost:8800/movies/" + id);
        window.location.reload();
      }
    } catch (err) {
      console.log(err);
    }
  };


  return (
    <div>
      <h1>Movie Shop On-line</h1>
      <div className="movies">
       Movies Here
      </div>
      <button className='movieButton'><Link to="/add">Add new movie</Link></button>
    </div>
  )
}


export default Movies
