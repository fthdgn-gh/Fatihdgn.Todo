import { NgModule, Provider, forwardRef } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";

import { AppComponent } from "./app.component";
import { LoginComponent } from "./account/login/login.component";
import { RegisterComponent } from "./account/register/register.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AppRoutingModule } from "./app.routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { ApiModule } from "src/api/api.module";
import { environment } from "src/environments/environment";

import { ApiInterceptor } from "./helpers/api.interceptor";
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { SideNavComponent } from "./components/sidenav/sidenav.component";

export const API_INTERCEPTOR_PROVIDER: Provider = {
    provide: HTTP_INTERCEPTORS,
    useExisting: forwardRef(() => ApiInterceptor),
    multi: true
};

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        RegisterComponent,
        DashboardComponent,
        SideNavComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        AppRoutingModule,
        HttpClientModule,
        ApiModule.forRoot({ rootUrl: environment.apiBaseUrl })
    ],
    providers: [
        ApiInterceptor,
        API_INTERCEPTOR_PROVIDER
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
