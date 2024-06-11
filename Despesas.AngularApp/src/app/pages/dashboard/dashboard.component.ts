import { Component, ViewChild } from '@angular/core';
import * as dayjs from 'dayjs';
import { BarraFerramentaComponent, BarChartComponent, AlertComponent, AlertType } from 'src/app/shared/components';
import { MenuService, FilterAnoService } from 'src/app/shared/services';
import { DashboardService } from 'src/app/shared/services/api';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent {
  @ViewChild(BarraFerramentaComponent) barraFerramenta: BarraFerramentaComponent;
  @ViewChild(BarChartComponent) baseChart: BarChartComponent;
  barChartDatasets: any[] = [];
  barChartLabels: string[] = [];

  constructor(
    public menuService: MenuService,
    public dashboardService: DashboardService,
    public modalAlert: AlertComponent,
    private filterAnoService: FilterAnoService) {}

  ngOnInit() {
    this.menuService.setMenuSelecionado(1);
    this.initializeChart();
  }

  initializeChart = () => {
    this.dashboardService.getDataGraphicByYear(dayjs(`${this.filterAnoService.dataAno}-01-01`))
      .subscribe({
        next: (response: any) => {
          if (response) {
            this.barChartLabels = response.labels;
            this.barChartDatasets = response.datasets;
            this.baseChart.loadBarChart(response.labels, response.datasets);
            this.barraFerramenta.setOnChangeDataAno(this.updateChart);

          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }

  updateChart = () => {
    this.dashboardService.getDataGraphicByYear(dayjs(`${this.filterAnoService.dataAno}-01-01`))
      .subscribe({
        next: (response: any) => {
          if (response) {
            this.barChartDatasets = response.datasets;
            this.baseChart.updateBarChart(response.datasets);
          }
        },
        error: (errorMessage: string) => {
          this.modalAlert.open(AlertComponent, errorMessage, AlertType.Warning);
        }
      });
  }
}
