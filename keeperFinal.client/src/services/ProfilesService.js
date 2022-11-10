import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { Profile } from "../models/Profile";
import { Vault } from "../models/Vault";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService"

class  ProfilesService {
  async getProfile(id){
    const res =  await api.get('api/profiles/' + id)
    // console.log(res.data);
    AppState.activeProfile = new Profile(res.data)
  }
  async getProfileKeeps(id){
    const res = await api.get(`api/profiles/${id}/keeps`)
    // console.log(res.data);
   AppState.keeps = res.data.map(k => new Keep(k))
    // logger.log('[profile, keeps]', AppState.keeps)
  }
  async getProfileVaults(id){
    const res = await api.get(`api/profiles/${id}/vaults`)
    console.log(res.data);
   
    let vaults = res.data.map(v => new Vault(v))
    
    AppState.vaults = vaults
    logger.log(AppState.vaults)
  }
  // 
}
export const profilesService = new ProfilesService()