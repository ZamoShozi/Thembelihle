import './App.css';
import React, { Component } from "react";
import { Route, Routes, BrowserRouter as Router } from 'react-router-dom';

import {Layout} from "./components/Layout";
import Login from "./screens/Login";
import Home from "./screens/Home";
import AppRoutes from "./components/Routes";

function App(){
    return (
        <Layout>
            <Router>
                <Routes>
                    {AppRoutes.map((route, index)=>{
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </Router>
        </Layout>
    );
}


export default App;
