import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Login } from '@models/login.model';
import { Auth } from '@models/auth.model';
import { ApiService } from '@services/api-service/api.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

	public username: FormControl = new FormControl('', [Validators.required]);
	public password: FormControl = new FormControl('', [Validators.required]);
	public apiService: ApiService;
	public router: Router;

	constructor(_apiService: ApiService, _router: Router) {
		this.apiService = _apiService;
    this.router = _router;
	}

  public ngOnInit(): void {}

	public getErrorMessage(obj: FormControl): string {
		if (obj.hasError('required')) {
			return 'Field required';
		}

		return '';
	}

	public async onSubmit(): Promise<void> {
		const login: Login = {
			username: this.username.value,
			password: this.password.value
		};

		const auth: Auth = await this.apiService.login(login);
		localStorage.setItem('token', auth.token);
    this.router.navigateByUrl('/home')
	}
}
