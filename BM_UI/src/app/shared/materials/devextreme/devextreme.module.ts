import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxDataGridModule, DxDateBoxModule, DxDrawerModule, DxFormModule, DxListModule, DxMenuModule, DxSelectBoxModule, DxTabsModule, DxTextBoxModule, DxToolbarModule, DxTreeListModule, DxTreeViewModule } from 'devextreme-angular';
import { DxiFieldModule, DxiGroupItemModule, DxiItemModule, DxiTotalItemModule, DxoEditingModule, DxoItemModule, DxoLabelModule, DxoSummaryModule } from 'devextreme-angular/ui/nested';

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
    DxoEditingModule
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

  ]
})
export class DevextremeModule { }
