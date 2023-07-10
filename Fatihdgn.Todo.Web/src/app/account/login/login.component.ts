import { ChangeDetectionStrategy, ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { AuthLoginResponseDto } from 'src/api/models';
import { AuthService } from 'src/api/services';
import { LocalStorageService } from 'src/app/helpers/local-storage.service';
import { SubSink } from 'subsink';

@Component({
  selector: 'todo-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoginComponent implements OnDestroy, OnInit {
  private subs = new SubSink();
  error: string = "";
  f: FormGroup;
  constructor(
    private readonly fb: FormBuilder, 
    private readonly router: Router, 
    private readonly auth: AuthService,
    private readonly storage: LocalStorageService,
    private readonly cdr: ChangeDetectorRef
  ) {
    this.f = this.fb.group({
      email: ['user@example.com', [Validators.required, Validators.email]],
      password: ['Password1!', [Validators.required]],
      rememberMe: [false]
    })
  }
  ngOnInit(): void {
    if(this.storage.get<AuthLoginResponseDto>("login")){
      this.router.navigate(["/"]);
      return;
    }
    const email = this.storage.get<string>("email");
    if(email){
      this.f.get("email")?.setValue(email);
    }
  }

  ngOnDestroy(): void {
    this.subs.unsubscribe();
  }

  redirectToRegister(){
    this.router.navigate(["/account/register"]);
  }

  onSubmit() {
    if(this.f.invalid) return;
    const value = this.f.getRawValue();
    this.subs.sink = this.auth.authLogin({ 
      body:{
        email: value.email,
        password: value.password,
        rememberMe: value.rememberMe
      }
    }).pipe(
      catchError(err => {
        console.log(err);
        this.error = err.error;
        this.cdr.markForCheck();
        return of();
      }),
      tap(resp => {
        this.storage.set("email", value.email);
        this.storage.set("login", resp);
      }),
      tap(() => {
        this.router.navigate(["/"]);
      })
    ).subscribe();
  }
}
