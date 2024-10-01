import { useRef, useState } from "react";
import axiosClient from "../axios_client";

const MovieSearch = () => {
	const [query, setQuery] = useState("");
    const [error, setError] = useState(null);
	const [searchResults, setSearchResults] = useState([]);

	const url = "/Movie/search";
    const searchRef = useRef();

	const searchMovie = async (url, payload) => {
        // alert(payload.title)
		try {
			const resp = await axiosClient.post(url, payload);
			if (Object.getOwnPropertyNames(resp).includes("Search")) {
				setSearchResults(resp.Search);
				//alert("search seemn");
				//location.reload()
			} else setError(error.response.data);
		} catch (err) {
			console.log(err);
		}
	};

	const handleSearch = (event) => {
		event.preventDefault();

        const payload = {title: searchRef.current.value}
        setQuery(searchRef.current.value);
        searchMovie(url, payload)
		
	};

	return (
		<div style={{ margin: "20px", width: "800px !important" }}>
			{/* Search Form */}
			<form onSubmit={handleSearch} style={{ display: "flex", width: "100%" }}>
				<input
					className="search"
					type="text"
					placeholder="Search for a movie..."
					ref={searchRef}
					value={query}
					onChange={(e) => setQuery(e.target.value)}
				/>
				<button type="submit" className="searchButton">
					Search
				</button>
			</form>

			{/* Search Results */}
			<div style={{ marginTop: "20px" }}>
				{searchResults.length > 0 ? (
					<ul>
						{searchResults.map((result) => (
							<li
								key={result.id}
								style={{ padding: "10px 0", listStyle: "none" }}
							>
								<strong>{result.Title}</strong> ({result.Year})
							</li>
						))}
					</ul>
				) : (
					query && <p>No results found for "{query}".</p>
				)}
			</div>
		</div>
	);
};

export default MovieSearch;
