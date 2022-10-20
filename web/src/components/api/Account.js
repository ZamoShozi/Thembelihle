import api from "../API";

export const AccountDetails = async () =>{
    return await api.get("account").then(r => r.data)
}