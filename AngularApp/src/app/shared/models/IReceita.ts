import { Dayjs } from "dayjs";
import { ICategoria } from "./ICategoria";
export interface IReceita {
  id: number | null;
  categoria?: ICategoria;
  data: Dayjs | string | null;
  descricao: string;
  valor: number;
}
