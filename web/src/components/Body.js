import React, { useState} from 'react';
import NavBar from "./view/NavBar";
import CustomToast from "./view/Toast";

export default function Body(props) {
    const [toast, setToast] = useState({})
    Body.prototype.showToast = function (message, type, about, time) {
        setToast({message:message, type:type, about:about, time:time})
        setTimeout(()=>{
            setToast({})
        }, time)
    }
    return (
        <>
        <NavBar />
        <body>
            {Object.entries(toast).length > 0? <CustomToast toast={toast} onClose={()=>{
                setToast({})
            }}/> : <></>}
            {props.children}
        </body>
        </>
    );
}
