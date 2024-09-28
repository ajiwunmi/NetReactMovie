import { useRef , useState, useEffect } from 'react';
import {useNavigate, useParams} from "react-router-dom";
import { useStateContext } from '../contexts/ContextProvider';
import axiosClient from '../axios_client';



export default function UserForm() {

   const Navigate = useNavigate();
  const {id} = useParams();
  const [error, setError] = useState(null);
  const {loading,setLoading} = useState(false);
  const {setNotification} = useStateContext();

  const [user, setUser] = useState({
    id:null,
    name: '',
    email: '',
    password: '',
    password_confirmation :''
  });

    if(id){
      useEffect(()=>{
       axiosClient.get(`/users/${id}`)
      .then(({data})=>{
        // debugger;
       // console.log(data);
        setUser(data)
        setLoading(false);

      }).catch((err)=>{
        console.log(err);
        // setLoading(false);
      });
    },[])
  }



  const onSubmit = (e) => {
     e.preventDefault();
    setError(null);

   // console.log(user)

    if(user.id){
     // console.log(user);
      // debugger;
      axiosClient.put(`/users/${user.id}`, user)
        .then(()=>{
          //console.log(data);
          setNotification("User updated successfully");
          Navigate("/users")
        }).catch(error =>{
          const response = error.response;
          if(response && response.status === 422){
            //console.log(response.data.errors);
            setError(response.data.errors);
          }
        });
    }else{

      axiosClient.post('/users', user)
        .then(({data})=> {
        //console.log(data);
        setNotification("User created successfully");
         Navigate("/users")
        // setUser(data.user);

      })
      .catch(error => {
       // console.log(response.data.errors);
        const response = error.response;
        if(response && response.status === 422){
          //console.log(response.data.errors);
          setError(response.data.errors);
        }
      });

       // console.log("============end create user==================");

    }

}


return (
    <div className="card animated fadeInDown">
      <div className="form">
      <h1 className="title">{user.id && `Update user ${user.name}`}{!user.id && 'Create New User'}</h1>
        {
          error && <div  className='alert'>
          {Object.keys(error).map((key, index) => ( <p key={index}>{error[key]}</p>))}
          </div>
        }
        {loading &&
          <div className="alert">
            Loading...
          </div>
        }
        {!loading &&
        <form onSubmit={onSubmit}>
          <input value={user.name}  onChange={e =>setUser({...user, name : e.target.value})} type="text" placeholder="Full name" />
          <input value={user.email}  onChange={ e =>setUser({...user, email : e.target.value})}type="text" placeholder="Email" />
          <input   onChange={e=>setUser({...user, password :e.target.value})} type="password" placeholder="Password" />
          <input  onChange={e=>setUser({...user, password_confirmation:e.target.value})} type="password" placeholder="Cornfirm Password" />
          <button type="submit" className="btn">Save</button>
        </form>
       }
      </div>
    </div>
  )
}
