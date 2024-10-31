import { Button, Input} from "@chakra-ui/react";
import { useState } from "react";
import { useNavigate } from "react-router-dom"

export default function RegisterForm({ onRegister }) {
    const [user, setUser] = useState();
    const navigate = useNavigate();

    const onSubmit = (e) => {
        e.preventDefault();
        console.log("Была нажата кнопка");
        setUser(null);
		onRegister(user);
		navigate("/login");
    };

    const onClick = (e) =>{
        e.preventDefault();
        navigate("/login");
    }

	return (
		<form onSubmit={onSubmit} className="w-full flex flex-col gap-3">
			<h3 className="font-bold text-xl">Регистрация пользователя</h3>
			<Input
				placeholder="Имя"
				value={user?.name ?? ""}
				onChange={(e) => setUser({ ...user, name: e.target.value })}
			/>
            <Input
				placeholder="Email"
				value={user?.email ?? ""}
				onChange={(e) => setUser({ ...user, email: e.target.value })}
			/>
            <Input
				placeholder="Пароль"
				value={user?.password ?? ""}
				onChange={(e) => setUser({ ...user, password: e.target.value })}
			/>
			<Button type="submit" colorScheme="teal">
				Зарегистрироваться
			</Button>

            <Button type="button" onClick={onClick} colorScheme="teal">
				На страницу входа
			</Button>
		</form>
	);
}
