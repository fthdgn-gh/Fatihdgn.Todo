import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function passwordConfirmationValidator(control: AbstractControl, confirmationControl: AbstractControl): ValidatorFn {
    return (): ValidationErrors | null => {
        const password = control.value;
        const confirmPassword = confirmationControl.value;
        if (password !== confirmPassword) {
            return { passwordMismatch: true };
        }

        return null;
    };
}