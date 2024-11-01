
import { BrowserRouter as Router, Routes, Route } from "react-router-dom"
import { ChakraProvider } from "@chakra-ui/react"
import RegisterForm from "./components/RegisterForm";
import LoginForm from "./components/LoginForm";
import { loginUser, registerUser, getCurrentUser } from "./services/Users";
import HomePage from "./components/HomePage";
// import ProtectedRoute from "./components/ProtectedRoute";

function App() {
  const onRegister = async (user) => {
		let userId = await registerUser(user);
    console.log(userId);
	};

  const onLogin = async (user) => {
		let userId = await loginUser(user);
    console.log(userId);
    console.log(getCurrentUser());
	};

  return (
    <ChakraProvider>
      <div>
        <Router>
          <Routes>
              <Route exact path="/"  element={<RegisterForm onRegister={onRegister}/>} />
              <Route path="/login"  element={<LoginForm onLogin={onLogin}/>} />
              <Route path="/home"  element={<HomePage />} />
              {/* <Route element={<ProtectedRoute />}>
                <Route path="/home" element={<HomePage />} />
              </Route> */}
          </Routes>
        </Router>
      </div>
    </ChakraProvider>
			
  )
}

export default App
