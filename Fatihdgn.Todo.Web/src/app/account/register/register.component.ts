import { Component, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, tap } from 'rxjs';
import { AuthService } from 'src/api/services';
import { LocalStorageService } from 'src/app/helpers/local-storage.service';
import { passwordConfirmationValidator } from 'src/app/helpers/password-confirmation.validator';
import { SubSink } from 'subsink';

@Component({
  selector: 'todo-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnDestroy {
  private subs = new SubSink();
  f: FormGroup;
  errors: string[] = [];
  constructor(
    private readonly fb: FormBuilder,
    private readonly router: Router,
    private readonly auth: AuthService,
    private readonly storage: LocalStorageService
  ) {
    this.f = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
    });
    this.f.addValidators(passwordConfirmationValidator(
      this.f.get('password')!,
      this.f.get('confirmPassword')!
    ))
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  onSubmit() {
    const values = this.f.getRawValue();
    this.subs.sink = this.auth.authRegister({
      body: {
        email: values.email,
        password: values.password,
        confirmPassword: values.confirmPassword
      }
    })
      .pipe(
        catchError(err => {
          const errors = JSON.parse(err.error) as [{ code: string, description: string }];
          while (this.errors.length > 0)
            this.errors.pop();
          errors.map(error => error.description).forEach(error => {
            this.errors.push(error);
          });

          return of();
        }),
        tap(() => {
          this.storage.set("email", values.email);
          this.redirectToLogin();
        })
      )
      .subscribe();
  }

  redirectToLogin() {
    this.router.navigate(["/account/login"]);
  }
}
