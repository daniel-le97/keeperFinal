export class Profile {
  constructor(data) {
    this.id = data.id;
    this.name = data.name;
    this.picture = data.picture;
    this.coverImg =
      data.coverImg ||
      "https://images.unsplash.com/photo-1489674267075-cee793167910?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1674&q=80";
  }
}
