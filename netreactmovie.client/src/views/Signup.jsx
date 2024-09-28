import { useRef , useState} from 'react';
import {Link} from 'react-router-dom';
import { useStateContext } from '../contexts/ContextProvider';
import axiosClient from '../axios_client';


export default function Signup() {

  
  const url = "/Auth/register";
  const nameRef = useRef();
  const emailRef = useRef();
  const passwordRef = useRef();
  const passwordConfirmationRef = useRef();

  const {setUser,setToken} = useStateContext();
  const [error, setError] = useState(null);
 
  const registerUser = async (url,userData) => {
		try {
     	 const { data } = await axiosClient.post(url, userData);
       //  console.log("===========SUCCESS TESPONSE ========>",data);
			 setToken(data.token);
			 setUser(data.user.name);      
			// console.log("User registered successfully", data);
      
		} catch (error) {
      //  console.log("===========CATCH ERROR =============");
        //  console.log(error);  
      if("data" in error.response){
         setError(error.response.data);
      }
    //  debugger;
       throw error.response;;
		}
	};

  const onSubmit = (e) => {

    setError(null);
    e.preventDefault();

    const payload ={
      name: nameRef.current.value,
      email: emailRef.current.value,
      password: passwordRef.current.value,
      password_confirmation: passwordConfirmationRef.current.value
    }

      registerUser(url, payload);
  }
return (
	<div className="login-signup-form animated fadeInDown">
		<div className="form">
			<h1 className="title">Create an account</h1>
			{error && (
				<div className="alert">
					{error.map((item, index) => (
						<p key={index}>{item.description}</p>
					))}
				</div>
			)}
			<form onSubmit={onSubmit}>
				<input ref={nameRef} type="text" placeholder="Full name" />
				<input ref={emailRef} type="text" placeholder="Email" />
				<input ref={passwordRef} type="password" placeholder="Password" />
				<input
					ref={passwordConfirmationRef}
					type="password"
					placeholder="Cornfirm Password"
				/>
				<button type="submit" className="btn btn-block">
					Signup
				</button>
				<p className="message">
					Already registered? <Link to="/login">Login </Link>
				</p>
			</form>
		</div>
	</div>
);
}
