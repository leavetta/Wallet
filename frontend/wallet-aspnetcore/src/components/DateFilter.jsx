import {
	Button,
    Fade,
  } from '@chakra-ui/react'
  import { useState} from "react";
  import DatePicker from "react-date-picker";
  import 'react-date-picker/dist/DatePicker.css';
  import 'react-calendar/dist/Calendar.css';

export default function OperationForm({ startDate, setStartDate, endDate, setEndDate }) {
    const [isButtonDisabled, setButtonDisabled] = useState(true);
    const [show, setShow] = useState(false);

    const handleToggle = ()=> {
        setShow(!show);
        setButtonDisabled(!isButtonDisabled);
    }

    
    // useEffect(() => {
    //     const getData = async () => {
          
          
    //     };
    //     getData();
    // }, []);

    // const onClick = ()=> {
    //     console.log(startDate);
    //     console.log(endDate);
    //     setStartDate(null);
    //     setEndDate(null);
    //     handleToggle();
    // }

	return (
		<div className="w-full flex flex-col gap-3">
            {/* <Text fontSize='xl'>{titleText}</Text> */}
            <Button onClick={handleToggle}>Выбрать промежуток</Button>
            <Fade in={show} className=" flex flex-col gap-3">
                <div className='flex flex-row'>
                    <DatePicker 
                        value={startDate}
                        onChange={(startDate) => setStartDate(startDate)}  />
                    
                    
                    <DatePicker 
                        value={endDate}
                        onChange={(endDate) => setEndDate(endDate)}  />
                    {/* <Button type="button" onClick={onClick} colorScheme="teal" disabled={isButtonDisabled}>
                        Применить
                    </Button>                 */}
                </div>
                
                
            </Fade>
           
        </div>
		
	);
}