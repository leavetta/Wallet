import {
	Stat,
	StatLabel,
	StatNumber,
	Divider
  } from '@chakra-ui/react'
//   import { useState,  useEffect} from "react";
// import WithdrawForm from './WithdrawForm';
import OperationForm from './OperationForm';

export default function HomePage() {
	// const [value, setValue] = useState<string[]>([]);
	

	return (
		<section className="p-8 flex flex-row justify-start items-start gap-12">
			<div className="p-8">
				<Stat>
					<StatLabel>Баланс</StatLabel>
					<StatNumber>£0.00</StatNumber>
				</Stat>
				<Divider size='xl' borderWidth="6px" borderColor={"white"} />
				<OperationForm titleText={"Пополнение"} kindOfTransaction={true}/>
				<Divider size='xl' borderWidth="6px" borderColor={"white"} />
				<OperationForm titleText={"Списание"} kindOfTransaction={false}/>
        	</div>

			<ul className="flex flex-col gap-5 w-1/2">
				{notes.map((n) => (
					<li key={n.id}>
						<Note
							title={n.title}
							desctiption={n.description}
							createdAt={n.createdAt}
						/>
					</li>
				))}
			</ul>
		</section>
		
	);
}