import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DxButtonModule, DxDateBoxModule, DxDrawerModule, DxFormModule, DxListModule, DxTabsModule, DxTextBoxModule, DxToolbarModule, DxTreeListModule } from 'devextreme-angular';
import { DxiFieldModule, DxiItemModule, DxoLabelModule } from 'devextreme-angular/ui/nested';




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
    DxListModule
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
    DxListModule
  ]
})
export class DevextremeModule { }
