import { ChangeDetectorRef, Component, Input, OnInit, ViewChild } from '@angular/core';
import { ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';
import DataLabelsPlugin from 'chartjs-plugin-datalabels';
import { ViewportRuler } from '@angular/cdk/scrolling';

@Component({
  selector: 'app-bar-chart',
  templateUrl: './bar-chart.component.html',
  styleUrls: ['./bar-chart.component.scss'],
})
export class BarChartComponent implements OnInit {
  @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;
  @Input() datasets: any[] = [];
  private resizeTimeout: any;

  constructor(private viewportRuler: ViewportRuler, private cdr: ChangeDetectorRef) { }

  public barChartOptions: any = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        labels: {
          font: {
            size: 14
          }
        }
      },
      datalabels: {
        font: {
          size: 8
        },
        anchor: 'end',
        align: 'end',
        formatter: (value, context) => {
          return new Intl.NumberFormat('pt-BR', {
            style: 'currency',
            currency: 'BRL',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
          }).format(value);
        }
      },
      tooltip: {
        callbacks: {
          label: function (context) {
            let label = context.dataset.label || '';

            if (label) {
              label += ': ';
            }
            if (context.parsed.y !== null) {
              label += new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(context.parsed.y);
            }
            return label;
          }
        }
      }
    }
  };

  public barChartType: ChartType = 'bar';
  public barChartPlugins = [DataLabelsPlugin];

  public barChartData: ChartData<'bar'> = {
    labels: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
    datasets: this.datasets
  };

  public chartClicked({ event, active }: { event?: ChartEvent, active?: {}[] }): void {
    console.log(event, active);
  }

  public chartHovered({ event, active }: { event?: ChartEvent, active?: {}[] }): void {
    console.log(event, active);
  }

  public loadBarChart(lables: any[], datasetes: any[]): void {
    this.barChartData.labels = lables;
    this.barChartData.datasets = datasetes;
    this.chart?.update();
  }

  public updateBarChart(datasetes: any[]): void {
    this.barChartData.datasets = datasetes;
    this.chart?.update();
  }

  ngOnInit(): void {
    this.checkWindowSize();
    window.addEventListener('resize', () => {
      clearTimeout(this.resizeTimeout);
      this.resizeTimeout = setTimeout(() => {
        this.checkWindowSize();
      }, 100);
    });
  }

  private checkWindowSize(): void {
    const windowWidth = this.viewportRuler.getViewportSize().width;
    this.barChartOptions.indexAxis = windowWidth < 653 ? 'y' : undefined;
    this.cdr.detectChanges();
    this.chart?.render();
    this.chart?.update();
  }
}
