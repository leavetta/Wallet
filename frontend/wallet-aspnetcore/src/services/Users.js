import axios from "axios";


export const registerUser = async (user) => {
	try {
		var response = await axios.post("https://localhost:7242/users/register", user);
        
		return response.data.userId;
	} catch (e) {
		console.error(e);
	}
};

export const loginUser = async (user) => {
	try {
		var response = await axios.post("https://localhost:7242/users/login", user);
        localStorage.setItem("user", JSON.stringify(response.data));
		return response.data.userId;
	} catch (e) {
		console.error(e);
	}
};

export const getCurrentUser = () => {
    return JSON.parse(localStorage.getItem("user"));
  };