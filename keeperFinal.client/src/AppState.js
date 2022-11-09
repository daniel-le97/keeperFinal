// import { EventEmitter } from '@bcwdev/auth0provider-client'
import { reactive } from 'vue'
import { EventEmitter } from './utils/EventEmitter.js';

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js).Keep[]} */
  keeps:[],
  /** @type {import('./models/Keep.js).Keep[]} */
  activeKeep: null,
  // /** @type {import('./models/Vault.js).Vault[]} */
  public:false,
  private: false,
  /** @type {import('./models/Vault.js).Vault[]} */
  userVaults:[],
  /** @type {import('./models/Vault.js).Vault[]} */
  publicVaults:[],
  /** @type {import('./models/Vault.js).Vault[]} */
  privateVaults:[],
  /** @type {import('./models/Vault.js).Vault[]} */
  vaults:[],
  /** @type {import('./models/Vault.js).Vault | null} */
  vaultPick: null,
  /** @type {import('./models/Vault.js).Vault | null} */
  activeVault: null,
  /** @type {import('./models/Account.js').Account} */
  activeProfile:null,
  vaultKeeps: [],
  keepForm:0,
  vaultForm:0,
  offset: 0,

})

// class State extends EventEmitter {
//   /** @type {import('./Models/Value').Value[]} */
//   values = []

//   // /** @type {import('./Models/Task').Task[]} */
//   // tasks = loadState("tasks", Task);

//   // /** @type {import('./Models/List').List[]} */
//   // lists = loadState("lists", List);
// }

// export const state = new Proxy(new State(), {
//   get(target, prop) {
//     isValidProp(target, prop);
//     return target[prop];
//   },
//   set(target, prop, value) {
//     isValidProp(target, prop);
//     target[prop] = value;
//     target.emit(prop, value);
//     return true;
//   },
// });
