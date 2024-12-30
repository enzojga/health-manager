import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { CreateModalComponent } from '../create-modal/create-modal.component';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateModalData, SelectModalData } from '../../interfaces/external-interfaces';
import { MatInputModule } from '@angular/material/input';
import { map, Observable, startWith } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-select-modal',
  templateUrl: './select-modal.component.html',
  styleUrls: ['./select-modal.component.css'],
  imports: [
      MatButtonModule,
      MatDividerModule,
      MatIconModule,
      MatFormFieldModule,
      FormsModule,
      MatAutocompleteModule,
      ReactiveFormsModule,
      MatInputModule,
      AsyncPipe
  ]
})
export class SelectModalComponent {
  
  @ViewChild('input') input!: ElementRef<HTMLInputElement>;
  myControl = new FormControl('');
  filteredOptions: string[];

  constructor(
    public dialogRef: MatDialogRef<CreateModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: SelectModalData
  ) {
    this.filteredOptions = this.data.options.slice();
  }

  filter(): void {
    const filterValue = this.input.nativeElement.value.toLowerCase();
    this.filteredOptions = this.data.options.filter(o => o.toLowerCase().includes(filterValue));
  }

  closeModal() {
    this.dialogRef.close(this.myControl.value);
  }
}
