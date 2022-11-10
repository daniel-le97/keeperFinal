import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { Vault } from "../models/Vault";
import { router } from "../router";
import Pop from "../utils/Pop";
import { api } from "./AxiosService";

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`);
    console.log(res.data);
    AppState.activeVault = new Vault(res.data);
  }
  async getKeepsInVault(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`);
    console.log(res.data);
    AppState.keeps = res.data.map((k) => new Keep(k));

    // console.log(AppState.keeps, "[vaultsService]");
  }
  async createVault(vaultData) {
    const res = await api.post("api/vaults", vaultData);
    // console.log(res.data);
    const vault = new Vault(res.data);
    AppState.vaults = [...AppState.vaults, vault];
    AppState.private = false
    AppState.public = false
    AppState.filterVaults = AppState.userVaults
    AppState.userVaults = [...AppState.userVaults,vault]
    
    // AppState.filterVaults = [...AppState.filterVaults, vault];
    return vault;
  }
  async deleteVault(vaultId) {
    const res = await api.delete(`api/vaults/${vaultId}`);
    // console.log(res.data);
    AppState.activeVault = null;
    AppState.userVaults = AppState.userVaults.filter((v) => v.id != vaultId);
    AppState.filterVaults = AppState.filterVaults.filter(
      (v) => v.id != vaultId
    );

    AppState.vaultKeeps = AppState.vaultKeeps.filter(
      (v) => v.vaultId != vaultId
    );
  }
  async deleteVaultKeep(vaultKeepId) {
    const res = await api.delete(`api/vaultKeeps/${vaultKeepId}`);
    // console.log(res.data);
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
