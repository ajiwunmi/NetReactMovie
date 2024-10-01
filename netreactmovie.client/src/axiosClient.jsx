import axios from "axios";

const registerUser = async (userData) => {
	try {
		const response = await axios.post(
			"https://your-api-url.com/api/users/register",
			userData,
			{
				headers: {
					"Content-Type": "application/json",
				},
			}
		);
		console.log("User registered successfully", response.data);
	} catch (error) {
		console.error(
			"Error registering user",
			error.response ? error.response.data : error.message
		);
	}
};

// Example usage
const userData = {
	username: "newuser",
	email: "newuser@example.com",
	password: "securepassword",
};

registerUser(userData);
