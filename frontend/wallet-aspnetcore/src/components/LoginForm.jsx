import { Button, Input} from "@chakra-ui/react";
import { useState } from "react";
import { useNavigate } from "react-router-dom"


export default function LoginForm({ onLogin }) {
    const [user, setUser] = useState();
    const navigate = useNavigate();


    const onSubmit = (e) => {
        e.preventDefault();
        console.log("Была нажата кнопка");
        setUser(null);
		onLogin(user);
        navigate("/home");
    };

	return (
		<form onSubmit={onSubmit} className="w-full flex flex-col gap-3">
			<h3 className="font-bold text-xl">Вход</h3>
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
				Войти
			</Button>

		</form>
	);
}