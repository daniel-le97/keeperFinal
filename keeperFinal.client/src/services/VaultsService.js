import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { Vault } from "../models/Vault";
import { api } from "./AxiosService";

class VaultsService {
  async getVaultById(vaultId){
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
  }
  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    AppState.keeps = res.data.map((k) => new Keep(k));
    console.log(AppState.keeps, '[vaultsService]');
  }
  async createVault(vaultData){
    const res =  await api.post('api/vaults', vaultData)
    console.log(res.data)
    const vault = new Vault(res.data)
    return vault
  }
  //
}
export const vaultsService = new VaultsService();
