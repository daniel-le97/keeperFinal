import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { VaultKeep } from "../models/VaultKeep";
import { api } from "./AxiosService";

class KeepsService {
  async getAllKeeps(offset) {
    const res = await api.get("api/keeps", {
      params: {
        offset: offset,
      },
    });
    console.log(res.data);
    let keeps = res.data.map((k) => new Keep(k));
    AppState.offset += keeps.length;
    console.log(AppState.offset);
    AppState.keeps = [...AppState.keeps, ...keeps];
  }
  async saveKeep(vaultKeep) {
    const res = await api.post("api/vaultKeeps", vaultKeep);
    // console.log(res.data);
    AppState.vaultKeeps = [...AppState.vaultKeeps, new VaultKeep(res.data)];
    AppState.activeKeep.kept++;
  }

  async getMatchingVaultKeep(vaultId, keepId) {
    console.log(vaultId, keepId);
  }
  async getKeepById(keep) {
    // console.log(keep);
    let vaultKeepId = keep.vaultKeepId
  
    const res = await api.get(`api/keeps/${keep.id}`);
    // console.log(res.data);
    let newKeep = new Keep(res.data)
    AppState.activeKeep = newKeep

    if (vaultKeepId) {
      AppState.activeKeep.vaultKeepId = vaultKeepId
    }
    // console.log(AppState.activeKeep);

    for (const v of AppState.userVaults) {
      let hi = AppState.vaultKeeps.find(
        (vk) => vk.vaultId == v.id && vk.keepId == keep.id
      );

      if (hi) {
        v.keeper = true;
      } else {
        v.keeper = false;
      }
    }

  
  }
  async createKeep(keepData) {
    const res = await api.post("api/keeps", keepData);
    const keep = new Keep(res.data);
    AppState.keeps = [keep, ...AppState.keeps];
    return keep;
  }
  async deleteKeep(keepId) {
    const res = await api.delete(`api/keeps/${keepId}`);
    console.log(res.data);
    AppState.activeKeep = null;
    let index = AppState.keeps.findIndex((k) => k.id == keepId);
    AppState.keeps.splice(index, 1);
  }
}
export const keepsService = new KeepsService();
