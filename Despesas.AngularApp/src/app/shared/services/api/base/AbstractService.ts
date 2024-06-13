import { environment } from "../../../../../environments/environment";

export abstract class AbstractService {
  protected readonly routeUrl: string; 
  constructor(route: string) {
    this.routeUrl = `${ environment.API_VERSION }/${route}`;
  }
}
