import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { NgChartsModule } from "ng2-charts";
import { BarChartComponent } from "../../shared/components";
import { DashboardService } from "../../shared/services/api";
import { SharedModule } from "../../shared/shared.module";
import { DashboardComponent } from "./dashboard.component";
import { DashboardRoutingModule } from "./dashboard.routing.module";

@NgModule({
  declarations: [DashboardComponent, BarChartComponent],
  imports: [CommonModule, DashboardRoutingModule, SharedModule, NgChartsModule],
  providers: [DashboardService ]
})

export class DashboardModule {}
