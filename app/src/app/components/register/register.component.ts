import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {

	public name = new FormControl('', [Validators.required]);
	public username = new FormControl('', [Validators.required]);
	public password = new FormControl('', [Validators.required]);
	public confirm = new FormControl('', [Validators.required]);

	constructor() {}

	public getErrorMessage(obj: FormControl): string {
		if(obj.hasError('required'))
			return "Field required";

		return '';
	}

	public onSubmit(): void {

	}

	public ngOnInit(): void {

	}
}
