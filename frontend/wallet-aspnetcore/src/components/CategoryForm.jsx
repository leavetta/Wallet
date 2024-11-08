import {
	Button,
    Input,
    // useDisclosure,
    Fade,
  } from '@chakra-ui/react'
  import { useState, useEffect} from "react";
  import { fetchCategories } from "../services/Categories";


export default function OperationForm({ onCreate, setOptions,  kindOfTransaction}) {
    const [category, setCategory] = useState("");
    // const { isOpen, onToggle, onClose } = useDisclosure();
    const [isButtonDisabled, setButtonDisabled] = useState(true);
    const [show, setShow] = useState(false);

    const handleToggle = ()=> {
        setShow(!show);
        setButtonDisabled(!isButtonDisabled);
    }

	const onSubmit = (e) => {
		e.preventDefault();
		setCategory(null);
		onCreate(category);
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
            //   console.log("Options " + options.label);
              
            });
        };
        getData();
      }, [category, kindOfTransaction, setOptions]);

	return (
		<form onSubmit={onSubmit} className="w-full flex flex-col gap-3">
            {/* <Text fontSize='xl'>{titleText}</Text> */}
            <Button onClick={handleToggle}>Создать категорию</Button>
            <Fade in={show} className="w-full flex flex-col gap-3">
                <Input
                    placeholder="Название категории"
                    value={category?.name ?? ""}
                    onChange={(e) => setCategory({ ...category, name: e.target.value })}
                />
                <Button type="submit" colorScheme="teal" disabled={isButtonDisabled}>
                    Создать
                </Button>
            </Fade>
           
        </form>
		
	);
}