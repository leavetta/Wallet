import {
	Button,
    Input,
    useDisclosure,
    Fade,
  } from '@chakra-ui/react'
  import { useState} from "react";


export default function OperationForm({ onCreate }) {
    const [category, setCategory] = useState("");
    const { isOpen, onToggle } = useDisclosure()

	const onSubmit = (e) => {
		e.preventDefault();
		setCategory(null);
		onCreate(category);
	};

	return (
		<form onSubmit={onSubmit} className="w-full flex flex-col gap-3">
            {/* <Text fontSize='xl'>{titleText}</Text> */}
            <Button onClick={onToggle}>Click Me</Button>
            <Fade in={isOpen} className="w-full flex flex-col gap-3">
                <Input
                    placeholder="Название категории"
                    value={category?.name ?? ""}
                    onChange={(e) => setCategory({ ...category, name: e.target.value })}
                />
                <Button type="submit" colorScheme="teal">
                    Создать
                </Button>
            </Fade>
           
        </form>
		
	);
}