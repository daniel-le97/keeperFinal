export class Vault{
  constructor(data){
    this.id = data.id
    this.name = data.name
    this.description = data.description
    this.coverImg = data.coverImg
    this.isPrivate = data.isPrivate
    this.creatorId = data.creatorId
    this.createdAt = new Date(data.createdAt).toLocaleDateString()
    this.updatedAt = new Date( data.updatedAt).toLocaleDateString()
    this.keeper = false
  }
}