import { environment } from "../../../../../environments/environment";

export abstract class AbstractService {
  protected readonly routeUrl: string;
  constructor(route: string) {
    this.routeUrl = `${ environment.BASE_URL }/${ environment.API_VERSION }/${route}`;
  }
}
