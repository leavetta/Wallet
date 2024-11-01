import axios from "axios";


export const createCategory = async (category, isDeposit) => {
	try {
		var response = await axios.post("https://localhost:7242/categories", {
			params:{
				name: category.name,
				isIncome: isDeposit,
			}});
		return response.data;
	} catch (e) {
		console.error(e);
	}
};

export const fetchCategories = async (selected) => {
	try {
		var response = await axios.get("https://localhost:7242/categories/selected", {
			params: {
				selectedKey: selected,
			},
		});
        
		return response.data;
	} catch (e) {
		console.error(e);
	}
};

