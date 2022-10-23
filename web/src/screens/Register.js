import React,{useState, useRef, useEffect} from 'react';
import {register} from "../components/api/Authorization";

function Register() {
    const [password, setPassword] = useState("")
    const [passwordC, setPasswordC] = useState("")
    const [name, setName] = useState("")
    const [surname, setSurname] = useState("")
    const [phone, setPhone] = useState("")
    const [email, setEmail] = useState("")
    const [state, setState] = useState("")
    const [country, setCountry] = useState("")
    const [city, setCity] = useState("")
    const [zip, setZip] = useState("")
    const [valid, setValid] = useState(false)
    useEffect(()=>{
        if(passwordC === passwordC){
            setValid(true)
        }
    }, [password, passwordC, name, surname, phone, email, state, country, city, zip])
    return (
        <div className={"register-page"}>
            <div className={"register-form"}>
                <h3>Registration Page</h3>
                <h5>Contact Details</h5>
                <div className={"name-surname"}>
                    <div className="form-floating mb-3">
                        <input type="text" value={name} className="form-control" id="floatingInputName" placeholder="Brighten" onChange={
                            (text)=>{
                                setName(text.target.value)
                            }
                        }/>
                        <label htmlFor="floatingInputName">Enter Name</label>
                    </div>
                    <div className="form-floating">
                        <input type="password" value={surname} className="form-control" id="floatingInputSurname" placeholder="Shozi" onChange={
                            (text)=>{
                                setSurname(text.target.value)
                            }
                        }/>
                        <label htmlFor="floatingInputSurname">Enter Surname</label>
                    </div>
                </div>
                <div className="input-group mb-3">
                    <span className="input-group-text">+27</span>
                    <input type="number" className="form-control" aria-label="Phone Number" value={phone}  onChange={text=>{
                        if(phone.length < 10 || phone.length > text.target.value.length){
                            setPhone(text.target.value)
                        }
                    }} aria-multiline={false}/>
                </div>
                <div className="form-floating mb-3">
                    <input type="email" className="form-control" id="floatingInput" placeholder="name@example.com" onChange={
                        (text)=>{
                            setEmail(text.target.value)
                        }
                    }/>
                    <label htmlFor="floatingInput">Email address</label>
                </div>
                <h5>Address</h5>
                <div className={"name-surname"}>
                    <div className="form-floating mb-3">
                        <input type="text" value={country} className="form-control" id="floatingInputCounty" placeholder="Country" onChange={
                            (text)=>{
                                setCountry(text.target.value)
                            }
                        }/>
                        <label htmlFor="floatingInputCounty">Country</label>
                    </div>
                    <div className="form-floating">
                        <input type="text" value={state} className="form-control" id="floatingInputState" placeholder="State" onChange={
                            (text)=>{
                                setState(text.target.value)
                            }
                        }/>
                        <label htmlFor="floatingInputState">State</label>
                    </div>
                </div>
                <div className={"name-surname"}>
                    <div className="form-floating mb-3">
                        <input type="text" value={city} className="form-control" id="floatingInputCity" placeholder="City" onChange={
                            (text)=>{
                                setCity(text.target.value)
                            }
                        }/>
                        <label htmlFor="floatingInputCity">City</label>
                    </div>
                    <div className="form-floating">
                        <input type="number" value={zip} className="form-control" id="floatingInputZip" placeholder="zip code" onChange={
                            (text)=>{
                                setZip(text.target.value)
                            }
                        }/>
                        <label htmlFor="floatingInputZip">Zip Code</label>
                    </div>
                </div>
                <div className="form-floating mb-3">
                    <input type="password" value={password} className="form-control" id="floatingPassword" placeholder="Password" onChange={
                        (text)=>{
                            setPassword(text.target.value)
                        }
                    }/>
                    <label htmlFor="floatingPassword">Password</label>
                </div>
                <div className="form-floating mb-3">
                    <input type="password" value={passwordC} className="form-control" id="floatingPassword" placeholder="Confirm Password" onChange={
                        (text)=>{
                            setPasswordC(text.target.value)
                        }
                    }/>
                    <label htmlFor="floatingPassword">Confirm Password</label>
                </div>
                <button className={"btn btn-outline-success"} onClick={()=>{
                    setPhone("+27"+phone)
                    if(valid){
                        register(name, surname, phone, email, password, passwordC, country, state, city, zip).then(data =>{
                            if(data["success"]){
                                alert("Register successful")
                            }else{
                                alert(data["message"])
                            }
                        })
                    }else{
                        alert("please complete a valid form")
                    }
                }}>Register</button>
                <a href="/login" className="card-link">Login</a>
            </div>
        </div>
    );
}

export default Register;