import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { VaultKeep } from "../models/VaultKeep";
import { api } from "./AxiosService"

class  KeepsService {
 async getAllKeeps(){
  const res = await api.get('api/keeps')
  console.log(res.data);
  let keeps = res.data.map( k => new Keep(k))
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
}
export const keepsService = new KeepsService()