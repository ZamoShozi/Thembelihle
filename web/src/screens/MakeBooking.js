import React, {useEffect, useState} from 'react';
import {Button, Col, Form, Row, Spinner} from "react-bootstrap";
import Body from "../components/Body";
import {checkLogin} from "../components/api/authorization";
import {Container} from "reactstrap";

function MakeBooking() {
    if(!checkLogin()){
        window.location = "/login"
    }
    const queryParameters = new URLSearchParams(window.location.search)
    const [room, setRoom] = useState({})
    const d = queryParameters.get("d")
    useEffect(()=>{
        if(d != null){
            try {
                let jsonString = window.atob(d)
                setRoom(JSON.parse(jsonString))
            }catch (e){
                Body.prototype.showToast("Invalid url", "error", "error", 1000*60*30)
            }
        }
    }, [d])
    return (
        <div className={"make-booking"}>
            {Object.entries(room).length === 0 ? <Spinner className={"text-success"} animation={"border"}/>
                :
                <Container className={"make-booking-form"}>
                    <Form>
                        <Row className="mb-3">
                            <Form.Group as={Col} controlId="formGridEmail">
                                <Form.Label>Email</Form.Label>
                                <Form.Control type="email" placeholder="Enter email" />
                            </Form.Group>

                            <Form.Group as={Col} controlId="formGridPassword">
                                <Form.Label>Password</Form.Label>
                                <Form.Control type="password" placeholder="Password" />
                            </Form.Group>
                        </Row>

                        <Form.Group className="mb-3" controlId="formGridAddress1">
                            <Form.Label>Address</Form.Label>
                            <Form.Control placeholder="1234 Main St" />
                        </Form.Group>

                        <Form.Group className="mb-3" controlId="formGridAddress2">
                            <Form.Label>Address 2</Form.Label>
                            <Form.Control placeholder="Apartment, studio, or floor" />
                        </Form.Group>

                        <Row className="mb-3">
                            <Form.Group as={Col} controlId="formGridCity">
                                <Form.Label>City</Form.Label>
                                <Form.Control />
                            </Form.Group>

                            <Form.Group as={Col} controlId="formGridState">
                                <Form.Label>State</Form.Label>
                                <Form.Select defaultValue="Choose...">
                                    <option>Choose...</option>
                                    <option>...</option>
                                </Form.Select>
                            </Form.Group>

                            <Form.Group as={Col} controlId="formGridZip">
                                <Form.Label>Zip</Form.Label>
                                <Form.Control />
                            </Form.Group>
                        </Row>

                        <Form.Group className="mb-3" id="formGridCheckbox">
                            <Form.Check type="checkbox" label="Check me out" />
                        </Form.Group>
                        <Button variant="primary" type="submit">
                            Submit
                        </Button>
                    </Form>
                </Container>
            }
        </div>
    );
}

export default MakeBooking;