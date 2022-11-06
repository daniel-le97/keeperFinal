import { Profile } from "./Profile";


export class Keep {
  constructor(data) {
    this.id = data.id;
    this.views = data.views;
    this.kept = data.kept;
    this.img = data.img;
    this.name = data.name;
    this.description = data.description;
    this.creatorId = data.creatorId;
    this.creator = new Profile(data.creator);
    this.createdAt = new Date(data.createdAt).toLocaleDateString();
    this.updatedAt = new Date(data.updatedAt).toLocaleDateString();
    this.isKept = false
  }
}
