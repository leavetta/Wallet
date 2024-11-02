import {
	Button,
    Input,
    Text
  } from '@chakra-ui/react'
  import { useState,  useEffect} from "react";
  import { createCategory, fetchCategories } from "../services/Categories";
  import Select from 'react-select';
  import CategoryForm from './CategoryForm';

export default function OperationForm({titleText, kindOfTransaction, onCreate}) {
	const [options, setOptions] = useState([""]);
    const [transaction, setTransaction] = useState("");

	const onSubmit = (e) => {
		e.preventDefault();
		setTransaction({ ...transaction, amount: null })
		onCreate(transaction);
	};

	useEffect(() => {
	  const getData = async () => {
        
		const arr = [];
        await fetchCategories(kindOfTransaction).then((res) => {
            let result = res;
            result.map((category) => {
              return arr.push({value: category.id, label: category.name});
            });
            setOptions(arr);
			console.log("Options " + options.label);
            
          });
	  };
	  getData();
	}, []);

	const onCreateCategory = async (category) =>{
		await createCategory(category, kindOfTransaction);
		let result = await fetchCategories(kindOfTransaction);
		const arr = [];
		result.map((category) => {
			arr.push({value: category.id, label: category.name});
		  });
		  setOptions(arr);
	};

	return (
		<section className="w-full flex flex-row gap-3">
			<form onSubmit={onSubmit} className="w-full flex flex-col gap-3">
			<Text fontSize='xl'>{titleText}</Text>
				<Input
					placeholder="Сумма"
					value={transaction?.amount ?? ""}
					onChange={(e) => setTransaction({ ...transaction, amount: e.target.value })}
				/>
				<Select 
					value={options.label}
					onChange={(e) => setTransaction({ ...transaction, categoryid: e.value })} //strange - must be e.target.value
					options={options} />

				<Button type="submit" colorScheme="teal">
					Создать
				</Button>
			</form>
			<CategoryForm onCreate={onCreateCategory}/>
		</section>
		
		
	);
}