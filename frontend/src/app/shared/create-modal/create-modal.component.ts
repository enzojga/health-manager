import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule, MatIconRegistry } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CreateModalData } from '../../external/interfaces/external-interfaces';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-create-modal',
  templateUrl: './create-modal.component.html',
  styleUrls: ['./create-modal.component.css'],
  imports: [ 
    MatButtonModule,
    MatDividerModule,
    MatIconModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    NgxMaskDirective,
    CommonModule
  ],
  providers: [provideNgxMask()],
})
export class CreateModalComponent implements OnInit {

  modalFrom: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<CreateModalComponent>,
    private readonly formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: CreateModalData
  ) {
    if(data.isPatient) {
      this.modalFrom = this.formBuilder.group({
        name: ['', [Validators.required, Validators.minLength(3)]],
        cpf: ['', Validators.required],
      });
    } else {
      this.modalFrom = this.formBuilder.group({
        name: ['', [Validators.required, Validators.minLength(3)]],
      });
    }
  }

  ngOnInit() {
  }

  closeMoal() {
    const { value } = this.modalFrom;
    this.dialogRef.close(value);
  }

}
