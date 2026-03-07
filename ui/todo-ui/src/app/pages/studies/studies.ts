import { Component, OnInit } from '@angular/core';
import { Study } from '../../models/study.model';
import { StudyService } from '../../services/study';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BehaviorSubject } from 'rxjs';

@Component({
  selector: 'app-studies',
  imports: [FormsModule, CommonModule],
  templateUrl: './studies.html',
  styleUrl: './studies.scss',
})
export class Studies implements OnInit {


  studies$ = new BehaviorSubject<Study[]>([]);

  selectedFiles: File[] = [];
  patientName = '';

  constructor(private studyService: StudyService) { }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.studyService.getAll()
      .subscribe(res => this.studies$.next(res));
  }

  onFileChange(event: any) {
    if (event.target.files) {
      this.selectedFiles = Array.from(event.target.files);
    }
  }

  upload() {
    if (!this.selectedFiles.length) return;

    this.studyService
      .create(this.selectedFiles, this.patientName)
      .subscribe(() => {
        this.patientName = '';
        this.selectedFiles = [];
        this.load();
      });
  }

  delete(id: number) {
    this.studyService.delete(id)
      .subscribe(() => this.load());
  }

  view(study: Study) {
    window.location.href =
      `http://localhost/viewer?StudyInstanceUIDs=${study.studyInstanceUID}`;
  }
}