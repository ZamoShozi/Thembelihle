import React,{useState, useRef, useEffect} from 'react';
import {login} from "../components/api/authorization";
import {Spinner} from "react-bootstrap";
import Body from "../components/Body";

function Login() {
    const [password, setPassword] = useState("")
    const [email, setEmail] = useState("")
    const [loading, setLoading] = useState(false)
    const [disable, setDisable] = useState(false)
    return (
        <div className={"login-page"}>
                <div className={"login-form"}>
                    <h3>Login Page</h3>
                <div className="form-floating mb-3">
                    <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" onChange={
                        (text)=>{
                        setEmail(text.target.value)
                    }
                    }/>
                        <label htmlFor="floatingInput">Email address</label>
                </div>
                <div className="form-floating">
                    <input type="password" className="form-control" id="floatingPassword" placeholder="Password" onChange={
                        (text)=>{
                            setPassword(text.target.value)
                        }
                    }/>
                    <label htmlFor="floatingPassword">Password</label>
                </div>
                    {loading ? <Spinner className={"text-success"} animation={"border"}/> :
                        <button disabled={disable} className={"btn btn-outline-success"} onClick={()=>{
                            setLoading(true)
                            login(email, password).then(data =>{
                                if(data["success"]){
                                    Body.prototype.showToast(data["message"], "Login Status", "success", 1500)
                                    sessionStorage.setItem("expiry", `${new Date().getTime() + (60000*30)}`)
                                    setDisable(true)
                                    setTimeout(()=>{
                                        window.location = "/"
                                    }, 1500)
                                }else{
                                    Body.prototype.showToast(data["message"], "Login Status", "error", 5000)
                                }
                                setLoading(false)
                            })
                        }}>Login</button>
                    }
                <div className={"login-more"}>
                    <a href="/reset-password" className="card-link">Reset Password</a>
                    <a href="/register" className="card-link">Register Account</a>
                </div>
            </div>
        </div>
    );
}

export default Login;