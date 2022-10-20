import api from "../API";

export const login = async (email, password) =>{
    return await api.post("authorization/login", JSON.stringify({email:email, password:password}), {
        headers:{
            "Content-Type":"application/json"
        }
    }).then(r => r.data)
}