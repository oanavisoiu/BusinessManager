import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxChartModule, DxDataGridModule, DxDateBoxModule, DxDrawerModule, DxFormModule, DxListModule, DxMenuModule, DxNumberBoxModule, DxSchedulerModule, DxSelectBoxModule, DxTabsModule, DxTextBoxModule, DxToolbarModule, DxTreeListModule, DxTreeViewModule } from 'devextreme-angular';
import { DxiFieldModule, DxiGroupItemModule, DxiItemModule, DxiResourceModule, DxiTotalItemModule, DxoEditingModule, DxoItemModule, DxoLabelModule, DxoLookupModule, DxoSummaryModule } from 'devextreme-angular/ui/nested';
import { DxLookupModule, DxLookupTypes } from 'devextreme-angular/ui/lookup';
import { exportDataGrid } from 'devextreme/pdf_exporter';

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    DxButtonModule,
    DxiItemModule,
    DxTabsModule,
    DxFormModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxiFieldModule,
    DxoLabelModule,
    DxTreeListModule,
    DxToolbarModule,
    DxDrawerModule,
    DxListModule,
    DxMenuModule,
    DxTreeViewModule,
    DxSelectBoxModule,
    DxoEditingModule,
    DxoItemModule,
    DxoSummaryModule,
    DxoSummaryModule,
    DxiTotalItemModule,
    DxiGroupItemModule,
    DxDataGridModule,
    DxoEditingModule,
    DxLookupModule,
    DxNumberBoxModule,
    DxSchedulerModule,
    DxiResourceModule,
    DxChartModule
  ],
  exports: [
    DxButtonModule,
    DxiItemModule,
    DxTabsModule,
    DxFormModule,
    DxTextBoxModule,
    DxDateBoxModule,
    DxiFieldModule,
    DxoLabelModule,
    DxTreeListModule,
    DxToolbarModule,
    DxDrawerModule,
    DxListModule,
    DxMenuModule,
    DxTreeViewModule,
    DxSelectBoxModule,
    DxoEditingModule,
    DxoItemModule,
    DxoSummaryModule,
    DxiTotalItemModule,
    DxiGroupItemModule,
    DxDataGridModule,
    DxLookupModule,
    DxNumberBoxModule,
    DxSchedulerModule,
    DxiResourceModule,
    DxChartModule
  ]
})
export class DevextremeModule { }
