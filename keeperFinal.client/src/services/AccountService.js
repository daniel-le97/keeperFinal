import { AppState } from '../AppState'
import { Vault } from '../models/Vault'
import { VaultKeep } from '../models/VaultKeep'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = res.data
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  async getAccountVaults(){
    const res = await api.get('account/vaults')
    // logger.log(res.data)
    AppState.userVaults = res.data.map( v => new Vault(v))
    logger.log('[accountsService]', AppState.userVaults )
  }
  async getAccountVaultKeeps(){
    const res = await api.get('account/vaultKeeps')
   AppState.vaultKeeps =  res.data.map(v => new VaultKeep(v))
  }
}

export const accountService = new AccountService()
