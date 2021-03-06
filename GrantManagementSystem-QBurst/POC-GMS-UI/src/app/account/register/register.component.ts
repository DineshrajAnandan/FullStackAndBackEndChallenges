import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AccountService } from '../../services/account.service';
import { ToasterService } from 'src/app/shared/toaster.service';

@Component({ templateUrl: './register.component.html' })
export class RegisterComponent implements OnInit {
    form: FormGroup;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private toaster: ToasterService
    ) { }

    ngOnInit() {
        this.form = this.formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            username: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    get f() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;

        if (this.form.invalid) {
            return;
        }

        this.loading = true;

        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe(
                data => {
                    this.toaster.success('Registration successful');
                    this.router.navigate(['../login'], { relativeTo: this.route });
                },
                error => {
                    this.toaster.error('Registration failed');
                    this.loading = false;
                });
    }
}