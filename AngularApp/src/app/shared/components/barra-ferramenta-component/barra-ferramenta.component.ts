import { Component, Input } from "@angular/core";
import { FilterAnoService, FilterMesAnoService } from "../../services";
import { BarraFerramentaClass } from "./barra-ferramenta.abstract";

@Component({
  selector: 'app-barra-ferramenta',
  templateUrl: './barra-ferramenta.component.html',
  styleUrls: ['./barra-ferramenta.component.scss']
})

export class BarraFerramentaComponent implements BarraFerramentaClass {
  @Input() onClickNovo: Function = () => {};
  @Input() btnNovo: boolean =  false;
  @Input() dtAno: boolean = false;
  @Input() dtMesAno: boolean = false;

  onChangeDataAno: Function = () => { };
  setOnChangeDataAno = (onChangeDataAno: Function) => {
    this.onChangeDataAno = onChangeDataAno;
  }

  onChangeDataMesAno: Function = () => { };
  setOnChangeDataMesAno = (onChangeMesAno: Function) => {
    this.onChangeDataMesAno = onChangeMesAno;
  }

  constructor(public filterAnoService: FilterAnoService,  public filterMesAnoService: FilterMesAnoService) {}

  clickBtnNovo = () => {
    if (this.onClickNovo) {
      this.onClickNovo();
    }
  }

  clickBtnVoltar = () => {
    window.history.back();
  }
}
