import api from "../API";

export const accountDetails = async () =>{
    return await api.get("account").then(r => r.data)
}