import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { Vault } from "../models/Vault";
import { router } from "../router";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";

class VaultsService {
  async getVaultById(vaultId){
    try {
      const res = await api.get(`api/vaults/${vaultId}`)
      AppState.activeVault = new Vault(res.data)
      } catch (error) {
        console.log(error);
        router.push({name: 'Home'})
      }
    // console.log(res.data);
    // if (!res) {
    //   router.push({name: 'Home'})
    // }
  }
  async getKeepsInVault(vaultId) {
    try {
      const res = await api.get(`api/vaults/${vaultId}/keeps`);
      AppState.keeps = res.data.map((k) => new Keep(k));
    } catch (error) {
      router.push({name: 'Home'})
    }
    // console.log(AppState.keeps, '[vaultsService]');
  }
  async createVault(vaultData){
    const res =  await api.post('api/vaults', vaultData)
    console.log(res.data)
    const vault = new Vault(res.data)
    return vault
  }
  async deleteVault(vaultId){
    const res  =  await api.delete(`api/vaults${vaultId}`)
    
  }
  //
}
export const vaultsService = new VaultsService();
