import React from 'react';

function Home() {
    return (
        <div>
            <header className = "header" id = "header">
                <div className = "head-bottom flex">
                    <h2>NICE AND COMFORTABLE PLACE TO STAY</h2>
                    <p>Thembelihle Guest House is conveniently situated in the heart of Scottsville in Pietermaritzburg, the capital city of KwaZulu-Natal. We offer a perfect and convenient stay for travellers exploring local attractions, business, or to simply relax and unwind</p>
                    <button type = "button" className = "head-btn"  onClick={()=>{
                        window.location = "/rooms"
                    }}>BOOK NOW</button>
                </div>
                
            </header>
        </div>
    );
}

export default Home;