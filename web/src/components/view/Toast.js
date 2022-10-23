import React from 'react';
import Toast from 'react-bootstrap/Toast';
import {ToastContainer} from "react-bootstrap";
import info from "../../images/info.png"
import error from "../../images/error.png"
import warning from "../../images/warning.png"
import success from "../../images/success.png"


function CustomToast({toast, onClose}) {
    const about = {"error":{"icon":error, variant:"danger"}, "success":{"icon":success, variant:"success"},
        "warning":{"icon":warning, variant:"warning"}, "info": {"icon":info, variant:"info"}}
    const variant = about[toast.about] ?? {"icon":info, variant:"info"}
    console.log(variant, toast.about)
    return (
        <ToastContainer className="p-3"  position={"bottom-end"}>
            <Toast className="d-inline-block m-1" bg={variant.variant} onClose={() => onClose()} delay={3000}>
                <Toast.Header>
                    <img src={variant.icon} className="rounded me-2" alt="" />
                    <strong className="me-auto">{toast.type}</strong>
                    <small>{new Date().toLocaleString().replaceAll("/", "-")}</small>
                </Toast.Header>
                <Toast.Body className={variant.variant !== "info" && "text-white"}>{toast.message}</Toast.Body>
            </Toast>
        </ToastContainer>
    );
}

export default  CustomToast;