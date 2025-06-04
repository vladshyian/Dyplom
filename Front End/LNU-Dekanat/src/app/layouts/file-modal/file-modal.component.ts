import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FileService } from '../../core/services/file/file.service';

@Component({
  selector: 'app-file-modal',
  standalone: true,
  imports: [],
  templateUrl: './file-modal.component.html',
  styleUrl: './file-modal.component.css'
})
export class FileModalComponent {
  @Input() isOpen: boolean = false;
  @Output() close = new EventEmitter<void>();
  selectedFile: File | null = null;

  constructor(private fileService: FileService) {}

  closeModal(): void {
    this.close.emit();
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  uploadFile(): void {
    if (this.selectedFile) {
      this.fileService.uploadFile(this.selectedFile).subscribe({
        next: (response) => {
          alert('File uploaded successfully!');
          this.closeModal();
        },
        error: (error) => {
          console.error('File upload failed:', error);
          alert('File upload failed!');
        },
      });
    }
  }
}
