import { AppState } from '../AppState'
import { Account } from '../models/Account'
import { Vault } from '../models/Vault'
import { VaultKeep } from '../models/VaultKeep'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account =  new Account(res.data)
      console.log(AppState.account);
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async editAccount(accountData){{
    const res = await api.put('account', accountData)
    AppState.account = new Account(res.data)
  }}
  async getAccountVaults(){
    const res = await api.get('account/vaults')
    // logger.log(res.data)
    AppState.userVaults = res.data.map( v => new Vault(v))
    // logger.log('[accountsService, userVaults]', AppState.userVaults )
  }
  async getAccountVaultKeeps(){
    const res = await api.get('account/vaultKeeps')
   AppState.vaultKeeps =  res.data.map(v => new VaultKeep(v))
  //  let bestVaults = AppState.vaultKeeps.sort(v => v.vaultId)
  console.log(AppState.vaultKeeps.length);
   const count = [];

    // for (const vaultKeep of AppState.vaultKeeps) {
    //   count.push([vaultKeep, AppState.vaultKeeps[vaultKeep]])
    // }
   
    

  //  AppState.vaultKeeps.forEach((v) => {
  //    count[v,v.vaultId] = (count[v, v.vaultId] || 0) + 1;
  //  });
   console.log(count);
  }
}

export const accountService = new AccountService()
