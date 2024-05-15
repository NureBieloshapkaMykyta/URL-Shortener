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