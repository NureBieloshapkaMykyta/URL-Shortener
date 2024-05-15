import axios, { AxiosResponse } from "axios";

axios.defaults.baseURL = 'https://localhost:7239'

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody)
}

export const roles = ["Admin", "User"];

export const jwt = () => localStorage.getItem("jwt");

export const setJwt = (token: string) => {
    axios.defaults.headers.common['Authorization'] = 'Bearer ' + token; 
    localStorage.setItem("jwt", token)
} 

export const account = {
    details: () => requests.get("/Account"),
    login: (data: LoginVm) => requests.post("/Account/Login", data),
    signUp: (data: SignUpVm) => requests.post("/Account/Register", data),
    logout: () => requests.get("/Account/Logout")
}

export const url = {
    create: (data: CreateUrl) => requests.post("/Url", data),
    get: () => requests.get("/Url"),
    getById: (id: string) => requests.get("/Url/" + id),
    getFullUrl: (code:string) => requests.get("/Url/full/" + code),
    permissionToDelete: (id:string) => requests.get("/Url/CheckDeletePermissions/"+id),
    delete: (id: string) => requests.delete("/Url/" + id)
}

export const about = {
    get: () => requests.get("/About"),
    update: (id:string, data: UpdateDescriptionRequest) => requests.put("/About/"+id, data),
}