import { setTransitionHooks } from "vue";
import { Keep } from "./Keep";

export class VaultKeep {
  constructor(data) {
    this.id = data.id;
    this.vaultId = data.vaultId;
    this.keepId = data.keepId;
    this.keep = new Keep(data.keep)
    this.createdAt = data.createdAt;
    this.updatedAt = data.updatedAt;
    this.creatorId = data.creatorId;
  }
}
