import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { Login } from '@models/login.model';
import { ApiService } from '@services/api-service/api.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss']
})

export class LoginComponent implements OnInit {

	public username: FormControl = new FormControl('', [Validators.required]);
	public password: FormControl = new FormControl('', [Validators.required]);

	constructor(private apiService: ApiService, private router: Router) {}

  public ngOnInit(): void {}

	public getErrorMessage(obj: FormControl): string {
		if (obj.hasError('required')) {
			return 'Field required';
		}

		return '';
	}


	redirectionSpotify(){
		const authorizeUrl = 'https://accounts.spotify.com/authorize'+
		`?client_id=${environment.clientId}`+
		`&redirect_uri=${environment.redirectUri}`+
		`&response_type=code`;

		window.location.href = authorizeUrl;
		return true;
	}

	public onSubmit() {
		const login: Login = {
			username: this.username.value,
			password: this.password.value
		};

    this.apiService.login(login).subscribe({
      next: (auth) => {
        localStorage.setItem('token', auth.token);
        this.router.navigateByUrl('/home');
      },
      error: (error) => console.log(error)
    });
	}
}
