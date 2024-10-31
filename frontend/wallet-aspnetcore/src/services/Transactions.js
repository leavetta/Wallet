import axios from "axios";


export const createTransaction = async (transaction) => {
	try {
		var response = await axios.post("https://localhost:7242/transactions", transaction);
		return response.data;
	} catch (e) {
		console.error(e);
	}
};

export const fetchTransactions = async () => {
	try {
		var response = await axios.get("https://localhost:7242/transactions");
        
		return response.data;
	} catch (e) {
		console.error(e);
	}
};

