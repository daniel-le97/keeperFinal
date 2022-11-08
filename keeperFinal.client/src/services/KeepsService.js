import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { VaultKeep } from "../models/VaultKeep";
import { api } from "./AxiosService"

class  KeepsService {
 async getAllKeeps(offset){
  const res = await api.get('api/keeps',{
    params:{
      offset: offset
    }
  })
  console.log(res.data);
  let keeps = res.data.map( k => new Keep(k))
  AppState.offset += keeps.length
  console.log(AppState.offset);
  AppState.keeps = [...AppState.keeps, ...keeps]
 }
  async saveKeep(vaultKeep){
    const res = await api.post('api/vaultKeeps', vaultKeep)
    // console.log(res.data);
    AppState.vaultKeeps = [...AppState.vaultKeeps, new VaultKeep(res.data)]
    AppState.activeKeep.kept ++
  }

  async getMatchingVaultKeep(vaultId, keepId){
    console.log(vaultId, keepId);
  }
  async getKeepById(keepId){
    const res = await api.get(`api/keeps/${keepId}`)
    // console.log(res.data);
    AppState.activeKeep.views++
    
  }
  async createKeep(keepData){
    const res = await api.post('api/keeps', keepData)
    const keep = new Keep(res.data)
    AppState.keeps = [keep, ...AppState.keeps]
    return keep
  }
  async deleteKeep(keepId){
    const res =  await api.delete(`api/keeps/${keepId}`)
    console.log(res.data);
    AppState.activeKeep = null
    let index = AppState.keeps.findIndex( k  => k.id == keepId)
    AppState.keeps.splice(index, 1)

  }
}
export const keepsService = new KeepsService()