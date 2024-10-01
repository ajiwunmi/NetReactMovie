import { createContext, useContext, useState } from "react";
import PropTypes from 'prop-types';

const StateContext = createContext({
    user: null,
    token: null,
    setUser: () => {},
    setToken: () => {},
    notification :'',
    setNotification : () => {},
  });


export const ContextProvider = ({children})=>{

  const [user, setUser] = useState(null);
  const [notification, _setNotification] = useState(null);
  const [token, _setToken] = useState(localStorage.getItem("ACCESS_TOKEN"));


  const setToken = (token) => {
    _setToken(token);
    if(token){
      localStorage.setItem("ACCESS_TOKEN", token);
    }else{
      localStorage.removeItem("ACCESS_TOKEN")
    }

  }
  const setNotification = (message) => {
    _setNotification(message);
    setTimeout(()=>{
      _setNotification(null);
    }, 3000);
  }

  return(
    <StateContext.Provider value={{
      user,
      token,
      setUser,
      setToken,
      notification,
      setNotification
    }}>
      {children}
    </StateContext.Provider>
  );

}

ContextProvider.propTypes = {
  children: PropTypes.node.isRequired,
};

export const useStateContext = () => useContext(StateContext);
