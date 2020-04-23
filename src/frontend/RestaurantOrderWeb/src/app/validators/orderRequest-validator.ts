import { AbstractControl, ValidationErrors, ValidatorFn, Validator, NG_VALIDATORS, FormControl } from '@angular/forms';

export function orderRequestValidator(control: AbstractControl): ValidationErrors | null {
    const orderRequest = control.value.split(',');

    // No need to validate if the request is empty
    if (control.value == '') {
        return null;
    }

    // Excludes blank dishes
    for (let i = 0; i < orderRequest.length; i++) {
        if (orderRequest[i] === '') {
            orderRequest.splice(i, 1);
        }
    }

    // It is mandatory to have an time of day and at least one dish
    if (orderRequest.length < 2) {
        return { orderRequestValidator: true};
    }

    const regexp = new RegExp('[a-zA-Z]');

    // The time of day must be a word
    if (!regexp.test(orderRequest[0])) {
        return { orderRequestValidator: true};
    }

    // The dish must be represented by a number greater than zero
    for (let x = 1; x < orderRequest.length; x++) {
        if (isNaN(orderRequest[x]) || orderRequest[x] < 1) {
            return { orderRequestValidator: true};
        }
    }

    return null;
}
