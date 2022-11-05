import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Vault.js).Vault[]} */
  keeps:[],
  /** @type {import('./models/Vault.js).Vault | null} */
  activeKeep: null,
  /** @type {import('./models/Keep.js).Keep[]} */
  vaults:[]
})
