import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

	public username = new FormControl('', [Validators.required]);
	public password = new FormControl('', [Validators.required]);

	constructor() {}

	public getErrorMessage(obj: FormControl): string {
		if(obj.hasError('required'))
			return "Field required";

		return '';
	}

	public onSubmit(): void {
		
	}

	public onCreateCtx(): void {

	}

	public ngOnInit(): void {

	}
}
