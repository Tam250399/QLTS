export interface IBaseRequest<T>{
    url: string
    data?: T
    params?: T
    method: "GET" | "POST" | "PUT" | "PATCH" | "DELETE"
}

export interface IBaseResponse<T, P = null> {
    status: number
    statusCode?: number
    message?: string
    data?: T | null
    error?: string
    pagination?: P | null
}

export interface IPaginationResponse {
    page: number
    limit: number
    total_items: number
    total_pages: number
}
