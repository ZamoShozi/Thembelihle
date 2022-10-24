import api from "../API";

export const availableRooms = async () =>{
   return await api.get("/Rooms").then(data => data.data)
}