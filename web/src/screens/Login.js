import React,{useState, useRef, useEffect} from 'react';
import {login} from "../components/api/Authorization";
import {AccountDetails} from "../components/api/Account";
function Login() {
    const [password, setPassword] = useState("")
    const [email, setEmail] = useState("")
    return (
        <div className={"login-page"}>
            <div>
                <input type={"email"} placeholder={"Enter Email"} onChange={(text)=>{
                    setEmail(text.target.value)
                }}/>
                <input type={"password"} placeholder={"Enter password"} onChange={(text)=>{
                    setPassword(text.target.value)
                }}/>
                <button onClick={()=>{
                    login(email, password).then(data =>{
                        if(data["success"]){
                            alert("Login successful")
                        }else{
                            alert(data["message"])
                        }
                    })
                }}>Login</button>
                <button onClick={
                    ()=>{
                        AccountDetails().then(data => {
                            if(data["success"]){
                                alert(data["message"])
                            }else{
                                alert(data["message"])
                            }
                        })
                    }
                }>Details</button>
            </div>
        </div>
    );
}

export default Login;