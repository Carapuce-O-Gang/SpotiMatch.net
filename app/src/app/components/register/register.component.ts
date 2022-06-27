import { Component, OnInit } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Register } from '@models/register.model';
import { ApiService } from '@services/api-service/api.service';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss']
})

export class RegisterComponent implements OnInit {

	public name: FormControl = new FormControl('', [Validators.required]);
	public displayName: FormControl = new FormControl('', [Validators.required]);
	public email: FormControl = new FormControl('', [Validators.required, Validators.email]);
	public password: FormControl = new FormControl('', [Validators.required]);
	public confirm: FormControl = new FormControl('', [Validators.required]);
	public authorizationToken: FormControl = new FormControl('', [Validators.required]);

	constructor(
    private apiService: ApiService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  public ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      if (params.code) {
        this.authorizationToken.setValue(params.code);
      }

      if (params.error) {
        console.log(params.error);
      }
    });
  }

	public getErrorMessage(obj: FormControl): string {
		if(obj.hasError('required')) {
      return 'Field required';
    }

		return '';
	}

	public onSubmit() {
		const register: Register = {
			name: this.name.value,
			displayName: this.displayName.value,
			email: this.email.value,
			password: this.password.value,
			passwordConfirmation: this.confirm.value,
      authorizationToken: this.authorizationToken.value
		};

		this.apiService.register(register).subscribe({
      next: () => this.router.navigateByUrl('/'),
      error: (error) => console.log(error)
    });
	}
}
