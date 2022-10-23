import './App.css';
import React from "react";
import { Route, Routes, BrowserRouter as Router } from 'react-router-dom';
import Body from "./components/Body";
import AppRoutes from "./components/Routes";

function App(){
    return (
        <Body>
            <Router>
                <Routes>
                    {AppRoutes.map((route, index)=>{
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </Router>
        </Body>
    );
}


export default App;
