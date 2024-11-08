import {
	Stat,
	StatLabel,
	StatNumber,
	Divider,
	Text,
  } from '@chakra-ui/react'
import { useState,  useEffect} from "react";
import OperationForm from './OperationForm';
import Transaction from './Transaction';
import DateFilter from './DateFilter';
import {fetchTransactions, createTransaction} from '../services/Transactions';
import { getCurrentUser } from "../services/Users";
import { fetchBalance } from '../services/Balance';

export default function HomePage() {
	const [transactions, setTransactions] = useState();
	const [balance, setBalance] = useState();
	const [loaded, setLoaded] = useState(false);
	const [startDate, setStartDate] = useState(null);
	const [endDate, setEndDate] = useState(null);

	useEffect(() => {
		const fetchData = async () => {
			let userId = getCurrentUser();
			try
			{
				let transactions = await fetchTransactions(userId, startDate, endDate);
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
		
	}, [startDate, endDate]);

	const onCreate = async (transaction) => {
		await createTransaction(transaction);
		let transactions = await fetchTransactions(getCurrentUser(), startDate, endDate);
		setTransactions(transactions);
		let balance = await fetchBalance(getCurrentUser());
		setBalance(balance);
	};


	if (loaded) {
		return (
			<section className="p-8 flex flex-row justify-start items-start gap-12">
				<div className="p-8 w-full" >
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
				<div className="flex flex-col gap-4 p-8">
					<Text fontSize='xl' bg='teal' color='white'>История операций</Text>
					{/* <Divider size='xl' borderWidth="2px" borderColor={"gray"} /> */}
					<ul className="flex flex-col gap-5">
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
				</div>
				
				<DateFilter startDate={startDate} setStartDate={setStartDate} endDate={endDate} setEndDate={setEndDate}/>
				
			</section>
			
		);
	}

	return <span>Loading...</span>;
	
}