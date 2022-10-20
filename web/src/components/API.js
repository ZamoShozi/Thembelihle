import axios from "axios";

const baseURL = "https://localhost:7109/api";

const api = axios.create({
    baseURL: baseURL,
    withCredentials: true
});

export default api;