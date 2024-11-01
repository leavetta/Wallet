import axios from "axios";

export const fetchBalance = async (userId) => {
	try {
		var response = await axios.get("https://localhost:7242/balances", {
			params: {
				id: userId,
			},

		});
        
		return response.data.currentAmount;
	} catch (e) {
		console.error(e);
	}
};
