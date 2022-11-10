import { AppState } from "../AppState";
import { Account } from "../models/Account";
import { Vault } from "../models/Vault";
import { VaultKeep } from "../models/VaultKeep";
import { logger } from "../utils/Logger";
import { api } from "./AxiosService";

class AccountService {
  async getAccount() {
    try {
      const res = await api.get("/account");
      AppState.account = new Account(res.data);
      // console.log(AppState.account);
    } catch (err) {
      logger.error("HAVE YOU STARTED YOUR SERVER YET???", err);
    }
  }
  async editAccount(accountData) {
    {
      const res = await api.put("account", accountData);
      AppState.account = new Account(res.data);
    }
  }
  async getAccountVaults() {
    const res = await api.get("account/vaults");
    // logger.log(res.data)
    let vaults = res.data.map((v) => new Vault(v));
  
    AppState.userVaults = vaults
    AppState.filterVaults = AppState.userVaults
  }
  async getAccountVaultKeeps() {
    const res = await api.get("account/vaultKeeps");
    AppState.vaultKeeps = res.data.map((v) => new VaultKeep(v));

  }
}

export const accountService = new AccountService();
