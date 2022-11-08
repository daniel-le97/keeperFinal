import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { Vault } from "../models/Vault";
import { router } from "../router";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";

class VaultsService {
  async getVaultById(vaultId) {
    // try {
    // } catch (error) {
    //   // console.log(error);
    //   router.push({name: 'Home'})
    // }
    const res = await api.get(`api/vaults/${vaultId}`);
    AppState.activeVault = new Vault(res.data);
    console.log(res.data);
    // if (!res) {
    //   router.push({name: 'Home'})
    // }
  }
  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    console.log(res.data);
    AppState.keeps = res.data.map((k) => new Keep(k));

    console.log(AppState.keeps, "[vaultsService]");
  }
  async createVault(vaultData) {
    const res = await api.post("api/vaults", vaultData);
    console.log(res.data);
    const vault = new Vault(res.data);
    return vault;
  }
  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`);
    console.log(res.data);
    AppState.activeVault = null;
    AppState.userVaults = AppState.userVaults.filter((v) => v.id != vaultId);
    AppState.vaultKeeps = AppState.vaultKeeps.filter((v) => v.vaultId != vaultId);
  }
  async deleteVaultKeep(vaultKeepId) {
    const res = await api.delete(`api/vaultKeeps/${vaultKeepId}`);
    console.log(res.data);
    AppState.keeps = AppState.keeps.filter((k) => k.vaultKeepId != vaultKeepId);
  }
  async editVault(vaultData) {
    delete vaultData.createdAt;
    delete vaultData.updatedAt;
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData);
    // console.log(res.data);
    AppState.activeVault = new Vault(res.data);
  }
  //
}
export const vaultsService = new VaultsService();
