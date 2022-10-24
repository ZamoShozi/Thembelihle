import React, {useState, useEffect, useRef} from 'react';
import {Navbar, NavDropdown} from "react-bootstrap";
import {checkLogin, logout} from "../api/authorization";

function NavBar() {
    const [home, setHome] = useState("nav-link")
    const [room, setRoom] = useState("nav-link")
    const [login, setLogin] = useState("nav-link")
    const [loggedIn, setLoggedIn] = useState(false)
    useEffect(()=>{
        setLoggedIn(checkLogin)
        switch (document.location.pathname){
            case "/":{
                setHome("nav-link active")
                break
            }
            case "/login":{
                setLogin("nav-link active")
                break
            }
            case "/rooms":{
                setRoom("nav-link active")
                break
            }
        }
    }, [])
    return (
        <Navbar variant={"dark"} bg="dark" expand="lg" >
        <div className="container-fluid">
                <a className="navbar-brand" href="#">Thembelihle</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <a className={home} aria-current="page" href="./">Home</a>
                        </li>
                        <li className="nav-item">
                            <a className={room} href="./rooms">Rooms</a>
                        </li>
                        <li className="nav-item">
                            <a className={room} href="./rooms">About</a>
                        </li>
                        <li className="nav-item">
                            <a className={room} href="./rooms">Contact</a>
                        </li>
                        {!loggedIn ?
                            <li className="nav-item">
                                <a className={login} href="/login">Login</a>
                            </li>
                            :
                            <NavDropdown title="Account" id="basic-nav-dropdown">
                                <NavDropdown.Item href="/bookings">Bookings</NavDropdown.Item>
                                <NavDropdown.Item href="/profile">Profile</NavDropdown.Item>
                                <NavDropdown.Divider />
                                <NavDropdown.Item onClick={()=>{
                                    logout().then(r => {
                                        if(r.success){
                                            sessionStorage.removeItem("expiry")
                                        }
                                        setLoggedIn(false)
                                    })
                                }}>
                                    Logout
                                </NavDropdown.Item>
                            </NavDropdown>
                        }
                    </ul>
                </div>
            </div>
        </Navbar>
    );
}

export default NavBar;