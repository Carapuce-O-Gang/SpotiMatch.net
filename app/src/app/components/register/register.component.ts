import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {

	public name: FormControl = new FormControl('', [Validators.required]);
	public username: FormControl = new FormControl('', [Validators.required]);
	public email: FormControl = new FormControl('', [Validators.required, Validators.email]);
	public password: FormControl = new FormControl('', [Validators.required]);
	public confirm: FormControl = new FormControl('', [Validators.required]);

	constructor() {}

	public getErrorMessage(obj: FormControl): string {
		if(obj.hasError('required'))
			return "Field required";

		return '';
	}

	public onSubmit(): void {
		let data = {
			name: this.name.value,
			username: this.username.value,
			email: this.email.value,
			password: this.password.value,
			confirm: this.confirm.value
		};

		console.table(data);
	}

	public ngOnInit(): void {

	}
}
