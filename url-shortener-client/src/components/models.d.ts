interface DisplayUrl {
    id:string,
    baseUrl:string,
    shorteredUrl:string
}

interface DetailsUrl {
    id:string
    baseUrl:string,
    shorteredUrl:string,
    modifiedDate: string,
    creator: DisplayUser
}

interface DisplayUser {
    username: string
}

interface DetailsUser {
    username: string
    role:number
}

interface DisplayAlhoritmDescriptionVM {
    description: string
}

interface UpdateDescriptionRequest {
    description:string
}

interface AlhoritmInfo {
    id:string,
    name:string,
    description:string
}