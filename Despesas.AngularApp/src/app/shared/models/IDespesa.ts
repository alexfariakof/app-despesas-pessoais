import { Dayjs } from "dayjs";
import { ICategoria } from "./ICategoria";
export interface IDespesa {
  id: number | null;
  categoria: ICategoria;
  data: Dayjs | string | null;
  descricao: string;
  valor: number;
  dataVencimento: Dayjs | string | null;
}
