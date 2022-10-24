import React, {useEffect, useState} from 'react';
import {availableRooms} from "../components/api/rooms";
import Body from "../components/Body";
import {checkLogin} from "../components/api/authorization";
import {Spinner, Button} from "react-bootstrap";

function Rooms() {
    let data = <></>
    const [loading, setLoading] = useState(true)
    const [booking, setBooking] = useState(<></>)
    useEffect(()=>{
        availableRooms().then(r=>{
            if(r.success){
                let data = r.data.map((room, index)=>{
                    return (
                        <div key={index} className = "check-room">
                            <img   alt = "room"/>
                            <h3>{room.type}</h3>
                            <p>{room.description}</p>
                            <span>{`Beds: ${room.number_of_beds},  Available: ${room.quantity}`}</span>
                            <br/>
                            <div>
                                <span className={"room-price"}>{`R${room.price}.00`}</span> / Per Night
                            </div>
                            <br/>
                            <Button  variant={"success"} onClick={()=>{
                                if(checkLogin()){
                                    let encrypt = window.btoa(JSON.stringify(room))
                                    window.location = `/make-booking?d=${encrypt}`
                                }else{
                                    Body.prototype.showToast("You need to login first in order to make a booking", "Info", "info", 10000)
                                }
                            }}>Book</Button>
                        </div>
                    )
                })
                setBooking(data)
                setLoading(false)
            }else{
                Body.prototype.showToast("Failed to load data", "connection", "error", 10000)
            }
        })
    }, [])
    return (
        <>
            <div className={"check-rooms-background"}>
            </div>
            <div>
                {loading ? <Spinner className={"text-success"} animation={"border"}/>:
                    <div className={`check-rooms-div`}>
                        <section className = "check-rooms" id = "check-rooms">
                            <div className = "sec-width">
                                <div className = "check-rooms-container">{booking}</div>
                            </div>
                        </section>
                    </div>
                }
            </div>
        </>
    );
}

export default Rooms;