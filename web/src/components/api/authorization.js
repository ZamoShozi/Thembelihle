import api from "../API";

export const login = async (email, password) =>{
    return await api.post("authorization/login", JSON.stringify({email:email, password:password}), {
        headers:{
            "Content-Type":"application/json"
        }
    }).then(r => r.data)
}

export const logout = async () =>{
    return await api.get("authorization/logout").then(r => r.data)
}

export const register = async (name, surname, phone, email, password, passwordC, country, state, city, zip) =>{
    return await api.post("authorization/register", JSON.stringify({name:name, surname:surname, phoneNumber:phone, 
        email:email, password:password, passwordC:passwordC, address:{country:country, state:state, city:city, zip:zip}}), {
        headers:{
            "Content-Type":"application/json"
        }
    }).then(r => r.data)
}
export const checkLogin = () =>{
    if(sessionStorage.getItem("expiry") !== null){
        if(new Date(Number.parseInt(sessionStorage.getItem("expiry")))  > new Date()){
            return true
        }
    }
    return false
}