import { HubConnectionBuilder } from '@aspnet/signalr'

class ConnectionHub {
  constructor() {
    this.client = HubConnectionBuilder.withUrl('').build()
  }

  start() {
  }
}

export default new ConnectionHub()
