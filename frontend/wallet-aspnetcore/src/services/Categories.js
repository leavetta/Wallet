import axios from "axios";


export const createCategory = async (category) => {
	try {
		var response = await axios.post("https://localhost:7242/categories", category);
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

