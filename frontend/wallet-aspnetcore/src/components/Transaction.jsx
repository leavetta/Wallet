import {
	Box,
} from "@chakra-ui/react";

export default function Transaction({ amount, createdAt, categoryName}) {
	return (
		<Box display='flex' alignItems='center' height="12" borderColor="gray.400" borderBottomWidth="2px">
			<Box p="1" 
				textTransform='uppercase' 
				fontWeight='semibold' 
				width="200px">
				{categoryName}
			</Box>
			<Box width="30px"/>
			<Box p="1" width="200px"
				color='white.500'
				fontWeight='semibold'
				textTransform='uppercase'>
				{amount}
			</Box>
			<Box width="30px"/>
			<Box p="1" 
				textTransform='uppercase' 
				fontWeight='semibold' 
				width="150px">
				{createdAt}
			</Box>
		</Box>
	);
}
