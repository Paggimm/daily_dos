namespace DailyDos.Generated

[<CLIMutable>]
type LoginResponse = {
    token: string
}

[<CLIMutable>]
type User = {
    id: int
    name: string
}

[<CLIMutable>]
type RegisterData = {
    name: string
    email: string
    password: string
}
