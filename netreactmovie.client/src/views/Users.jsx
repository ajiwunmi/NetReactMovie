import {useState, useEffect} from 'react';
import axiosClient from '../axios_client';
import { Link } from 'react-router-dom';

export default function Users() {
  const userStyle = {
    display: 'flex',
    justifyContent: "space-between",
    alignItems: 'center',
   
  }

  const [users, setUsers] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(()=>{
    getUsers();
  }, []);

  const getUsers = ()=>{
    setLoading(true);
    axiosClient.get('/users')
    .then(({data})=>{
      // console.log(data);
      setUsers(data.data)
      setLoading(false);
    }).catch((err)=>{
      console.log(err);
      setLoading(false);
    });
  }

  const onDeleteClick =(user)=>{
    if(!window.confirm("Are you sure you want to delete this user?")){
      return;
    }
    axiosClient.delete(`/users/${user.id}`)
    .then(()=>{
      getUsers();
    }).catch((err)=>{
      console.log(err);
    })  
  }


  return (
    <>
      <div style={userStyle}>          
        <h1>Users</h1>
        <Link to="/users/new" className='btn-add'>New User</Link>
     </div> 
      <div className='card animated fadeInDown'>
        <table>
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Email</th>
              <th>Created At</th>
              <th>Action</th>
            </tr>
          </thead>
             {loading &&
            <tbody>
            <tr>
              <td colSpan="5" className="text-center">
                Loading...
              </td>
            </tr>
            </tbody>
          }
          {!loading &&
          <tbody>
            {users.map((user)=>(
              <tr key={user.id}>
                <td>{user.id}</td>
                <td>{user.name}</td>
                <td>{user.email}</td>
                <td>{user.created_at}</td>
                <td>
                  <Link to={`/users/${user.id}`} className='btn-edit'>Edit</Link>&nbsp;
                  <button onClick={ e => onDeleteClick(user)} className='btn-delete'>Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
          }
        </table>     
      
    </div>
    </>
  )
}
