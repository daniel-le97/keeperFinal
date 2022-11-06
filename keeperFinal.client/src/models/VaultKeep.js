export class VaultKeep{
  constructor(data){
    this.id = data.id
    this.keepId = data.keepId
    this.vaultId = data.vaultId
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
    this.creatorId = data.creatorId
  }
}