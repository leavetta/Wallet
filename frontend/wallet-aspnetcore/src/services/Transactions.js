import axios from "axios";
import { getCurrentUser } from '../services/Users';

export const createTransaction = async (transaction) => {
	try {
		transaction.userId = getCurrentUser();
		console.log(transaction);
		var response = await axios.post("https://localhost:7242/transactions", transaction);
		return response.data;
	} catch (e) {
		console.error(e);
	}
};

export const fetchTransactions = async (userId, startDate, endDate) => { //todo: convert startDate and endDate to normal time and date
	try {
		var response = await axios.get("https://localhost:7242/transactions", {
			params: {
				id: userId,
				startDate: startDate,
				endDate: endDate
			},
		});
        
		return response.data;
	} catch (e) {
		console.error(e);
	}
};
