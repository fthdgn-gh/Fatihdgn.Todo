import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { AuthGuard } from "./helpers/auth.guard";
import { LoginComponent } from "./account/login/login.component";
import { RegisterComponent } from "./account/register/register.component";

const routes: Routes = [
    {
        path: "",
        redirectTo:"dashboard",
        pathMatch: "full"
    },
    {
        path: "dashboard",
        component: DashboardComponent,
        canActivate: [AuthGuard]
    },

    {
        path: "account/login",
        component: LoginComponent
    },
    {
        path: "account/register",
        component: RegisterComponent
    },
    // Other routes...
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }