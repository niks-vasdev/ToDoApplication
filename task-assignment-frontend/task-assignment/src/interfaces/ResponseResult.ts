export interface ResponseResult<T>{
    data:T,
    isSuccessful:boolean,
    error:string
}