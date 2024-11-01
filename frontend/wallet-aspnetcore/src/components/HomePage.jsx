import {
	Stat,
	StatLabel,
	StatNumber,
	Divider,
	// Box,
  } from '@chakra-ui/react'
import { useState,  useEffect} from "react";
import OperationForm from './OperationForm';
import Transaction from './Transaction';
import {fetchTransactions, createTransaction} from '../services/Transactions';
import { getCurrentUser } from "../services/Users";
import { fetchBalance } from '../services/Balance';

export default function HomePage() {
	const [transactions, setTransactions] = useState();
	const [balance, setBalance] = useState();
	const [loaded, setLoaded] = useState(false);

	useEffect(() => {
		const fetchData = async () => {
			let userId = getCurrentUser();
			try
			{
				let transactions = await fetchTransactions(userId);
				setTransactions(transactions);
				console.log(transactions);
				let balance = await fetchBalance(userId);
				setBalance(balance);
				console.log(balance);
			}
			finally {
				setLoaded(true);
			}
			
			
		};
		fetchData();
		
	}, []);

	const onCreate = async (transaction) => {
		await createTransaction(transaction);
		let transactions = await fetchTransactions(getCurrentUser());
		setTransactions(transactions);
		let balance = await fetchBalance(getCurrentUser());
		setBalance(balance);
	};


	if (loaded) {
		return (
			<section className="p-8 flex flex-row justify-start items-start gap-12">
				<div className="p-8">
					<Stat>
						<StatLabel fontSize='xl'>Баланс</StatLabel>
						<StatNumber>{balance} Руб.</StatNumber>
					</Stat>
					<Divider size='xl' borderWidth="1px" borderColor={"teal"} />
					<Divider size='xl' borderWidth="20px" borderColor={"white"} />
					<OperationForm titleText={"Пополнение"} kindOfTransaction={true} onCreate={onCreate}/>
					<Divider size='xl' borderWidth="6px" borderColor={"white"} />
					<OperationForm titleText={"Списание"} kindOfTransaction={false} onCreate={onCreate}/>
					<Divider size='xl' borderWidth="6px" borderColor={"white"} />
				</div>
				{/* <Box className="flex flex-col">
					
				</Box> */}
				
				<ul className="flex flex-col gap-5 w-1/2">
					{transactions.map((t) => (
						<li key={t.id}>
							<Transaction
								amount={t.amount}
								createdAt={t.operationDate}
								categoryName={t.categoryName}
							/>
						</li>
					))}
				</ul> 
				
				
			</section>
			
		);
	}

	return <span>Loading...</span>;
	
}