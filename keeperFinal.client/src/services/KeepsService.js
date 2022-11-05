import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService"

class  KeepsService {
 async getAllKeeps(){
  const res = await api.get('api/keeps')
  console.log(res.data);
  let keeps = res.data.map( k => new Keep(k))
  AppState.keeps = [...AppState.keeps, ...keeps]
 }
  
}
export const keepsService = new KeepsService()